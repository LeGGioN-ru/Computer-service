using Computer_service.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Computer_service.Views.Pages.Client1
{
    public partial class ClientEditPage : Page
    {
        private Client currentClient;
        private Action updateGrid;

        public ClientEditPage(Action action)
        {
            InitializeComponent();
            updateGrid = action;
        }

        public ClientEditPage(Action action, Client client)
        {
            InitializeComponent();

            this.currentClient = client;
            SetData(client);
            EditButton.Content = "Изменить";
            updateGrid = action;
        }

        private void SetData(Client client)
        {
            AddressTextBox.Text = client.Address_client;
            SecondNameTextBox.Text = client.Surname_client;
            MiddleNameTextBox.Text = client.Patronymic_client;
            PhoneNumberTextBox.Text = client.Phone_number_client;
            NameTextBox.Text = client.Client_name;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsNullOrEmptyOrDigits(NameTextBox.Text) || IsNullOrEmptyOrDigits(SecondNameTextBox.Text) || IsNumberUncorrect(PhoneNumberTextBox.Text) || IsHasSymbols(AddressTextBox.Text))
                {
                    UIHelper.ShowException("добавить/изменить клиента (некорректные данные. Номер не формата:*(***)***-**-**) и/или адрес с символами и/или пустые поля)");
                    return;
                }

                if (IsExistAnotherClient())
                {
                    UIHelper.ShowException("добавить/изменить клиента (уже существует клиент с таким адресом и/или телефоном)");
                    return;
                }

                if (currentClient == null)
                    Add();
                else
                    Edit();

                App.Context.SaveChanges();
                updateGrid.Invoke();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                if (currentClient == null)
                    UIHelper.ShowException("добавить клиента");
                else
                    UIHelper.ShowException("изменить клиента");
            }
        }

        private void Edit()
        {
            currentClient.Address_client = AddressTextBox.Text;
            currentClient.Surname_client = SecondNameTextBox.Text;
            currentClient.Patronymic_client = MiddleNameTextBox.Text;
            currentClient.Phone_number_client = PhoneNumberTextBox.Text;
            currentClient.Client_name = NameTextBox.Text;
        }

        private void Add()
        {
            App.Context.Client.Add(new Client() { Address_client = AddressTextBox.Text, Client_name = NameTextBox.Text, Patronymic_client = MiddleNameTextBox.Text, Phone_number_client = PhoneNumberTextBox.Text, Surname_client = SecondNameTextBox.Text });
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private bool IsHasSymbols(string str)
        {
            return str.Any(c => char.IsSymbol(c) && c != '.' && c != ',');
        }

        private bool IsNumberUncorrect(string phoneNumber)
        {
            return phoneNumber.Count() != 15 || phoneNumber.Any(c => char.IsLetter(c));
        }

        private bool IsNullOrEmptyOrDigits(string str)
        {
            return string.IsNullOrEmpty(str) || str.Any(c => char.IsLetter(c) == false);
        }

        private bool IsExistAnotherClient()
        {
            Client client = App.Context.Client.FirstOrDefault(x => x.Address_client == AddressTextBox.Text || x.Phone_number_client == PhoneNumberTextBox.Text);

            if (currentClient != null && client.Id_Client == currentClient.Id_Client)
                client = null;

            return client != null;
        }
    }
}
