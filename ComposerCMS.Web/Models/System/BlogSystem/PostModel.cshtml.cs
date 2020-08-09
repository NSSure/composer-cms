using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace ComposerCMS.Web.Models.System.BlogSystem
{
    public class PostModel : PageModel
    {
        private readonly PostUtility _postUtil;

        [BindProperty]
        public Post Post { get; set; }

        public PostModel(PostUtility postUtil)
        {
            this._postUtil = postUtil;
        }

        public async Task OnGet()
        {
            Guid _postID;

            if (Guid.TryParse(this.HttpContext.Items["PostID"].ToString(), out _postID))
            {
                this.Post = await this._postUtil.FirstOrDefaultAsync(a => a.ID == _postID);
            }
            else
            {
                // Show post does not exist page.
            }
        }
    }
}