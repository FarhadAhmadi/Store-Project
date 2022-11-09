using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceStack;
using Store.Aplication.Interface.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Aplication.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDatabaseContext _context;
        private readonly IHostingEnvironment _environment;
        private IDatabaseContext context;

        public AddNewProductService(IDatabaseContext context)
        {
            this.context = context;
        }

        public AddNewProductService(IDatabaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }


        public ResultDto Execute(RequestAddNewProductDto request)
        {

            try
            {

                var category = _context.Categories.Find(request.CategoryId);

                Product product = new Product()
                {
                    Brand = request.Brand,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price,
                    Inventory = request.Inventory,
                    Category = category,
                    Displayed = request.Displayed,
                };
                _context.Products.Add(product);

                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in request.images)
                {
                    var uploadedResult = UploadFile(item);
                    productImages.Add(new ProductImage()
                    {
                        Product = product,
                        Src = uploadedResult.FileNameAddress,
                    });
                }

                _context.ProductImages.AddRange(productImages);


                List<ProductFeaTures> productFeatures = new List<ProductFeaTures>();
                foreach (var item in request.Features)
                {
                    productFeatures.Add(new ProductFeaTures
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product,
                    });
                }
                _context.ProductFeaTures.AddRange(productFeatures);

                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت به محصولات فروشگاه اضافه شد",
                };
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد ",
                };
            }

        }


        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }
}
public class UploadDto
{
    public long Id { get; set; }
    public bool Status { get; set; }
    public string FileNameAddress { get; set; }
}


public class RequestAddNewProductDto
{
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Inventory { get; set; }
    public int CategoryId { get; set; }
    public bool Displayed { get; set; }
    public List<IFormFile> images { get; set; }
    public List<AddNewProduct_Feature> Features { get; set; }
}
public class AddNewProduct_Feature
{
    public string DisplayName { get; set; }
    public string Value { get; set; }
}

