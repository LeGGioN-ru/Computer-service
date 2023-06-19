using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_service.Models
{
    public partial class TB_Services
    {
        public override string ToString()
        {
            return $"{Service.Service_name} {Service.Service_price}₽";
        }
    }
}
