using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class ExternalResourceUtility : BaseRepository<ExternalResource>
    {
        public ExternalResourceUtility(UserResolver userResolver) : base(userResolver)
        {
  
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

        public async Task ConvertCssFileToExternalResource(IFormFile formFile, UploadParams uploadParams)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.CssDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"~/composer-cms/css/{_fileInfo.Name}";
            externalResource.ExternalPackageID = uploadParams.ExternalPackageID;

            await this.AddAsync(externalResource);
        }

        public async Task ConvertJsFileToExternalResourcec(IFormFile formFile, UploadParams uploadParams)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.JsDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"~/composer-cms/js/{_fileInfo.Name}";
            externalResource.ExternalPackageID = uploadParams.ExternalPackageID;

            await this.AddAsync(externalResource);
        }

        public async Task ConvertMediaFileToExternalResource(IFormFile formFile, UploadParams uploadParams)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.MediaDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Path = $"~/composer-cms/media/{_fileInfo.Name}";
            externalResource.ExternalPackageID = uploadParams.ExternalPackageID;

            await this.AddAsync(externalResource);
        }

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