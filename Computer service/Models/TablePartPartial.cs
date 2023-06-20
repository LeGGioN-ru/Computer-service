using System.Linq;

namespace Computer_service.Models
{
    public partial class Table_part
    {
        public int CountServices => TB_Services.Count;
        public int SumServices => TB_Services.Sum(x => x.Service.Service_price);
        public string IsApprove => Table_part_is_aprove ? "Да" : "Нет";

        public override string ToString()
        {
            string finalString = string.Empty;

            finalString += $"№Номер чека {Entry_number}\n";
            finalString += $"Услуги:\n";

            foreach (TB_Services tb_service in TB_Services)
                finalString += tb_service.ToString() + "\n";

            finalString += $"Сумма чека: {SumServices}\n";
            finalString += $"ИНН/КПП: 9102578105/715490681\n";

            return finalString;
        }
    }
}
