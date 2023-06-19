using Computer_service.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Computer_service.Views.Pages.Services
{
    public partial class ServiceEditPage : Page
    {
        private Action UpdateDatagrid;
        private Service _service;

        public ServiceEditPage(Action updateDatagrid)
        {
            InitializeComponent();
            UpdateDatagrid = updateDatagrid;
        }

        public ServiceEditPage(Action updateDatagrid, Service service)
        {
            InitializeComponent();
            UpdateDatagrid = updateDatagrid;
            _service = service;
            TextBoxDescription.Text = service.Service_description;
            TextBoxName.Text = service.Service_name;
            TextBoxPrice.Text = service.Service_price.ToString();
            ButtonAddEdit.Text = "Изменить";
        }

        private void ButtonAddEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Validate();

                if (_service == null)
                    Add();
                else
                    Edit();

                App.Context.SaveChanges();
                UpdateDatagrid?.Invoke();
                NavigationService.GoBack();
            }
            catch (Exception)
            {
                if (_service == null)
                    UIHelper.ShowException("добавить услугу");
                else
                    UIHelper.ShowException("изменить услугу");
            }
        }

        private void Add()
        {
            Service service = new Service()
            {
                Service_name = TextBoxName.Text,
                Service_description = TextBoxDescription.Text,
                Service_price = Convert.ToInt32(TextBoxPrice.Text),
            };

            App.Context.Service.Add(service);
        }

        private void Edit()
        {
            _service.Service_name = TextBoxName.Text;
            _service.Service_price = Convert.ToInt32(TextBoxPrice.Text);
            _service.Service_description = TextBoxDescription.Text;

            App.Context.SaveChanges();
        }

        private bool Validate()
        {
            if (float.TryParse(TextBoxPrice.Text, out float result))
            {
                if (result > 0)
                    return true;

                throw new Exception();
            }
            else
            {
                throw new Exception();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
