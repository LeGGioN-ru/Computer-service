using Computer_service.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace Computer_service.Views.Windows
{
    public partial class SmetaWindow : Window
    {
        private Table_part _tablePart;
        private List<Check> _list;

        public SmetaWindow(Table_part table_Part)
        {
            InitializeComponent();

            _list = new List<Check>();
            _tablePart = table_Part;
            foreach (TB_Services tB_Services in table_Part.TB_Services)
            {
                if (_list.Exists(x => x.Name == tB_Services.Service.Service_name))
                    continue;

                _list.Add(new Check(tB_Services, table_Part));
            }

            DataGridServices.ItemsSource = _list;

            TextBlockEmployee0.Text = $"{table_Part.Contract.Employee.Employee_surname} {table_Part.Contract.Employee.Employee_name} {table_Part.Contract.Employee.Employee_patronymic}";
            TextBlockEmployee1.Text = $"{table_Part.Contract.Employee.Employee_surname} {table_Part.Contract.Employee.Employee_name} {table_Part.Contract.Employee.Employee_patronymic}";
            TextBlockClient1.Text = $"{table_Part.Contract.Client.FullNameClient}";
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            string templatePath = $"{Environment.CurrentDirectory}\\SmetaTemplate.docx";

            try
            {
                Word.Application wordApp = new Word.Application();

                wordApp.Visible = false;

                Word.Document doc = wordApp.Documents.Add(templatePath);

                Word.Bookmarks bookmarks = doc.Bookmarks;

                if (bookmarks.Exists("Client"))
                {
                    Word.Bookmark bookmark1 = bookmarks["Client"];
                    bookmark1.Range.Text = $"{_tablePart.Contract.Client.FullNameClient}";
                }

                if (bookmarks.Exists("Employee1"))
                {
                    Word.Bookmark bookmark2 = bookmarks["Employee1"];
                    bookmark2.Range.Text = $"{_tablePart.Contract.Employee.FullName}";
                }

                if (bookmarks.Exists("Employee2"))
                {
                    Word.Bookmark bookmark2 = bookmarks["Employee2"];
                    bookmark2.Range.Text = $"{_tablePart.Contract.Employee.FullName}";
                }

                if (bookmarks.Exists("Services"))
                {
                    Word.Bookmark tableBookmark = bookmarks["Services"];

                    if (_list != null && _list.Count > 0)
                    {
                        int rowCount = _list.Count + 1;
                        int columnCount = 4;

                        Word.Range tableRange = tableBookmark.Range;
                        Word.Table table = doc.Tables.Add(tableRange, rowCount, columnCount);

                        table.Cell(1, 1).Range.Text = "Наименование услуги";
                        table.Cell(1, 2).Range.Text = "Количество";
                        table.Cell(1, 3).Range.Text = "Цена";
                        table.Cell(1, 4).Range.Text = "Сумма";

                        table.Borders.Enable = 1;
                        table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;

                        // Заполнение данных в таблице
                        for (int i = 0; i < _list.Count; i++)
                        {
                            Check check = _list[i];
                            table.Cell(i + 2, 1).Range.Text = check.Name;
                            table.Cell(i + 2, 2).Range.Text = check.Amount.ToString();
                            table.Cell(i + 2, 3).Range.Text = check.Price.ToString();
                            table.Cell(i + 2, 4).Range.Text = check.Sum.ToString();
                        }
                    }
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string outputPath = saveFileDialog.FileName;

                    doc.SaveAs2(outputPath);
                    doc.Close();

                    wordApp.Quit();

                    MessageBox.Show("Документ Word успешно создан и заполнен закладками.", "Успех");
                }
                else
                {
                    // Закрытие документа без сохранения
                    doc.Close(false);

                    // Завершение работы приложения Word
                    wordApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании документа Word: " + ex.Message, "Ошибка");
            }
        }
    }
}

