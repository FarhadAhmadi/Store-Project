using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Aplication.Interface.FacadPatterns;
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

namespace Store.Aplication.Services.Products.FacadPattern
{
    public class productFacad : iproductFacad
    {
        private readonly IDatabaseContext _Context;
        private readonly IHostingEnvironment _Environment;
        public productFacad(IDatabaseContext context , IHostingEnvironment environment)
        {
            _Context = context;
            _Environment = environment;
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

        private AddNewProductService _AddNewProductService;
        public AddNewProductService AddNewProductService
        {
            get
            {
                return _AddNewProductService = _AddNewProductService ?? new AddNewProductService(_Context, _Environment);
            }
        }


        private GetAllCategoriesService _GetAllCategoriesService ;
        public GetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _GetAllCategoriesService = _GetAllCategoriesService ?? new GetAllCategoriesService(_Context);
            }
        }

        private GetProductForAdminService _GetProductForAdminService;
        public GetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _GetProductForAdminService = _GetProductForAdminService ?? new GetProductForAdminService(_Context);

            }
        }
    }
}