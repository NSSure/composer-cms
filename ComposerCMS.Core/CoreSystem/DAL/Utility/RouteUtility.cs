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

        public async Task<bool> TryProcessRoute<TEntityID>(TEntityID entityID, string originalEntityText, params string[] segments)
        {
            string _normalizedUrl = await this.NormalizeUrl(originalEntityText, segments);

            Route _route = await this.Table.FirstOrDefaultAsync(a => a.EntityID == entityID.ToString());

            if (_route == null && await this.Table.CountAsync(a => a.Url == _normalizedUrl) > 0)
            {
                throw new Exception("Route already exists for the given url.");
            }

            Route _routeMatch = await this.Table.FirstOrDefaultAsync(a => a.Url == _normalizedUrl);

            if (_routeMatch != null && _routeMatch.EntityID != entityID.ToString())
            {
                throw new Exception("Route already exists for the given url under a different entity.");
            }

            if (_route != null)
            {
                if (_route.Url != _normalizedUrl)
                {
                    _route.Url = _normalizedUrl;
                }
                
                await this.UpdateAsync(_route);
            }
            else
            {
                await this.AddAsync(new Route()
                {
                    EntityID = entityID.ToString(),
                    Url = _normalizedUrl,
                    OriginalEntityText = originalEntityText
                });
            }

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