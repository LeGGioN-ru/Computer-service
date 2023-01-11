using Computer_service.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Computer_service.Views.Pages.Technic1
{
    public partial class TechnicEditPage : Page
    {
        private Technic technic;
        private Action updateGrid;

        public TechnicEditPage(Action action)
        {
            InitializeComponent();
            updateGrid = action;
            SetComboBoxes();
        }

        public TechnicEditPage(Action action, Technic technic)
        {
            InitializeComponent();
            updateGrid = action;
            this.technic = technic;
            EditButton.Content = "Изменить";
            SetComboBoxes();
            BindingComboBoxes(technic);
        }

        private void SetComboBoxes()
        {
            CPUComboBox.ItemsSource = App.Context.CPU.ToList();
            MotherboardComboBox.ItemsSource = App.Context.Motherboard.ToList();
            PSUComboBox.ItemsSource = App.Context.PSU.ToList();
            RAMComboBox.ItemsSource = (App.Context.RAM.ToList());
            GPUComboBox.ItemsSource = (App.Context.GPU.ToList());
            TechnicTypeComboBox.ItemsSource = App.Context.Technic_type.ToList();
        }

        private void BindingComboBoxes(Technic technic)
        {
            CPUComboBox.SelectedItem = technic.CPU;
            MotherboardComboBox.SelectedItem = technic.Motherboard;
            PSUComboBox.SelectedItem = technic.PSU;
            RAMComboBox.SelectedItem = technic.RAM;
            GPUComboBox.SelectedItem = technic.GPU;
            TechnicTypeComboBox.SelectedItem = technic.Technic_type;
            TechnicNameTextBox.Text = technic.Technic_name;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (technic == null)
                    Add();
                else
                    Edit();

                App.Context.SaveChanges();
                updateGrid?.Invoke();
                NavigationService.GoBack();
            }
            catch (Exception)
            {
                if (technic == null)
                    UIHelper.ShowException("добавить технику");
                else
                    UIHelper.ShowException("изменить технику");
            }
        }

        private void Add()
        {
            App.Context.Technic.Add(new Technic()
            {
                RAM = RAMComboBox.SelectedItem as RAM,
                CPU = CPUComboBox.SelectedItem as CPU,
                GPU = GPUComboBox.SelectedItem as GPU,
                Motherboard = MotherboardComboBox.SelectedItem as Motherboard,
                PSU = PSUComboBox.SelectedItem as PSU,
                Technic_type = TechnicTypeComboBox.SelectedItem as Technic_type,
                Technic_name = TechnicNameTextBox.Text
            });
        }

        private void Edit()
        {
            technic.RAM = RAMComboBox.SelectedItem as RAM;
            technic.CPU = CPUComboBox.SelectedItem as CPU;
            technic.GPU = GPUComboBox.SelectedItem as GPU;
            technic.Motherboard = MotherboardComboBox.SelectedItem as Motherboard;
            technic.PSU = PSUComboBox.SelectedItem as PSU;
            technic.Technic_type = TechnicTypeComboBox.SelectedItem as Technic_type;
            technic.Technic_name = TechnicNameTextBox.Text;
        }
    }
}
