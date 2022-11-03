using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Aplication.Services.Products.Commands.AddNewCategory
{
    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly IDatabaseContext _Context;
        public AddNewCategoryService(IDatabaseContext context)
        {
            _Context = context;
        }
        public ResultDto Execute(int? parentId, string name)
        {

            if (string.IsNullOrEmpty(name))
            {
                new ResultDto
                {
                    IsSuccess = false,
                    Message = "نام را وارد کنید"
                };
            }

            Category category = new Category()
            {
                Name = name,
                ParentCategory = GetParentId(parentId)
            };
            _Context.Categories.Add(category);
            _Context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت ثبت شد"
            };
        }
        private Category GetParentId(int? parentId)
        {
            return _Context.Categories.Find(parentId);
        }
    }
}
