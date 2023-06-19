using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_service.Models1
{
    public partial class RAM
    {
        public string FullName => Brand.Brand_name + " размер памяти:" + RAM_volume;
    }
}
