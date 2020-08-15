using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Models.System.BlogSystem
{
    public class BlogModel : PageModel
    {
        private readonly BlogUtility _blogUtil;
        private readonly PostUtility _postUtil;
        private readonly RouteUtility _routeUtil;

        [BindProperty]
        public Blog Blog { get; set; }

        [BindProperty]
        public List<Post> PinnedPosts { get; set; }

        [BindProperty]
        public List<Post> Posts { get; set; }

        [BindProperty]
        public Dictionary<Guid, Route> RouteMap { get; set; } = new Dictionary<Guid, Route>();

        public BlogModel(BlogUtility blogUtil, PostUtility postUtil, RouteUtility routeUtil)
        {
            this._blogUtil = blogUtil;
            this._postUtil = postUtil;
            this._routeUtil = routeUtil;
        }

        public async Task OnGet()
        {
            Guid _blogID;

            if (Guid.TryParse(this.HttpContext.Items["BlogID"].ToString(), out _blogID))
            {
                this.Blog = await this._blogUtil.FirstOrDefaultAsync(a => a.ID == _blogID);

                List<Post> _posts = await this._postUtil.QueryReleasedPosts(_blogID);

                this.Posts = _posts.Where(a => !a.IsPinned).ToList();
                this.PinnedPosts = _posts.Where(a => a.IsPinned).ToList();

                // TODO: Fix this disaster.
                List<string> _postEntityIDs = _posts.Select(a => a.ID.ToString()).ToList();
                var routes = await this._routeUtil.ToListAsync(a => _postEntityIDs.Contains(a.EntityID));

                for (int i = 0; i < _postEntityIDs.Count; i++)
                {
                    this.RouteMap.Add(Guid.Parse(_postEntityIDs[i]), routes[i]);
                }
            }
            else
            {
                // Show blog does not exist page.
            }
        }
    }
}