using Store.Domain.Entities.Commons;

namespace Store.Domain.Entities.Products
{
    public class ProductFeaTures : BaseEnitity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }       
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}