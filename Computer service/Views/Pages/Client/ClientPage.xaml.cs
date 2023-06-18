using Computer_service.Models;
using Computer_service.Views.Pages.Client1;
using Computer_service.Views.Pages.Contract1;
using Computer_service.Views.Pages.Services;
using Computer_service.Views.Pages.TablePart1;
using Computer_service.Views.Pages.Technic1;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Computer_service.Views.Pages
{
    public partial class ClientPage : Page
    {
        private List<Client> _clients = new List<Client>();

        public ClientPage()
        {
            InitializeComponent();
            ClientsDataGrid.ItemsSource = App.Context.Client.ToList();
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
                _clients = App.Context.Client.ToList();
                SearchData();
                SortData();
                ClientsDataGrid.ItemsSource = null;
                ClientsDataGrid.ItemsSource = _clients;
            }
            catch (System.Exception)
            {
            }
        }

        private void SortData()
        {
            switch ((SortComboBox.SelectedItem as ComboBoxItem).Content)
            {
                case "По убыванию (фамилия)":
                    _clients = _clients.OrderByDescending(x => x.Surname_client).ToList();
                    break;

                case "По возрастанию (фамилия)":
                    _clients = _clients.OrderBy(x => x.Surname_client).ToList();
                    break;
            }
        }

        private void SearchData()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text) == false)
                _clients = _clients.Where(x => x.Surname_client.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
        }

        private void ButtonTecnic_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new TechnicPage());
        }

        private void ButtonClient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractPage());
        }

        private void ButtonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientEditPage(UpdateDataGrid));
        }

        private void ButtonEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client client)
                NavigationService.Navigate(new ClientEditPage(UpdateDataGrid, client));
        }

        private void ButtonRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem is Client client)
            {
                if (UIHelper.GetConfirm("удалить сотрудника"))
                {
                    RemoveTableParts(client);
                    RemoveContracts(client);
                    App.Context.Client.Remove(client);
                    App.Context.SaveChanges();
                }
            }

            UpdateDataGrid();
        }

        private void RemoveTableParts(Client client)
        {
            List<Table_part> removeTableParts = App.Context.Table_part.Where(x => x.Contract.Id_Client == client.Id_Client).ToList();
            List<TB_Services> services = App.Context.TB_Services.ToList();

            for (int i = 0; i < services.Count; i++)
            {
                for (int j = 0; j < removeTableParts.Count; j++)
                {
                    if (services[i].Entry_number == removeTableParts[j].Entry_number)
                    {
                        App.Context.TB_Services.Remove(services[i]);
                        App.Context.SaveChanges();
                    }
                }
            }


            App.Context.Table_part.RemoveRange(removeTableParts);
            App.Context.SaveChanges();
        }

        private void RemoveContracts(Client client)
        {
            List<Contract> removeTableParts = App.Context.Contract.Where(x => x.Id_Client == client.Id_Client).ToList();

            App.Context.Contract.RemoveRange(removeTableParts);
            App.Context.SaveChanges();
        }

        private void ButtonContract_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContractPage());
        }

        private void ButtonTablePart_Click(object sender, System.Windows.RoutedEventArgs e)
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
