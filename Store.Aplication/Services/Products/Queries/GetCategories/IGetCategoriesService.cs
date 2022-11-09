using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Products.Queries.GetCategories
{
    public interface IGetCategoriesService
    {
        ResultDto<List<GetCategoriesDto>> Execute( int? parentId);
    }
    public class GetCategoriesService : IGetCategoriesService
    {
        private readonly IDatabaseContext _Context;
        public GetCategoriesService(IDatabaseContext context)
        {
            _Context = context;
        }
        public ResultDto<List<GetCategoriesDto>> Execute(int? parentId)
        {
            List<GetCategoriesDto>? result = _Context.Categories
                .Include(e => e.ParentCategory).
                Include(e => e.ChildCategories).
                Where(e => e.ParentCategoryId == parentId).
                ToList().
                Select(e => new GetCategoriesDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    HasChild = e.ChildCategories.Count() > 0 ? true : false,

                    Parent = e.ParentCategory != null ? new
                    ParentDto
                    {
                        Id = e.ParentCategory.Id,
                        Name = e.ParentCategory.Name,
                    }
                    : null
                }).ToList();
            return new ResultDto<List<GetCategoriesDto>>
            {
                Data = result,
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد"
            };
        }
    }
    public class GetCategoriesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public bool HasChild { get; set; }
        public ParentDto Parent { get; set; }
    }
    public class ParentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}