using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/blog")]
    public class BlogController : Controller
    {
        private readonly BlogUtility _blogUtil;

        public BlogController(BlogUtility blogUtil)
        {
            this._blogUtil = blogUtil;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Blog blog)
        {
            try
            {
                await this._blogUtil.ProcessNewBlog(blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                List<Blog> _blogs = await this._blogUtil.Table.ToListAsync();
                return StatusCode(200, _blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
