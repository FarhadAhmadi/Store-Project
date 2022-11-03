using Store.Aplication.Services.Products.Commands.AddNewCategory;
using Store.Aplication.Services.Products.Queries.GetCategories;
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
    }
}
