using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Messege { get; set; }

    }
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Messege { get; set; }
        public T Data { get; set; }

    }
}
