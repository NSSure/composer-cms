using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposerCMS.Web.Models.Shared
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly RouteUtility _routeUtil;

        public List<Route> Routes { get; set; } = new List<Route>();

        public BreadcrumbsViewComponent(RouteUtility routeUtil)
        {
            this._routeUtil = routeUtil;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string _referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(_referer))
            {
                string _path = _referer.Split(Request.Host.Value)[1];

                Route _route = await this._routeUtil.FirstOrDefaultAsync(a => a.Url == _path.TrimEnd('/'));

                if (_route != null)
                {
                    this.Routes.Add(_route);
                }
            }

            // Searches for default.cshtml
            return View(this.Routes);
        }
    }
}
