using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class ExternalResourceUtility : BaseRepository<ExternalResource>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ExternalResourceUtility(UserManager<IdentityUser> userManager, UserResolver userResolver) : base(userResolver)
        {
            this._userManager = userManager;
        }

        public class ExternalResourceModel : ExternalResource
        {
            /// <summary>
            /// User who added the resource.
            /// </summary>
            public string UserName { get; set; }
        }

        public async Task<List<ExternalResourceModel>> ListStandaloneResourceModelsAsync()
        {
            List<ExternalResourceModel> _models = new List<ExternalResourceModel>();

            List<ExternalResource> externalResources = await this.Table.Where(a => a.ExternalPackageID == null).ToListAsync();
            List<string> _userIDs = externalResources.Select(a => a.UserIDAdded.ToString()).ToList();

            List<IdentityUser> _users = await this._userManager.Users.Where(a => _userIDs.Contains(a.Id)).ToListAsync();

            foreach (ExternalResource resource in externalResources)
            {
                ExternalResourceModel _model = new ExternalResourceModel()
                {
                    DateAdded = resource.DateAdded,
                    DateLastUpdated = resource.DateLastUpdated,
                    Extension = resource.Extension,
                    ExternalPackageID = resource.ExternalPackageID,
                    Name = resource.Name,
                    Order = resource.Order,
                    Path = resource.Path,
                    ID = resource.ID,
                    UserIDAdded = resource.ID,
                    UserIDLastUpdated = resource.UserIDLastUpdated,
                };

                _model.UserName = _users.FirstOrDefault(a => a.Id == resource.UserIDAdded.ToString()).UserName;

                _models.Add(_model);
            }

            return _models;
        }

        public HTMLNode GenerateIncludeNode(ExternalResource resource)
        {
            HTMLNode _node = null;

            switch (resource.Extension.ToLower())
            {
                case "js":
                    _node = new HTMLNode(HTMLTag.Link);
                    _node.AddAttribute(HTMLAttribute.Rel, "stylesheet");
                    _node.AddAttribute(HTMLAttribute.Href, resource.Path);
                    break;
                case "css":
                    _node = new HTMLNode(HTMLTag.Link);
                    _node.AddAttribute(HTMLAttribute.Type, "text/javascript");
                    _node.AddAttribute(HTMLAttribute.Src, resource.Path);
                    break;
                default:
                    // Unsupported file extension skip add resource.
                    throw new Exception("Unsupported resource type. Please ensure your resource is one of the supported options.");
            }

            return _node;
        }

        public async Task ConvertFileToPackageResource(IFormFile formFile, ExternalPackage package)
        {
            string _resourcePath = Path.Combine(Constants.Path.PackageDirectory, package.Name, formFile.Name);

            if (!File.Exists(_resourcePath))
            {
                throw new Exception("Package resource already exists.");
            }

            FileInfo _fileInfo = await this.WriteSourceFileToDisk(_resourcePath, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"/{Constants.Href.PackageDirectory}/{package.Name.NormalizeForUrl()}";
            externalResource.ExternalPackageID = package.ID;

            await this.AddAsync(externalResource);
        }

        public async Task ConvertFilesToStandaloneResource(IFormFileCollection formFiles)
        {
            foreach (IFormFile formFile in formFiles)
            {
                await this.ConvertFileToStandaloneResource(formFile);
            }
        }

        /// <summary>
        /// Add standalone external resources that are not apart of any package.
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task ConvertFileToStandaloneResource(IFormFile formFile)
        {
            string _extension = Path.GetExtension(formFile.FileName);

            if (Constants.File.SupportedImageExtensions.Contains(_extension))
            {
                await this.ConvertMediaFileToStandaloneResource(formFile);
            }
            else if (_extension == ".js")
            {
                await this.ConvertJsFileToStandaloneResource(formFile);
            }
            else if (_extension == ".css")
            {
                await this.ConvertCssFileToStandaloneResource(formFile);
            }
            else
            {
                throw new Exception("Unsupported file type uploaded.");
            }
        }

        public async Task ConvertCssFileToStandaloneResource(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.CssDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"/composer-cms/css/{_fileInfo.Name}";

            await this.AddAsync(externalResource);
        }

        public async Task ConvertJsFileToStandaloneResource(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.JsDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"/composer-cms/js/{_fileInfo.Name}";

            await this.AddAsync(externalResource);
        }

        public async Task ConvertMediaFileToStandaloneResource(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.MediaDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"/composer-cms/media/{_fileInfo.Name}";

            await this.AddAsync(externalResource);
        }

        /// <summary>
        /// Writes the actual file represented by the external resource to the disk.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<FileInfo> WriteSourceFileToDisk(string path, IFormFile formFile)
        {
            using (FileStream fileStream = System.IO.File.Open(Path.Combine(path, formFile.Name), FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            string fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
            FileInfo _fileInfo = new FileInfo(fileName);

            return _fileInfo;
        }
    }
}