using System.Linq;

namespace Computer_service.Models
{
    public partial class Table_part
    {
        public int CountServices => TB_Services.Count;
        public int SumServices => TB_Services.Sum(x => x.Service.Service_price);
    }
}
