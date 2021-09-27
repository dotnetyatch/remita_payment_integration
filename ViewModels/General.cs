using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaMiddleWare.ViewModels
{
    public class General
    {
    }

    public class BaseResponse<Object> where Object : class
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }
    }
}
