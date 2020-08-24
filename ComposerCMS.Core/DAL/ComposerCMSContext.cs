using ComposerCMS.Core.DAL.Config;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Entity.Structure;
using ComposerCMS.Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComposerCMS.Core.DAL
{
    public class ComposerCMSContext : IdentityDbContext<IdentityUser>
    {
        public ComposerCMSContext()
        {

        }

        public ComposerCMSContext(DbContextOptions<ComposerCMSContext> options) : base(options)
        {

        }

        public DbSet<ActivityHistory> ActivityHistory { get; set; }
        public DbSet<ExternalPackage> ExternalResourcePackage { get; set; }
        public DbSet<ExternalResource> ExternalResource { get; set; }
        public DbSet<Layout> Layout { get; set; }
        public DbSet<LayoutScript> LayoutScript { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<PageVersion> PageVersion { get; set; }
        public DbSet<PageResource> PageScript { get; set; }
        public DbSet<PagePackage> PageResourcePackage { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(Constants.Configuration.GetConnectionString("ComposerCMSContext"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityHistoryConfig());
            modelBuilder.ApplyConfiguration(new ExternalPackageConfig());
            modelBuilder.ApplyConfiguration(new ExternalResourceConfig());
            modelBuilder.ApplyConfiguration(new LayoutConfig());
            modelBuilder.ApplyConfiguration(new LayoutScriptConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new MenuItemConfig());
            modelBuilder.ApplyConfiguration(new PageConfig());
            modelBuilder.ApplyConfiguration(new PageVersionConfig());
            modelBuilder.ApplyConfiguration(new PageResourceConfig());
            modelBuilder.ApplyConfiguration(new PageResourcePackageConfig());
            modelBuilder.ApplyConfiguration(new BlogConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new RouteConfig());
            modelBuilder.ApplyConfiguration(new MenuConfig());
            modelBuilder.ApplyConfiguration(new MenuItemConfig());
            modelBuilder.ApplyConfiguration(new SettingsConfig());

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Constants.Permission.Role.AdminID.ToString(), Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Constants.Permission.Role.EditorID.ToString(), Name = "Editor", NormalizedName = "Editor".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Constants.Permission.Role.AuthorID.ToString(), Name = "Author", NormalizedName = "Author".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Constants.Permission.Role.ContributorID.ToString(), Name = "Contributor", NormalizedName = "Contributor".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = Constants.Permission.Role.UserID.ToString(), Name = "User", NormalizedName = "User".ToUpper() });

            Guid _adminUserID = Guid.Parse("DE0FA044-1D5B-44D7-A93E-66598B2B7C84");

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = _adminUserID.ToString(),
                UserName = "Admin",
                NormalizedUserName = "Admin".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "admin"),
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = Constants.Permission.Role.AdminID.ToString(),
                UserId = _adminUserID.ToString(),
            });

            base.OnModelCreating(modelBuilder);
        }

        public static void Initialize()
        {
            //DbContextOptionsBuilder<ComposerCMSContext> optionsBuilder = new DbContextOptionsBuilder<ComposerCMSContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ComposerCMS;Trusted_Connection=True");

            using (ComposerCMSContext context = new ComposerCMSContext())
            {
                List<string> pendingMigrations = context.Database.GetPendingMigrations().ToList();

                if (pendingMigrations.Count != 0)
                {
                    context.Database.Migrate();

                    if (context.Database.GetAppliedMigrations().Count() == 1)
                    {
                        List<Page> _pages = new List<Page>();
                        List<PageVersion> _pageVersions = new List<PageVersion>();

                        List<Route> _routes = new List<Route>();

                        foreach (RouteSection routeSection in ComposerCMSApp.SystemRoutes)
                        {
                            foreach (RouteItem routeItem in routeSection.RouteItems)
                            {
                                // Add variable to appsettings to allow the user to change the "Pages" folder to something else.
                                string _systemPagePath = Path.Combine(Environment.CurrentDirectory, "Pages", $"{routeItem.PhysicalPath.Trim('/')}.cshtml");
                                string _fileName = Path.GetFileNameWithoutExtension(_systemPagePath);
                                string _pageContents = File.ReadAllText(_systemPagePath);

                                _pages.Add(new Page()
                                {
                                    ID = Guid.NewGuid(),
                                    Name = _fileName,
                                    Title = _fileName,
                                    Path = _systemPagePath,
                                    Content = _pageContents,
                                    IsSystemPage = true,
                                    IsAbstract = routeItem.IsAbstract,
                                    IsPublished = true,
                                    DateAdded = DateTime.UtcNow,
                                    DateLastPublished = DateTime.UtcNow,
                                    DateLastUpdated = DateTime.UtcNow,
                                });
                            }
                        }

                        context.Page.AddRange(_pages);
                        context.SaveChanges();

                        foreach (RouteSection routeSection in ComposerCMSApp.SystemRoutes)
                        {
                            foreach (RouteItem routeItem in routeSection.RouteItems)
                            {
                                string _systemPagePath = Path.Combine(Environment.CurrentDirectory, "Pages", $"{routeItem.PhysicalPath.Trim('/')}.cshtml");
                                string _fileName = Path.GetFileNameWithoutExtension(_systemPagePath);

                                // TODO: Refactor so we don't have to do this twice.
                                string _pageContents = File.ReadAllText(_systemPagePath);

                                Page _systemPage = _pages.FirstOrDefault(a => a.Path == _systemPagePath);

                                if (!routeItem.IsAbstract)
                                {
                                    _routes.Add(new Route()
                                    {
                                        EntityID = _systemPage.ID.ToString(),
                                        OriginalEntityText = _fileName,
                                        Url = routeItem.VirutalPath.VerifyForwardSlash(),
                                        DateAdded = DateTime.UtcNow
                                    });
                                }

                                _pageVersions.Add(new PageVersion()
                                {
                                    Content = _pageContents,
                                    DateAdded = DateTime.UtcNow,
                                    DateLastUpdated = DateTime.UtcNow,
                                    Name = _fileName,
                                    Title = _fileName,
                                    PageID = _systemPage.ID,
                                    Path = _systemPage.Path,
                                    UserIDAdded = default(Guid),
                                    UserIDLastUpdated = default(Guid)
                                });
                            }
                        }

                        context.Route.AddRange(_routes);
                        context.PageVersion.AddRange(_pageVersions);

                        context.SaveChanges();

                        context.Settings.Add(new Settings()
                        {
                            ID = Constants.Settings.ID,
                            Title = "Composer CMS",
                            MinimizeCss = true,
                            MinimizeJs = true,
                            DefaultRouteID = _routes.FirstOrDefault(a => a.Url == "/").ID,
                            ThemeKey = Constants.Theme.DefaultID
                        });

                        context.SaveChanges();

                        ComposerCMSApp.Settings = context.Settings.FirstOrDefault();

                        Menu _menu = new Menu()
                        {
                            DateAdded = DateTime.UtcNow,
                            Name = "Default"
                        };

                        context.Menu.Add(_menu);

                        context.MenuItem.Add(new MenuItem()
                        {
                            DateAdded = DateTime.UtcNow,
                            DisplayText = "Home",
                            RouteID = _routes.FirstOrDefault(a => a.Url == "/").ID,
                            MenuID = _menu.ID
                        });

                        context.MenuItem.Add(new MenuItem()
                        {
                            DateAdded = DateTime.UtcNow,
                            DisplayText = "Blogs",
                            RouteID = _routes.FirstOrDefault(a => a.Url == "/blogs").ID,
                            MenuID = _menu.ID
                        });

                        context.SaveChanges();
                    }
                }
                else
                {
                    ComposerCMSApp.Settings = context.Settings.FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// Drops and recreates the database. (Warning this DELETES any and all data in the existing database).
        /// </summary>
        /// <returns></returns>
        public async Task ReinitializeDatabase()
        {
            //DbContextOptionsBuilder<ComposerCMSContext> optionsBuilder = new DbContextOptionsBuilder<ComposerCMSContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ComposerCMS;Trusted_Connection=True");

            using (ComposerCMSContext context = new ComposerCMSContext())
            {
                // Delete the database.
                await context.Database.EnsureDeletedAsync();

                // Reinitialize the database from scratch.
                Initialize();
            }
        }
    }
}