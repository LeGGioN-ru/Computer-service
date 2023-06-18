using Computer_service.Models;
using Microsoft.Office.Interop.Word;
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

namespace Computer_service
{
    public partial class WindowCheckCheck : System.Windows.Window
    {
        private Table_part _tablePart;
        private List<Check> _list;

        public WindowCheckCheck(Table_part table_Part)
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
            TextBlockCheck.Text = $"ТОВАРНЫЙ ЧЕК № ПК{_tablePart.Entry_number} от {_tablePart.Contract.Contract_date.ToShortDateString()}";
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            string templatePath = $"{Environment.CurrentDirectory}\\CheckTemplate.docx";

            // Путь к создаваемому документу Word
            try
            {
                // Создание экземпляра приложения Word
                Word.Application wordApp = new Word.Application();

                // Отключение отображения окна Word
                wordApp.Visible = false;

                // Создание нового документа на основе шаблона
                Document doc = wordApp.Documents.Add(templatePath);

                // Получение объекта для работы с закладками
                Bookmarks bookmarks = doc.Bookmarks;

                // Заполнение значениями закладок
                if (bookmarks.Exists("PCNumber"))
                {
                    Bookmark bookmark1 = bookmarks["PCNumber"];
                    bookmark1.Range.Text = $"{_tablePart.Entry_number}";
                }

                if (bookmarks.Exists("Date"))
                {
                    Bookmark bookmark2 = bookmarks["Date"];
                    bookmark2.Range.Text = $"{_tablePart.Contract.Contract_date.ToShortDateString()}";
                }

                if (bookmarks.Exists("TableServices"))
                {
                    Bookmark tableBookmark = bookmarks["TableServices"];

                    // Проверка наличия данных в коллекции
                    if (_list != null && _list.Count > 0)
                    {
                        // Создание таблицы
                        int rowCount = _list.Count + 1; // Количество строк в таблице (данные + заголовок)
                        int columnCount = 4; // Количество столбцов

                        // Вставка таблицы вместо закладки
                        Range tableRange = tableBookmark.Range;
                        Word.Table table = doc.Tables.Add(tableRange, rowCount, columnCount);

                        // Заполнение заголовков столбцов
                        table.Cell(1, 1).Range.Text = "Наименование услуги";
                        table.Cell(1, 2).Range.Text = "Количество";
                        table.Cell(1, 3).Range.Text = "Цена";
                        table.Cell(1, 4).Range.Text = "Сумма";

                        table.Borders.Enable = 1; // Включение обводки
                        table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle; // Стиль обводки внутри таблицы
                        table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle; // Стиль обводки вокруг таблицы

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

                // Показ диалогового окна сохранения файла
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

    public class Check
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }

        public Check(TB_Services service, Table_part table_Part)
        {
            Name = service.Service.Service_name;
            Amount = table_Part.TB_Services.Count(x => x.Service.Service_name == Name);
            Price = service.Service.Service_price;
            Sum = Price * Amount;
        }
    }
}


