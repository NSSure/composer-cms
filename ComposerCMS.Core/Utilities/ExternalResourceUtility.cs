using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.AspNetCore.Http;
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

        public async Task ConvertCssFileToExternalResource(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.CssDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Href = $"~/composer-cms/css/{_fileInfo.Name}";

            await this.AddAsync(externalResource);
        }

        public async Task ConvertJsFileToExternalResourcec(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.JsDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Href = $"~/composer-cms/js/{_fileInfo.Name}";

            await this.AddAsync(externalResource);
        }

        public async Task ConvertMediaFileToExternalResource(IFormFile formFile)
        {
            FileInfo _fileInfo = await this.WriteSourceFileToDisk(Constants.Path.MediaDirectory, formFile);

            ExternalResource externalResource = new ExternalResource();

            externalResource.Name = _fileInfo.Name;
            externalResource.Extension = _fileInfo.Extension;
            externalResource.Href = $"~/composer-cms/media/{_fileInfo.Name}";

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