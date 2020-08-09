using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using ComposerCMS.Core.Utilities;
using ComposerCMS.Core.Model;
using System.Linq;
using ComposerCMS.Core;
using ComposerCMS.Core.Models;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly SettingsUtility _settingsUtil;
        private readonly PageUtility _pageUtil;
        private readonly BlogUtility _blogUtil;
        private readonly PostUtility _postUtil;
        private readonly MenuUtility _menuUtil;
        private readonly ActivityHistoryUtility _activityHistoryUtil;

        public SettingsController(SettingsUtility settingsUtil, PageUtility pageUtil, BlogUtility blogUtil, PostUtility postUtil, MenuUtility menuUtil, ActivityHistoryUtility activityHistoryUtil)
        {
            this._settingsUtil = settingsUtil;
            this._pageUtil = pageUtil;
            this._blogUtil = blogUtil;
            this._postUtil = postUtil;
            this._menuUtil = menuUtil;
            this._activityHistoryUtil = activityHistoryUtil;
        }

        /// <summary>
        /// Updates the system settings for the site.
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public async Task<IActionResult> UpdateSystemSettings([FromBody] Settings settings)
        {
            try
            {
                await this._settingsUtil.UpdateSystemSettings(settings);
                return StatusCode(200, true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets the system settings for the site.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetSystemSettings()
        {
            try
            {
                Settings _settings = await this._settingsUtil.Table.FirstOrDefaultAsync();
                return StatusCode(200, _settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns an object containing some basic counts related to system entities.
        /// </summary>
        /// <returns></returns>
        [HttpGet("dashboard/stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                DashboardStats _dashboardStats = new DashboardStats();

                _dashboardStats.PageCount = await this._pageUtil.Table.CountAsync();
                _dashboardStats.BlogCount = await this._blogUtil.Table.CountAsync();
                _dashboardStats.PostCount = await this._postUtil.Table.CountAsync();
                _dashboardStats.MenuCount = await this._menuUtil.Table.CountAsync();

                _dashboardStats.RecentActivity = await this._activityHistoryUtil.Table.OrderByDescending(a => a.DateAdded).Take(5).ToListAsync();

                return StatusCode(200, _dashboardStats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Returns the themes defined within the appsettings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("list/themes")]
        public async Task<IActionResult> ListThemes()
        {
            try
            {
                return StatusCode(200, ComposerCMSApp.Themes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets the theme key property from the settings entity.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get/theme")]
        public async Task<IActionResult> GetTheme()
        {
            try
            {
                Guid? _themeKey = await this._settingsUtil.GetTheme();
                return StatusCode(200, _themeKey);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Updates the theme key property in the settings entity and updates the global
        /// settings object used throughout the system.
        /// </summary>
        /// <param name="themeKey"></param>
        /// <returns></returns>
        [HttpPost("set/theme")]
        public async Task<IActionResult> SetTheme([FromBody] Identifier<Guid> themeKey)
        {
            try
            {
                await this._settingsUtil.SetTheme(themeKey.Value);
                return StatusCode(200, true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Sync the system pages markup from the file system into the corresponding page entities in the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("file/system/sync")]
        public async Task<IActionResult> SyncFileSystem()
        {
            try
            {
                await this._settingsUtil.SyncFileSystem();
                return StatusCode(200, true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Purges and recreates the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet("system/purge")]
        public async Task<IActionResult> Purge()
        {
            try
            {
                await this._settingsUtil.PurgeSystem();
                return StatusCode(200, true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
