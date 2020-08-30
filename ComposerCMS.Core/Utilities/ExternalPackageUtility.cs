using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
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
        private readonly ExternalResourceUtility _externalResourceUtil;

        public ExternalPackageUtility(ExternalResourceUtility externalResourceUtil, UserResolver userResolver) : base(userResolver)
        {
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
            string _packagePath = Path.Combine(Constants.Path.PackageDirectory, packageName);

            if (Directory.Exists(_packagePath))
            {
                throw new Exception("Package already exists locally please change the name and try again.");
            }

            ExternalPackage _package = new ExternalPackage()
            {
                Name = packageName
            };

            await this.AddAsync(_package);

            _ = Directory.CreateDirectory(_packagePath);

            foreach (IFormFile packageFile in packageResources)
            {
                await this._externalResourceUtil.ConvertFileToPackageResource(packageFile, _package);
            }
        }

        public async Task<List<ExternalResource>> ListPackageResources(Guid packageID)
        {
            return await this._externalResourceUtil.Table.Where(a => a.ExternalPackageID == packageID).ToListAsync();
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
                HTMLNode _node = this._externalResourceUtil.GenerateIncludeNode(resource);

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