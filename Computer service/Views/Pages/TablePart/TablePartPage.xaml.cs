using Computer_service.Models;
using Computer_service.Views.Pages.Contract1;
using Computer_service.Views.Pages.Technic1;
using Computer_service.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Computer_service.Views.Pages.TablePart1
{
    public partial class TablePartPage : Page
    {
        private List<Table_part> _tableParts = new List<Table_part>();

        public TablePartPage()
        {
            InitializeComponent();
            TablePartsDataGrid.ItemsSource = App.Context.Table_part.ToList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TablePartEditPage(UpdateDataGrid));
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TablePartsDataGrid.SelectedItem is Table_part table_part)
                NavigationService.Navigate(new TablePartEditPage(UpdateDataGrid, table_part));
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (TablePartsDataGrid.SelectedItem is Table_part table_part)
            {
                if (UIHelper.GetConfirm("удалить табличную часть"))
                {
                    App.Context.Table_part.Remove(table_part);
                    App.Context.SaveChanges();
                }
            }
        }

        private void UpdateDataGrid()
        {
            try
            {
                _tableParts = App.Context.Table_part.ToList();
                SearchData();
                SortData();
                TablePartsDataGrid.ItemsSource = null;
                TablePartsDataGrid.ItemsSource = _tableParts;
            }
            catch (Exception)
            {
            }
        }

        private void SearchData()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) == false)
                _tableParts = _tableParts.Where(x => x.Technic.Technic_name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }

        private void SortData()
        {
            switch ((SortComboBox.SelectedItem as ComboBoxItem).Content)
            {
                case "По убыванию (дата)":
                    _tableParts = _tableParts.OrderByDescending(x => x.Contract.Contract_date).ToList();
                    break;

                case "По возрастанию (дата)":
                    _tableParts = _tableParts.OrderBy(x => x.Contract.Contract_date).ToList();
                    break;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new TechnicPage());
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractPage());
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new TablePartPage());
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if (TablePartsDataGrid.SelectedItem is Table_part table_Part)
            {
                CheckWindow checkWindow = new CheckWindow(table_Part);
                checkWindow.Show();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (UIHelper.GetConfirm("выйти из аккаунта"))
                NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
