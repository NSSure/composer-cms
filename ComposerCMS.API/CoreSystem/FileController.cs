using System;
using System.Threading.Tasks;
using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly FileUtility _fileUtil;
        private readonly ExternalResourceUtility _externalResourceUtil;

        public FileController(FileUtility fileUtility, ExternalResourceUtility externalResourceUtility)
        {
            this._fileUtil = fileUtility;
            this._externalResourceUtil = externalResourceUtility;
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                await this._externalResourceUtil.ConvertFilesToStandaloneResource(Request.Form.Files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }
    }
}
