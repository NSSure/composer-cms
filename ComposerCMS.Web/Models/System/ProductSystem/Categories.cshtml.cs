using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Entity.ProductSystem;
using ComposerCMS.Core.Utilities.ProductSystem;
using ComposerCMS.Core.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComposerCMS.Web.Models.System.ProductSystem
{
    public class CategoriesModel : PageModel
    {
        private readonly RouteUtility _routeUtil;
        private readonly CategoryUtility _productCategoryUtil;
        private readonly ProductUtility _productUtil;

        [BindProperty]
        public List<Category> ProductCategories { get; set; }

        [BindProperty]
        public Dictionary<Guid, Route> RouteMap { get; set; } = new Dictionary<Guid, Route>();

        public CategoriesModel(CategoryUtility productCategoryUtil, RouteUtility routeUtil, ProductUtility productUtil)
        {
            this._productCategoryUtil = productCategoryUtil;
            this._routeUtil = routeUtil;
            this._productUtil = productUtil;
        }

        public class t
        {
            public string query { get; set; } = "{id name}"; 
        }

        public async Task OnGet()
        {
            var t = this._productUtil.GraphQLTest(new Core.CoreSystem.GraphQL.GraphQLRequest()
            {
                Query = "query { product { id name categories { id name description } } }"
            });

            this.ProductCategories = this._productCategoryUtil.ListAll();

            if (this.ProductCategories.Count > 0)
            {
                // TODO: Fix this disaster.
                List<string> _categoryIDs = this.ProductCategories.Select(a => a.ID.ToString()).ToList();
                var routes = await this._routeUtil.ToListAsync(a => _categoryIDs.Contains(a.EntityID));

                for (int i = 0; i < _categoryIDs.Count; i++)
                {
                    this.RouteMap.Add(Guid.Parse(_categoryIDs[i]), routes[i]);
                }
            }
        }
    }
}