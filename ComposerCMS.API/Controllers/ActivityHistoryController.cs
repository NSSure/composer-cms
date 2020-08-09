using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/activity/history")]
    public class ActivityHistoryController : Controller
    {
        private readonly ActivityHistoryUtility _activityHistoryUtil;

        public ActivityHistoryController(ActivityHistoryUtility activityHistoryUtil)
        {
            this._activityHistoryUtil = activityHistoryUtil;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<ActivityHistory> _activityHistory = await this._activityHistoryUtil.Table.ToListAsync();
                return StatusCode(200, _activityHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
