using Computer_service.Models;
using Computer_service.Views.Pages.Contract1;
using Computer_service.Views.Pages.TablePart1;
using Computer_service.Views.Pages.Technic1;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Computer_service.Views.Pages.Services
{
    public partial class ServicePage : Page
    {
        private List<Service> services;

        public ServicePage()
        {
            InitializeComponent();
            DataGridServices.ItemsSource = App.Context.Service.ToList();
        }

        private void SearchData()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) == false)
                services = services.Where(x => x.Service_name.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void SortData()
        {
            switch ((SortComboBox.SelectedItem as ComboBoxItem).Content)
            {
                case "По убыванию (цена)":
                    services = services.OrderByDescending(x => x.Service_price).ToList();
                    break;

                case "По возрастанию (цена)":
                    services = services.OrderBy(x => x.Service_price).ToList();
                    break;
            }
        }

        private void UpdateDataGrid()
        {
            try
            {
                services = App.Context.Service.ToList();
                SearchData();
                SortData();
                DataGridServices.ItemsSource = null;
                DataGridServices.ItemsSource = services;
            }
            catch (Exception)
            {
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServiceEditPage(UpdateDataGrid));
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridServices.SelectedItem is Service service)
            {
                if (UIHelper.GetConfirm("удалить контракт"))
                {
                    Delete(service);
                }
            }

            UpdateDataGrid();
        }

        private void Delete(Service service)
        {
            foreach (TB_Services tb_service in App.Context.TB_Services)
            {
                if (tb_service.Service_id == service.Service_id)
                {
                    App.Context.TB_Services.Remove(tb_service);
                }
            }

            App.Context.Service.Remove(service);
            App.Context.SaveChanges();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridServices.SelectedItem is Service service)
                NavigationService.Navigate(new ServiceEditPage(UpdateDataGrid, service));
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
