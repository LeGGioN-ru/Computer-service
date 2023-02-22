using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_service.Models
{
    public partial class PSU
    {
        public string FullName => Brand.Brand_name + " мощь:" + Power_PSU;
    }
}
