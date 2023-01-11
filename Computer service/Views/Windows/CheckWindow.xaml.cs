using Computer_service.Models;
using System.Linq;
using System.Windows;

namespace Computer_service.Views.Windows
{
    public partial class CheckWindow : Window
    {
        public CheckWindow(Table_part table_Part)
        {
            InitializeComponent();
            DataGridService.ItemsSource = table_Part.TB_Services.ToList();
            CheckNumberTextBlock.Text = "№Номер чека:" + table_Part.Entry_number.ToString();
            SumTextBlock.Text = "Общая сумма чека:" + table_Part.SumServices.ToString();
        }
    }
}
