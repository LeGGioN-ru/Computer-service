using Computer_service.Models;
using Computer_service.Views.Pages.Services;
using Computer_service.Views.Pages.TablePart1;
using Computer_service.Views.Pages.Technic1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Computer_service.Views.Pages.Contract1
{
    public partial class ContractPage : Page
    {
        private List<Contract> _contracts = new List<Contract>();

        public ContractPage()
        {
            InitializeComponent();
            ContractDataGrid.ItemsSource = App.Context.Contract.ToList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            try
            {
                _contracts = App.Context.Contract.ToList();
                SearchData();
                SortData();
                ContractDataGrid.ItemsSource = null;
                ContractDataGrid.ItemsSource = _contracts;
            }
            catch (Exception)
            {
            }
        }

        private void SortData()
        {
            switch ((SortComboBox.SelectedItem as ComboBoxItem).Content)
            {
                case "По убыванию (фамилия)":
                    _contracts = _contracts.OrderByDescending(x => x.Client.Surname_client).ToList();
                    break;

                case "По возрастанию (фамилия)":
                    _contracts = _contracts.OrderBy(x => x.Client.Surname_client).ToList();
                    break;
            }
        }

        private void SearchData()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) == false)
                _contracts = _contracts.Where(x => x.Client.Surname_client.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }

        private void ButtonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractEditPage(UpdateDataGrid));
        }

        private void ButtonEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ContractDataGrid.SelectedItem is Contract contract)
                NavigationService.Navigate(new ContractEditPage(UpdateDataGrid, contract));
        }

        private void ButtonDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ContractDataGrid.SelectedItem is Contract contract)
            {
                if (UIHelper.GetConfirm("удалить контракт"))
                {
                    RemoveTableParts(contract);
                    App.Context.Contract.Remove(contract);
                    App.Context.SaveChanges();
                }
            }

            UpdateDataGrid();
        }

        private void RemoveTableParts(Contract contract)
        {
            List<Table_part> removeTableParts = App.Context.Table_part.Where(x => x.Contract.Contract_id == contract.Contract_id).ToList();
            List<TB_Services> removeTBparts = App.Context.TB_Services.Where(x => x.Table_part.Contract_id == contract.Contract_id).ToList();

            App.Context.Table_part.RemoveRange(removeTableParts);
            App.Context.TB_Services.RemoveRange(removeTBparts);
            App.Context.SaveChanges();
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (UIHelper.GetConfirm("выйти из аккаунта"))
                NavigationService.Navigate(new AuthorizationPage());
        }

        private void ButtonService_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());

        }
    }
}
