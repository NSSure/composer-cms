using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ComposerCMS.Core;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Http;
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
                IFormFile file = Request.Form.Files[0];

                string _extension = Path.GetExtension(file.FileName);

                if (Constants.File.SupportedImageExtensions.Contains(_extension))
                {
                    await this._externalResourceUtil.ConvertMediaFileToExternalResource(file);
                }
                else if (_extension == ".js")
                {
                    await this._externalResourceUtil.ConvertJsFileToExternalResourcec(file);
                }
                else if (_extension == ".css")
                {
                    await this._externalResourceUtil.ConvertCssFileToExternalResource(file);
                }
                else
                {
                    throw new Exception("Unsupported file type uploaded.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(200, true);
        }
    }
}
