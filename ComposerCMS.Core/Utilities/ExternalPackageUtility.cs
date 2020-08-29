using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using CMSSure.Builder.Utilities;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class ExternalPackageUtility : BaseRepository<ExternalPackage>
    {
        private readonly ExternalPackageUtility _externalPackageUtil;
        private readonly ExternalResourceUtility _externalResourceUtil;

        public ExternalPackageUtility(ExternalPackageUtility externalPackageUtil, ExternalResourceUtility externalResourceUtil, UserResolver userResolver) : base(userResolver)
        {
            this._externalPackageUtil = externalPackageUtil;
            this._externalResourceUtil = externalResourceUtil;
        }

        /// <summary>
        /// TODO: Need to create package folder and upload files into that folder.
        /// </summary>
        /// <param name="packageResources"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public async Task UploadLocalPackage(IFormFileCollection packageResources, string packageName)
        {
            if (Directory.Exists(Path.Combine(Constants.Path.PackageDirectory, packageName)))
            {
                throw new Exception("Package already exists locally please change the name and try again.");
            }

            ExternalPackage _package = new ExternalPackage()
            {
                Name = packageName
            };

            await this.AddAsync(_package);

            foreach (IFormFile packageFile in packageResources)
            {
                //FileInfo _fileInfo = await this._externalResourceUtil.ConvertFileToPackageResource(packageFile, packageName);

                //ExternalResource externalResource = new ExternalResource();

                //externalResource.Name = _fileInfo.Name;
                //externalResource.Extension = _fileInfo.Extension;
                //externalResource.Path = $"~/composer-cms/js/{_fileInfo.Name}";
                //externalResource.ExternalPackageID = uploadParams.ExternalPackageID;

                //await this.AddAsync(externalResource);
            }
        }

        public async Task<List<ExternalResource>> ListPackageResources(Guid packageID)
        {
            return null;
            //return await this._externalPackageUtil.Table.Where(a => a.ExternalPackageID == packageID).ToListAsync();
        }

        /// <summary>
        /// Generates the script and link elements for the package resources.
        /// </summary>
        /// <param name="packageID"></param>
        /// <returns></returns>
        public async Task<Dictionary<HTMLTag, List<HTMLNode>>> GenerateResourceIncludes(Guid packageID)
        {
            List<ExternalResource> _resources = await this.ComposerCMSContext.ExternalResource.Where(a => a.ExternalPackageID == packageID).ToListAsync();

            List<HTMLNode> _scriptNodes = new List<HTMLNode>();
            List<HTMLNode> _styleNodes = new List<HTMLNode>();

            foreach (ExternalResource resource in _resources)
            {
                //HTMLNode _node = this._externalPackageUtil.GenerateIncludeNode(resource);
                HTMLNode _node = null;

                if (_node.Tag == HTMLTag.Script)
                {
                    _scriptNodes.Add(_node);
                }
                else
                {
                    _styleNodes.Add(_node);
                }
            }

            return new Dictionary<HTMLTag, List<HTMLNode>>()
            {
                { HTMLTag.Script, _scriptNodes },
                { HTMLTag.Link, _styleNodes },
            };
        }
    }
}