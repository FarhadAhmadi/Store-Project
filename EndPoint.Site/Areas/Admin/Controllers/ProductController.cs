using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Aplication.Interface.FacadPatterns;
using Store.Aplication.Services.Products.FacadPattern;
using Store.Domain.Entities.Products;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly iproductFacad _Facad;
        public ProductController(iproductFacad facad)
        {
            _Facad = facad;
        }
        public IActionResult Index(int page = 0 , int pageSize = 20)
        {
            return View(_Facad.GetProductForAdminService.Execute(page,pageSize).Data);
        }
        [HttpGet]
        public IActionResult AddNewProduct()
        {

            ViewBag.Categories = new SelectList(_Facad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]

        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Feature> features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.images = images;
            request.Features = features;
            return Json(_Facad.AddNewProductService.Execute(request));
        }
    }
}
