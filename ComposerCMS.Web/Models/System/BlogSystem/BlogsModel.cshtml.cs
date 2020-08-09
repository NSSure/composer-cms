using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComposerCMS.Web.Models.System.BlogSystem
{
    public class BlogsModel : PageModel
    {
        private readonly BlogUtility _blogUtil;
        private readonly RouteUtility _routeUtil;

        [BindProperty]
        public List<Blog> Blogs { get; set; }

        [BindProperty]
        public Dictionary<Guid, Route> RouteMap { get; set; } = new Dictionary<Guid, Route>();

        public BlogsModel(BlogUtility blogUtil, RouteUtility routeUtil)
        {
            this._blogUtil = blogUtil;
            this._routeUtil = routeUtil;
        }

        public async Task OnGet()
        {
            this.Blogs = this._blogUtil.ListAll();

            List<string> _blogEntityIDs = this.Blogs.Select(a => a.ID.ToString()).ToList();
            var routes = await this._routeUtil  .ToListAsync(a => _blogEntityIDs.Contains(a.EntityID));

            foreach (Blog blog in this.Blogs)
            {
                this.RouteMap.Add(blog.ID, routes.FirstOrDefault(a => a.EntityID == blog.ID.ToString()));
            }
        }
    }
}