using Store.Aplication.Services.Products.Commands.AddNewCategory;
using Store.Aplication.Services.Products.Commands.AddNewProduct;
using Store.Aplication.Services.Products.Queries.GetAllCategories;
using Store.Aplication.Services.Products.Queries.GetCategories;
using Store.Aplication.Services.Products.Queries.GetProductForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Interface.FacadPatterns
{
    public interface iproductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        GetCategoriesService GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        GetAllCategoriesService GetAllCategoriesService { get; }
        /// <summary>
        /// دریافت لیست محصولات برای ادمین 
        /// </summary>
        GetProductForAdminService GetProductForAdminService { get; }
    }
}