using CMSSure.Builder.Enums;
using CMSSure.Builder.Models;
using CMSSure.Builder.Utilities;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class ExternalPackageUtility : BaseRepository<ExternalPackage>
    {
        private readonly ExternalResourceUtility _externalPackageUtil;

        public ExternalPackageUtility(ExternalResourceUtility externalPackageUtil, UserResolver userResolver) : base(userResolver)
        {
            this._externalPackageUtil = externalPackageUtil;
        }

        public async Task<List<ExternalResource>> ListPackageResources(Guid packageID)
        {
            return await this._externalPackageUtil.Table.Where(a => a.ExternalPackageID == packageID).ToListAsync();
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
                HTMLNode _node = this._externalPackageUtil.GenerateIncludeNode(resource);

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