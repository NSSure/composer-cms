using System;
using System.Collections.Generic;
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
    public class ProductsModel : PageModel
    {
        private readonly RouteUtility _routeUtil;
        private readonly ProductCategoryUtility _productCategoryUtil;

        [BindProperty]
        public List<Category> ProductCategories { get; set; }

        [BindProperty]
        public Dictionary<Guid, Route> RouteMap { get; set; } = new Dictionary<Guid, Route>();

        public ProductsModel(ProductCategoryUtility productCategoryUtil, RouteUtility routeUtil)
        {
            this._productCategoryUtil = productCategoryUtil;
            this._routeUtil = routeUtil;
        }

        public async Task OnGet()
        {
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