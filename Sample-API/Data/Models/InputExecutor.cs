using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Data.Models
{
    public class InputExecutor
    {
        public string NameModule { get; set; }
        public List<object> ParamsModule { get; set; }
        public List<object> ParamsStart { get; set; }
    }
}
