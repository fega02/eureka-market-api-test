using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSMarketAPI.Utils
{
    public class ResponseBase
    {
        public int Code { get; set; }
        public string Message{ get; set; }
        public bool Success { get; set; }


    }
}
