using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Common;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<ProductForAdminDto> Execute(int page = 1, int pageSize = 20);

    }
    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDatabaseContext _Context;
        public GetProductForAdminService(IDatabaseContext context)
        {
            _Context = context;
        }

        public ResultDto<ProductForAdminDto> Execute(int page = 1, int pageSize = 20)
        {
            int RowsCount = 0;
            var products = _Context.Products
                    .Include(p => p.Category)
                    .ToPaged(page, pageSize, out RowsCount)
                    .Select(P => new ProductForAdminList_Dto
                    {
                        Id = P.Id,
                        Brand = P.Brand,
                        Description = P.Description,
                        Displayed = P.Displayed,
                        Name = P.Name,
                        Inventory = P.Inventory,
                        Price = P.Price,
                        Category = P.Category.Name
                    }).ToList();

            return new ResultDto<ProductForAdminDto>
            {
                Data = new ProductForAdminDto
                {
                    products = products,
                    PageSize = pageSize,
                    CurrentPage = page,
                    RowCount = RowsCount
                },
                IsSuccess = true,
                Message = ""
            };            
        }
    }
    public class ProductForAdminDto
    {
        public int RowCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ProductForAdminList_Dto> products { get; set; }
    }
    public class ProductForAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
    }
}