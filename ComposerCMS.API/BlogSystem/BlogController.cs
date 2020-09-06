using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Blog blog)
        {
            try
            {
                await this._blogUtil.ProcessExistingBlog(blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }

        [HttpGet("get/{blogID}")]
        public async Task<IActionResult> List([FromRoute] Guid blogID)
        {
            try
            {
                Blog _blog = await this._blogUtil.Table.Where(a => a.ID == blogID).FirstOrDefaultAsync();
                return StatusCode(200, _blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
