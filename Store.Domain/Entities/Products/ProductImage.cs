using Store.Domain.Entities.Commons;

namespace Store.Domain.Entities.Products
{
    public class ProductImage : BaseEnitity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public string Src { get; set; }
    }
}