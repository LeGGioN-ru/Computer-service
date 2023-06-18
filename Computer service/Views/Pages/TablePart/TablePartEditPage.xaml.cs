using Computer_service.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Computer_service.Views.Pages.TablePart1
{
    public partial class TablePartEditPage : Page
    {
        private Action updateGrid;
        private Table_part table_part;

        public TablePartEditPage(Action action)
        {
            InitializeComponent();
            updateGrid = action;
            ServicesDataGrid.Visibility = System.Windows.Visibility.Collapsed;
            ButtonAdd.Visibility = System.Windows.Visibility.Collapsed;
            ButtonRemove.Visibility = System.Windows.Visibility.Collapsed;
            ServiceStackPanel.Visibility = System.Windows.Visibility.Collapsed;
            LoadData();
        }

        public TablePartEditPage(Action action, Table_part table_Part)
        {
            InitializeComponent();
            updateGrid = action;

            this.table_part = table_Part;
            EditButton.Content = "Изменить";
            LoadData();
            BindingData(table_Part);
            UpdateDataGridServices(table_Part);
        }

        private void LoadData()
        {
            TechnicComboBox.ItemsSource = App.Context.Technic.ToList();
            ContractComboBox.ItemsSource = App.Context.Contract.ToList();
            ServiceComboBox.ItemsSource = App.Context.Service.ToList();
        }

        private void BindingData(Table_part table_Part)
        {
            TechnicComboBox.SelectedItem = table_Part.Technic;
            ContractComboBox.SelectedItem = table_Part.Contract;
        }

        private void UpdateDataGridServices(Table_part table_Part)
        {
            ServicesDataGrid.ItemsSource = null;
            ServicesDataGrid.ItemsSource = table_Part.TB_Services.ToList();
        }

        private void ButtonCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (table_part == null)
                    Add();
                else
                    Edit();

                App.Context.SaveChanges();
                updateGrid?.Invoke();
                NavigationService.GoBack();
            }
            catch (Exception)
            {
                if (table_part == null)
                    UIHelper.ShowException("добавить табличную часть");
                else
                    UIHelper.ShowException("изменить табличную часть");
            }
        }

        private void Add()
        {
            App.Context.Table_part.Add(new Table_part()
            {
                Contract = ContractComboBox.SelectedItem as Contract,
                Technic = TechnicComboBox.SelectedItem as Technic,
            });
        }

        private void Edit()
        {
            table_part.Contract = ContractComboBox.SelectedItem as Contract;
            table_part.Technic = TechnicComboBox.SelectedItem as Technic;
        }

        private void ButtonAddService_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ServiceComboBox.SelectedItem is Service service)
            {
                App.Context.TB_Services.Add(new TB_Services() { Service = service, Table_part = table_part });
                UpdateDataGridServices(table_part);
                App.Context.SaveChanges();
            }
        }

        private void ButtonRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ServicesDataGrid.SelectedItem is TB_Services tp_service)
            {
                App.Context.TB_Services.Remove(tp_service);
                UpdateDataGridServices(table_part);
                App.Context.SaveChanges();
            }
        }
    }
}
