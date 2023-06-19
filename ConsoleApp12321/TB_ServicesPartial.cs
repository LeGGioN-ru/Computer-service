using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_service.Models1
{
    public partial class TB_Services
    {
        public override string ToString()
        {
            return $"Наименование услуги: {Service.Service_name}. Цена: {Service.Service_price}.";
        }
    }
}
