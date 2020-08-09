using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/route")]
    public class RouteController : Controller
    {
        private readonly RouteUtility _routeUtil;

        public RouteController(RouteUtility routeUtil)
        {
            this._routeUtil = routeUtil;
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            try
            {
                List<Route> _routes = this._routeUtil.ListAll(isTracking: false);
                return StatusCode(200, _routes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
