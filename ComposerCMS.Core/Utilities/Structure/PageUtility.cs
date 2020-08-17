using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class PageUtility : BaseRepository<Page>
    {
        private readonly FileUtility _fileUtil;
        private readonly PageScriptUtility _pageScriptUtil;
        private readonly ExternalResourceUtility _externalResourceUtil;
        private readonly SettingsUtility _settingsUtil;
        private readonly PageVersionUtility _pageVersionUtil;
        private readonly RouteUtility _routeUtil;

        public PageUtility(
            FileUtility fileUtil,
            PageScriptUtility pageScriptUtil,
            ExternalResourceUtility externalResourceUtil,
            SettingsUtility settingsUtil,
            PageVersionUtility pageVersionUtil,
            RouteUtility routeUtil,
            UserResolver userResolver) : base(userResolver)
        {
            this._fileUtil = fileUtil;
            this._pageScriptUtil = pageScriptUtil;
            this._externalResourceUtil = externalResourceUtil;
            this._settingsUtil = settingsUtil;
            this._pageVersionUtil = pageVersionUtil;
            this._routeUtil = routeUtil;
        }

        public async Task ProcessExitingPage(Page page, bool isPublished = false)
        {
            DateTime _currentDate = DateTime.UtcNow;

            page.DateLastUpdated = _currentDate;

            if (isPublished)
            {
                page.IsPublished = true;
                page.DateLastPublished = _currentDate;
                page.DateLastPublished = _currentDate;
            }

            await this.UpdateAsync(page);
        }

        public async Task Publish(Page page)
        {
            if (page.ID == default(Guid))
            {
                throw new Exception("Please save your draft before publishing.");
            }

            // Append any resources to the page content.
            string _resources = await this.ProcessScripts(page);
            page.Content += $"{Environment.NewLine}{_resources}";

            // Write the file with a normalized name to the pages folder.
            await this._fileUtil.WriteFile(page);

            // Update the page entity in the database.
            await this.ProcessExitingPage(page, isPublished: true);

            // Add a page version for the published page.
            await this._pageVersionUtil.AddAsync(new PageVersion()
            {
                PageID = page.ID,
                Name = page.Name,
                Title = page.Title,
                Path = page.Path,
                Content = page.Content,
                DateAdded = DateTime.UtcNow
            });

            // Only try and add the route is the page being updated is not a system page. The system page routes are seeded on startup.
            if (!page.IsSystemPage)
            {
                // Try and add the route if this is the first time being published.
                _ = await this._routeUtil.TryProcessRoute(page.ID, page.Title);
            }
        }

        private async Task<string> ProcessScripts(Page page)
        {
            List<PageScript> _pageScripts = this._pageScriptUtil.Table.Where(a => a.PageID == page.ID).OrderBy(a => a.LoadOrder).ToList();

            if (_pageScripts.Count > 0)
            {
                Settings _settings = await this._settingsUtil.GetCurrent();

                StringBuilder _s = new StringBuilder();

                List<Guid> _externalResourceIDs = _pageScripts.Select(a => a.ExternalResourceID).ToList();
                List<ExternalResource> _scriptResources = await this._externalResourceUtil.Table.Where(a => _externalResourceIDs.Contains(a.ID)).ToListAsync();

                _s.AppendLine("@section Scripts {");

                foreach (ExternalResource resource in _scriptResources)
                {
                    _s.AppendLine(string.Format("<link href=\"{0}\" rel=\"{1}\" />", resource.Href, "stylesheet"));
                }

                _s.AppendLine("}");

                return _s.ToString();
            }

            return string.Empty;
        }
    }
}