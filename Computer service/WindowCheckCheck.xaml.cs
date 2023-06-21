using Computer_service.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;

namespace Computer_service
{
    public partial class WindowCheckCheck : System.Windows.Window
    {
        private Table_part _tablePart;
        private List<Check> _list;
        private BitmapImage _image;

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
            FinalSumTextBlock.Text = "Итог: " + table_Part.SumServices.ToString() + "₽";
            TextBlockCheck.Text = $"ТОВАРНЫЙ ЧЕК № ПК{_tablePart.Entry_number} от {_tablePart.Contract.Contract_date.ToShortDateString()}";

            try
            {
                if (_tablePart.TB_Services.Count > 7)
                {
                    MessageBox.Show("Слишком много услуг, печать qr кода не возможна");
                }
                else
                {
                    Bitmap qrCodeImage = GenerateQRCode($"{table_Part}");
                    _image = ConvertBitmapToBitmapImage(qrCodeImage);
                    QrCode.Source = _image;
                }


            }
            catch (Exception ex)
            {

            }
        }

        private Bitmap GenerateQRCode(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(80);

            return qrCodeImage;
        }

        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }


        private void Window_Initialized(object sender, EventArgs e)
        {
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string templatePath = $"{Environment.CurrentDirectory}\\CheckTemplate.docx";

                Word.Application wordApp = new Word.Application();

                wordApp.Visible = false;

                Document doc = wordApp.Documents.Add(templatePath);

                Bookmarks bookmarks = doc.Bookmarks;

                if (bookmarks.Exists("PCNumber"))
                {
                    Bookmark bookmark1 = bookmarks["PCNumber"];
                    bookmark1.Range.Text = $"{_tablePart.Entry_number}";
                }

                if (bookmarks.Exists("SumCheck"))
                {
                    Word.Bookmark bookmark2 = bookmarks["SumCheck"];
                    bookmark2.Range.Text = $"{_tablePart.SumServices}₽";
                }

                if (bookmarks.Exists("Date"))
                {
                    Bookmark bookmark2 = bookmarks["Date"];
                    bookmark2.Range.Text = $"{_tablePart.Contract.Contract_date.ToShortDateString()}";
                }

                if (_image != null)
                {
                    // Преобразование BitmapImage в Bitmap
                    Bitmap bitmap;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        BitmapEncoder encoder = new BmpBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(_image));
                        encoder.Save(stream);
                        bitmap = new Bitmap(stream);
                    }

                    int newWidth = 150; // Новая ширина изображения
                    int newHeight = 150; // Новая высота изображения
                    Bitmap resizedBitmap = new Bitmap(bitmap, newWidth, newHeight);

                    // Создание временного файла для сохранения уменьшенного изображения в формате PNG
                    string tempFilePath = Path.GetTempFileName();
                    tempFilePath = Path.ChangeExtension(tempFilePath, ".png");

                    // Сохранение уменьшенного изображения в формате PNG
                    resizedBitmap.Save(tempFilePath, ImageFormat.Png);
                    if (bookmarks.Exists("QRCODE"))
                    {
                        Bookmark bookmark = bookmarks["QRCODE"];

                        bookmark.Range.InlineShapes.AddPicture(tempFilePath);
                    }

                    File.Delete(tempFilePath);
                }

                if (bookmarks.Exists("TableServices"))
                {
                    Bookmark tableBookmark = bookmarks["TableServices"];

                    if (_list != null && _list.Count > 0)
                    {
                        int rowCount = _list.Count + 1;
                        int columnCount = 4;

                        Range tableRange = tableBookmark.Range;
                        Word.Table table = doc.Tables.Add(tableRange, rowCount, columnCount);

                        table.Cell(1, 1).Range.Text = "Наименование услуги";
                        table.Cell(1, 2).Range.Text = "Количество";
                        table.Cell(1, 3).Range.Text = "Цена";
                        table.Cell(1, 4).Range.Text = "Сумма";

                        table.Borders.Enable = 1;
                        table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

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

                    MessageBox.Show("Документ Word успешно сохранены.", "Успех");

                }
                else
                {
                    doc.Close(false);
                    wordApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании ворд документа: " + ex.Message);
            }
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




