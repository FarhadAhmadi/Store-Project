using Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Products
{
    public class Category : BaseEnitity
    {
        public string Name { get; set; }

        public virtual Category ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
