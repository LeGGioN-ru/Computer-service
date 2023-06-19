using System.Linq;

namespace Computer_service.Models1
{
    public partial class Table_part
    {
        public int CountServices => TB_Services.Count;
        public int SumServices => TB_Services.Sum(x => x.Service.Service_price);

        public override string ToString()
        {
            string finalString = string.Empty;

            finalString += $"№Номер чека {Entry_number}\n";
            finalString += $"Услуги:\n";
            finalString += "_________\n";

            foreach (TB_Services tb_service in TB_Services)
                finalString += tb_service.ToString() + "\n";

            finalString += "_________\n";
            finalString += $"Количество услуг: {CountServices}\n";
            finalString += $"Сумма чека: {SumServices}\n";
            finalString += $"ИНН 1234567899\n";
            finalString += $"Адрес: Г.Серпухов ул. Ленина 110";

            return finalString;
        }
    }
}
