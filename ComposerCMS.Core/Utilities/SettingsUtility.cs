using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class SettingsUtility : BaseRepository<Settings>
    {
        public SettingsUtility(UserResolver userResolver) : base(userResolver)
        {

        }

        public async Task<Settings> GetCurrent()
        {
            return await this.FirstOrDefaultAsync(a => a.ID == Constants.Settings.ID);
        }

        public async Task UpdateSystemSettings(Settings settings)
        {
            Settings _existingSettings = await this.FirstOrDefaultAsync(a => a.ID == settings.ID);

            _existingSettings.Title = settings.Title;
            _existingSettings.DefaultRouteID = settings.DefaultRouteID;
            _existingSettings.MinimizeJs = settings.MinimizeJs;
            _existingSettings.MinimizeCss = settings.MinimizeCss;

            await this.UpdateAsync(_existingSettings);
        }

        public async Task SetTheme(Guid themeKey)
        {
            Settings _existingSettings = await this.GetCurrent();
            _existingSettings.ThemeKey = themeKey;
            await this.UpdateAsync(_existingSettings);

            ComposerCMSApp.Settings.ThemeKey = themeKey;
        }

        public async Task<Guid?> GetTheme()
        {
            Settings _existingSettings = await this.GetCurrent();
            return _existingSettings.ThemeKey;
        }

        public async Task SyncFileSystem()
        {
            List<Page> _systemPages = null;

            _systemPages = this.ComposerCMSContext.Page.Where(a => a.IsSystemPage).ToList();

            foreach (RouteSection routeSection in ComposerCMSApp.SystemRoutes)
            {
                foreach (RouteItem routeItem in routeSection.RouteItems)
                {
                    // Add variable to appsettings to allow the user to change the "Pages" folder to something else.
                    string _systemPagePath = Path.Combine(Environment.CurrentDirectory, "Pages", $"{routeItem.PhysicalPath.Trim('/')}.cshtml");
                    string _fileName = Path.GetFileNameWithoutExtension(_systemPagePath);
                    string _pageContents = File.ReadAllText(_systemPagePath);

                    Page _systemPage = _systemPages.FirstOrDefault(a => a.Path == _systemPagePath);

                    _systemPage.Title = _fileName;
                    _systemPage.Path = _systemPagePath;
                    _systemPage.Content = _pageContents;
                    _systemPage.IsAbstract = routeItem.IsAbstract;
                    _systemPage.DateLastUpdated = DateTime.UtcNow;
                }
            }

            this.ComposerCMSContext.Page.UpdateRange(_systemPages);
            await this.ComposerCMSContext.SaveChangesAsync();
        }

        public async Task PurgeSystem()
        {
            await this.ComposerCMSContext.ReinitializeDatabase();
        }
    }
}