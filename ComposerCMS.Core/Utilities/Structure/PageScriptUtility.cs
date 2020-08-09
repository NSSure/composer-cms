using ComposerCMS.Core.DAL;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class PageScriptUtility : BaseRepository<PageScript>
    {
        private readonly ExternalResourceUtility _externalResourceUtil;

        public PageScriptUtility(ExternalResourceUtility externalResourceUtil, UserResolver userResolver) : base(userResolver)
        {
            this._externalResourceUtil = externalResourceUtil;
        }

        public async Task<List<ExternalResource>> ListScriptResourcesByPage(Guid pageID)
        {
            List<Guid> _externalResourceIDs = await this.Table.Where(a => a.PageID == pageID).Select(a => a.ExternalResourceID).ToListAsync();
            List<ExternalResource> _scripts = await this._externalResourceUtil.ToListAsync(a => _externalResourceIDs.Contains(a.ID));
            return _scripts;
        }
    }
}