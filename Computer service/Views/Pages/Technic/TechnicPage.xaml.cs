using Computer_service.Models;
using Computer_service.Views.Pages.Contract1;
using Computer_service.Views.Pages.TablePart1;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Computer_service.Views.Pages.Technic1
{
    public partial class TechnicPage : Page
    {
        private List<Technic> _technics = new List<Technic>();

        public TechnicPage()
        {
            InitializeComponent();

            SetFilters();
            TechnicDataGrid.ItemsSource = App.Context.Technic.ToList();
        }

        private void SetFilters()
        {
            List<Technic_type> technic_Types = App.Context.Technic_type.ToList();
            Technic_type technic_Type = new Technic_type() { Technic_type_name = "Без фильтра" };
            technic_Types.Insert(0, technic_Type);
            FilterComboBox.ItemsSource = technic_Types;
            FilterComboBox.SelectedIndex = 0;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void FilterTechnic()
        {
            string selectedItem = (FilterComboBox.SelectedItem as Technic_type).Technic_type_name;

            if (selectedItem == "Без фильтра")
                return;

            _technics = _technics.Where(x => x.Technic_type.Technic_type_name == selectedItem).ToList();
        }

        private void UpdateDataGrid()
        {
            _technics = App.Context.Technic.ToList();
            FilterTechnic();
            SearchTechnic();

            TechnicDataGrid.ItemsSource = null;
            TechnicDataGrid.ItemsSource = _technics;
        }

        private void SearchTechnic()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
                return;

            string normilizedSearchText = SearchTextBox.Text.ToLower();

            _technics = _technics.Where(x => x.RAM.FullName.ToLower().Contains(normilizedSearchText) || x.CPU.CPU_model.ToLower().Contains(normilizedSearchText) ||
              x.GPU.Name_GPU.ToLower().Contains(normilizedSearchText) || x.Motherboard.FullName.ToLower().Contains(normilizedSearchText) ||
              x.PSU.FullName.ToLower().Contains(normilizedSearchText)).ToList();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ButtonEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TechnicDataGrid.SelectedItem is Technic technic)
                NavigationService.Navigate(new TechnicEditPage(UpdateDataGrid, technic));
        }

        private void ButtonAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new TechnicEditPage(UpdateDataGrid));
        }

        private void ButtonRemove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TechnicDataGrid.SelectedItem is Technic technic)
            {
                if (UIHelper.GetConfirm("удалить технику"))
                {
                    RemoveTableParts(technic);
                    App.Context.Technic.Remove(technic);
                }
            }

            UpdateDataGrid();
        }
        private void RemoveTableParts(Technic technic)
        {
            List<Table_part> removeTableParts = App.Context.Table_part.Where(x => x.Technic_id == x.Technic_id).ToList();

            App.Context.Table_part.RemoveRange(removeTableParts);
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
    }
}
