using Microsoft.AspNetCore.Mvc;
using Store.Aplication.Interface.FacadPatterns;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly iproductFacad _Facad;
        public CategoriesController(iproductFacad facad)
        {
            _Facad = facad;
        }

        public IActionResult Index(int? parentId)
        {
            return View(_Facad.GetCategoriesService.Execute(parentId).Data);
        }
        [HttpGet]
        public IActionResult AddNewCategory(int? parentId)
        {
            ViewBag.ParentId = parentId;
            return View();
        }
        [HttpPost]
        public IActionResult AddNewCategory(int? parentId, string name)
        {
            return Json(_Facad.AddNewCategoryService.Execute(parentId, name));
        }
    }
}
