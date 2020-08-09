using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ComposerCMS.Core.Middleware
{
    public class UrlRewritingMiddleware
    {
        private readonly RequestDelegate _next;

        //Your constructor will have the dependencies needed for database access
        public UrlRewritingMiddleware(RequestDelegate next)
        {
            Console.WriteLine("[URL REWRITTING MIDDLEWARE INITIALIZED]");
            _next = next;
        }

        public async Task Invoke(HttpContext context, PageUtility pageUtility, RouteUtility routeUtility, SettingsUtility settingsUtility)
        {
            Console.WriteLine("[URL REWRITTING MIDDLEWARE INVOKED]: " + context.Request.Path.ToUriComponent().ToString());

            var path = context.Request.Path.ToUriComponent();

            Route _route = await routeUtility.FirstOrDefaultAsync(a => a.Url == path);

            if (_route != null)
            {
                if (path == "/")
                {
                    Settings _settings = await settingsUtility.Table.FirstOrDefaultAsync();

                    if (_settings.DefaultRouteID != default)
                    {
                        _route = await routeUtility.FirstOrDefaultAsync(a => a.ID == _settings.DefaultRouteID);
                        context.Request.Path = _route.Url;
                    }
                }
                else
                {
                    //Page page = await pageUtility.FirstOrDefaultAsync(a => a.Name == _route.OriginalEntityText);

                    //if (page != null)
                    //{
                    //    Console.WriteLine("[PAGE ID]: " + page.ID);
                    //    context.Request.Path = page.Path;
                    //}
                }

                string[] _segments = _route.Url.Split("/", StringSplitOptions.RemoveEmptyEntries);

                if (_segments.Length > 0)
                {
                    if (_segments[0] == Constants.Route.BlogBaseUrl)
                    {
                        if (_segments.Length == 2)
                        {
                            context.Items.Add("BlogID", _route.EntityID);
                        }
                        else
                        {
                            context.Items.Add("PostID", _route.EntityID);
                        }
                    }
                }
            }

            //Let the next middleware (MVC routing) handle the request
            //In case the path was updated, the MVC routing will see the updated path
            await _next.Invoke(context);
        }
    }
}