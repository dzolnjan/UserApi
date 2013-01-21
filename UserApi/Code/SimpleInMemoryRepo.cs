using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserApi.Code
{
    public static class SimpleInMemoryRepo
    {
        public static IList<User> Users = new List<User>();
    }
}