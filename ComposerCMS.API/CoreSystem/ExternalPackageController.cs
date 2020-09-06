using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Web.Controllers
{
    [Route("api/external/package")]
    public class ExternalPackageController : Controller
    {
        private readonly ExternalPackageUtility _externalPackageUtil;

        public ExternalPackageController(ExternalPackageUtility externalPackageUtil)
        {
            this._externalPackageUtil = externalPackageUtil;
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadPackage()
        {
            try
            {
                await this._externalPackageUtil.UploadLocalPackage(Request.Form.Files, Request.Form["params"]);
                return StatusCode(200, true);
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
                var _t = await this._externalPackageUtil.Table.ToListAsync();

                List<PackageBundle> _models = new List<PackageBundle>();

                foreach (var item in _t)
                {
                    List<ExternalResource> _packageResource = await this._externalPackageUtil.ListPackageResources(item.ID);

                    _models.Add(new PackageBundle()
                    {
                        ID = item.ID,
                        DateAdded = item.DateAdded,
                        DateLastUpdated = item.DateLastUpdated,
                        Name = item.Name,
                        UserIDAdded = item.UserIDAdded,
                        UserIDLastUpdated = item.UserIDLastUpdated,
                        PackageResources = _packageResource
                    });
                }

                return StatusCode(200, _models);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
