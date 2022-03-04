using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Entities
{
    public static class AccessRole
    {
        public const string Master = "Master";
        public const string Admin = "Admin";
        public const string Cliente = "Cliente";
        public const string MasterAndCliente = "Master,Cliente";
        public const string MasterAndAdmin= "Master,Admin";
    }
}
