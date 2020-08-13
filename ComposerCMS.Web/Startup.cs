using ComposerCMS.Core;
using ComposerCMS.Core.DAL;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Middleware;
using ComposerCMS.Core.Model;
using ComposerCMS.Core.Utilities;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComposerCMS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Constants.Configuration = configuration;
        } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Ensures content directories and other requirements before the main startup is executed.
            ComposerCMSApp.Init();

            // Services are all configured so now init the database.
            ComposerCMSContext.Initialize();

            services.AddDbContext<ComposerCMSContext>(options => options.UseNpgsql(Constants.Configuration.GetConnectionString("ComposerCMSContext")));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ComposerCMSContext>().AddDefaultTokenProviders();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors((options) =>
            {
                options.AddPolicy("CorsPolicy", (builder) =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddControllers();

            services.AddRazorPages().AddRazorPagesOptions((options) =>
            {
                Theme _currentTheme = ComposerCMSApp.Themes.FirstOrDefault(a => a.Key == ComposerCMSApp.Settings.ThemeKey);

                string _themePath = Path.Combine("pages", "themes", _currentTheme.Name);

                List<string> _pageOverrides = Directory.GetFiles($"{_themePath}", "*.cshtml", searchOption: SearchOption.AllDirectories).Select(a =>
                {
                    return a.Split("pages")[1].Replace('\\', '/').Replace(".cshtml", string.Empty).ToLower();
                })
                .ToList();

                ComposerCMSApp.SystemRoutes.ForEach((routeSection) =>
                {
                    routeSection.RouteItems.ForEach((routeItem) =>
                    {
                        string _themeOverride = _pageOverrides.FirstOrDefault(a => a.EndsWith(routeItem.PhysicalPath.ToLower()));

                        if (!string.IsNullOrEmpty(_themeOverride))
                        {
                            options.Conventions.AddPageRoute(_themeOverride, routeItem.VirutalPath);
                        }
                        else
                        {
                            options.Conventions.AddPageRoute(routeItem.PhysicalPath, routeItem.VirutalPath);
                        }
                    });
                });
            })
            .AddRazorRuntimeCompilation();

            services.Configure<TokenManagement>(Constants.Configuration.GetSection("TokenManagement"));

            var token = Constants.Configuration.GetSection("TokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization();

            services.AddTransient<LayoutUtility>();
            services.AddTransient<MenuUtility>();
            services.AddTransient<MenuItemUtility>();
            services.AddTransient<PageUtility>();
            services.AddTransient<PageVersionUtility>();
            services.AddTransient<PageScriptUtility>();
            services.AddTransient<FileUtility>();
            services.AddTransient<ExternalResourceUtility>();
            services.AddTransient<ActivityHistoryUtility>();
            services.AddTransient<SettingsUtility>();
            services.AddTransient<BlogUtility>();
            services.AddTransient<PostUtility>();
            services.AddTransient<RouteUtility>();

            services.AddTransient<UserResolver>();
            services.AddScoped<IAuthenticationService, TokenAuthenticationService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<UrlRewritingMiddleware>();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // Workaround: https://github.com/dotnet/aspnetcore/issues/13715
            app.Use((context, next) =>
            {
                context.SetEndpoint(null);
                return next();
            });

            app.UseRouting();

            app.UseEndpoints((endpoints) =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.Map(new PathString("/admin"), appMember =>
            {
                appMember.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            });
        }
    }
}
