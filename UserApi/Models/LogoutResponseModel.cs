using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserApi.Models
{
    public class LogoutResponseModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
    }
}