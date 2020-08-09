using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/post")]
    public class PostController : Controller
    {
        private readonly PostUtility _postUtil;

        public PostController(PostUtility postUtil)
        {
            this._postUtil = postUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Post post)
        {
            try
            {
                await this._postUtil.ProcessNewPost(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Post post)
        {
            try
            {
                await this._postUtil.ProcessExistingPost(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("get/{postID}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid postID)
        {
            try
            {
                Post _post = await this._postUtil.Table.Where(a => a.ID == postID).FirstOrDefaultAsync();
                return StatusCode(200, _post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list/{blogID}")]
        public async Task<IActionResult> ListPosts([FromRoute] Guid blogID)
        {
            try
            {
                List<Post> _posts = await this._postUtil.Table.Where(a => a.BlogID == blogID).ToListAsync();
                return StatusCode(200, _posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
