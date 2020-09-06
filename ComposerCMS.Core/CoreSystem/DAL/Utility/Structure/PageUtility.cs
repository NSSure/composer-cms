using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Entity.Structure;
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
        private readonly PageResourceUtility _pageResourceUtil;
        private readonly PageResourcePackageUtility _pageResourcePackageUtil;
        private readonly ExternalResourceUtility _externalResourceUtil;
        private readonly SettingsUtility _settingsUtil;
        private readonly PageVersionUtility _pageVersionUtil;
        private readonly RouteUtility _routeUtil;
        private readonly UserResolver _userResolver;

        public PageUtility(
            FileUtility fileUtil,
            PageResourceUtility pageResourceUtil,
            PageResourcePackageUtility pageResourcePackageUtil,
            ExternalResourceUtility externalResourceUtil,
            SettingsUtility settingsUtil,
            PageVersionUtility pageVersionUtil,
            RouteUtility routeUtil,
            UserResolver userResolver) : base(userResolver)
        {
            this._fileUtil = fileUtil;
            this._pageResourceUtil = pageResourceUtil;
            this._pageResourcePackageUtil = pageResourcePackageUtil;
            this._externalResourceUtil = externalResourceUtil;
            this._settingsUtil = settingsUtil;
            this._pageVersionUtil = pageVersionUtil;
            this._routeUtil = routeUtil;
            this._userResolver = userResolver;
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

            StringBuilder _contentBuilder = new StringBuilder(page.Content);

            // Append any resources to the page content.
            Dictionary<HTMLTag, string> _includesMap = await this.GenerateResourcesMap(page.ID);

            foreach (KeyValuePair<HTMLTag, string> includeMap in _includesMap)
            {
                if (includeMap.Key == HTMLTag.Script)
                {
                    _contentBuilder.Insert(0, includeMap.Value);
                }
                else
                {
                    _contentBuilder.AppendLine(includeMap.Value);
                }
            }

            page.Content = _contentBuilder.ToString();

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

        private async Task<Dictionary<HTMLTag, string>> GenerateResourcesMap(Guid pageID)
        {
            StringBuilder _scriptsBuilder = new StringBuilder();
            StringBuilder _stylesBuilder = new StringBuilder();

            List<PagePackage> _pagePackages = this._pageResourcePackageUtil.Table.Where(a => a.PageID == pageID).OrderBy(a => a.Order).ToList();

            if (_pagePackages.Count > 0)
            {
                ExternalPackageUtility _packageUtil = new ExternalPackageUtility(this._externalResourceUtil, this._userResolver);

                foreach (PagePackage package in _pagePackages)
                {
                    var _includeMap = await _packageUtil.GenerateResourceIncludes(package.ID);

                    _scriptsBuilder.AppendLine(_includeMap[HTMLTag.Script].Compile());
                    _stylesBuilder.AppendLine(_includeMap[HTMLTag.Link].Compile());
                }
            }

            List<Guid> _scopedResourceIDs = this._pageResourceUtil.Table.Where(a => a.PageID == pageID).OrderBy(a => a.Order).Select(a => a.ExternalResourceID).ToList();
            List<ExternalResource> _scopedResources = await this._externalResourceUtil.Table.Where(a => _scopedResourceIDs.Contains(a.ID)).ToListAsync();

            if (_scopedResources.Count > 0)
            {
                foreach (ExternalResource externalResource in _scopedResources)
                {
                    HTMLNode _includeNode = this._externalResourceUtil.GenerateIncludeNode(externalResource);

                    if (_includeNode.Tag == HTMLTag.Script)
                    {
                        _scriptsBuilder.AppendLine(_includeNode.Compile());
                    }
                    else
                    {
                        _stylesBuilder.AppendLine(_includeNode.Compile());
                    }
                }
            }

            return new Dictionary<HTMLTag, string>()
            {
                { HTMLTag.Script, _scriptsBuilder.ToString() },
                { HTMLTag.Link, _stylesBuilder.ToString() },
            };
        }
    }
}