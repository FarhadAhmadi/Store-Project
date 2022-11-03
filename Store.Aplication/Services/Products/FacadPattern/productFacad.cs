using Store.Aplication.Interface.Contexts;
using Store.Aplication.Interface.FacadPatterns;
using Store.Aplication.Services.Products.Commands.AddNewCategory;
using Store.Aplication.Services.Products.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Products.FacadPattern
{
    public class productFacad : iproductFacad
    {
        private readonly IDatabaseContext _Context;
        public productFacad(IDatabaseContext context)
        {
            _Context = context;
        }
        private AddNewCategoryService _AddNewCategoryService;
        public AddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _AddNewCategoryService = _AddNewCategoryService ?? new AddNewCategoryService(_Context);
            }
        }

        private GetCategoriesService _GetCategoriesService;
        public GetCategoriesService GetCategoriesService
        {
            get
            {
                return _GetCategoriesService = _GetCategoriesService ?? new GetCategoriesService(_Context);
            }
        }

    }
}
