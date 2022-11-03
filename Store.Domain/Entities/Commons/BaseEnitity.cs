using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities.Commons
{
    public abstract class BaseEnitity<Tkey>
    {
        public Tkey Id { get; set; } 
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemoveTime { get; set; }
    }
    public abstract class BaseEnitity : BaseEnitity<int>
    {

    }
}