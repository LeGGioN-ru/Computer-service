using Computer_service.Models;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Word = Microsoft.Office.Interop.Word;

namespace Computer_service.Views.Windows
{
    public partial class CheckWindow : System.Windows.Window
    {
        private Table_part table_part;

        public CheckWindow(Table_part table_Part)
        {
            InitializeComponent();
            this.table_part = table_Part;
            DataGridService.ItemsSource = table_Part.TB_Services.ToList();
            CheckNumberTextBlock.Text = "№Номер чека:" + table_Part.Entry_number.ToString();
            SumTextBlock.Text = "Общая сумма чека:" + table_Part.SumServices.ToString();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Word.Application wordApp = new Word.Application();

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Word Document (*.docx)|*.docx";
            saveDialog.Title = "Save Word Document";

            if (saveDialog.ShowDialog().Value)
            {
                Word.Document wordDoc = wordApp.Documents.Add();

                wordDoc.Content.Text = table_part.ToString();

                wordDoc.SaveAs2(saveDialog.FileName);

                wordDoc.Close();
                wordApp.Quit();
            }
        }
    }
}
