using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Utility
{
    public class RouteUtility : BaseRepository<Route>
    {
        public RouteUtility(UserResolver userResolver) : base(userResolver)
        {

        }

        public async Task<bool> TryAddRoute<TEntityID>(TEntityID entityID, string originalEntityText, params string[] segments)
        {
            if (await this.Table.CountAsync(a => a.EntityID == entityID.ToString()) > 0)
            {
                return false;
            }

            string _normalizedUrl = await this.NormalizeUrl(originalEntityText, segments);

            await this.AddAsync(new Route()
            {
                EntityID = entityID.ToString(),
                Url = _normalizedUrl,
                OriginalEntityText = originalEntityText,
                DateAdded = DateTime.UtcNow
            });

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalEntityText"></param>
        /// <param name="segments"></param>
        /// <returns></returns>
        public async Task<string> NormalizeUrl(string originalEntityText, params string[] segments)
        {
            string _normalizedUrl = string.Empty;

            foreach (string segment in segments)
            {
                if (string.IsNullOrEmpty(_normalizedUrl))
                {
                    _normalizedUrl = $"{segment.NormalizeForUrl().TrimStart('/')}";
                }
                else
                {
                    _normalizedUrl = $"{_normalizedUrl}/{segment.NormalizeForUrl().TrimStart('/')}";
                }
            }

            _normalizedUrl = $"{_normalizedUrl}/{originalEntityText.NormalizeForUrl().TrimStart('/')}";

            if (await this.Table.CountAsync(a => a.Url == _normalizedUrl) > 0)
            {
                throw new Exception("Route already exists. Please try again.");
            }

            return _normalizedUrl.VerifyForwardSlash();
        }
    }
}