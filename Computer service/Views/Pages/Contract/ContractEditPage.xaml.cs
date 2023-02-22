using Computer_service.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace Computer_service.Views.Pages.Contract1
{
    public partial class ContractEditPage : Page
    {
        private Action updateGrid;
        private Contract contract;

        public ContractEditPage(Action action)
        {
            InitializeComponent();
            updateGrid = action;
            SetComboBoxes();
        }

        public ContractEditPage(Action action, Contract contract)
        {
            InitializeComponent();
            updateGrid = action;
            this.contract = contract;
            EditButton.Content = "Изменить";
            SetComboBoxes();
            BindingData(contract);
        }

        private void ButtonCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SetComboBoxes()
        {
            EmployeeComboBox.ItemsSource = App.Context.Employee.ToList();
            ClientCombobBox.ItemsSource = App.Context.Client.ToList();
        }

        private void BindingData(Contract contract)
        {
            EmployeeComboBox.SelectedItem = contract.Employee;
            ClientCombobBox.SelectedItem = contract.Client;
            DatePicker.SelectedDate = contract.Contract_date;
            DescriptionTextBox.Text = contract.Contract_discription;
        }

        private void EditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (IsContractDateNormal() == false)
                {
                    UIHelper.ShowException("добавить/изменить контракт, дата отстаёт от текущей более чем на год.");
                    return;
                }

                if (contract == null)
                    Add();
                else
                    Edit();

                App.Context.SaveChanges();
                updateGrid?.Invoke();
                NavigationService.GoBack();
            }
            catch (Exception)
            {
                if (contract == null)
                    UIHelper.ShowException("добавить контракт");
                else
                    UIHelper.ShowException("изменить контракт");
            }
        }

        private void Add()
        {
            App.Context.Contract.Add(new Contract() { Client = ClientCombobBox.SelectedItem as Client, Contract_date = DatePicker.SelectedDate.Value, Contract_discription = DescriptionTextBox.Text, Employee = EmployeeComboBox.SelectedItem as Employee });
        }

        private void Edit()
        {
            contract.Contract_discription = DescriptionTextBox.Text;
            contract.Contract_date = DatePicker.SelectedDate.Value;
            contract.Employee = EmployeeComboBox.SelectedItem as Employee;
            contract.Client = ClientCombobBox.SelectedItem as Client;
        }

        private bool IsContractDateNormal()
        {
            return (DateTime.Now - DatePicker.SelectedDate.Value).TotalDays < 365;
        }
    }
}
