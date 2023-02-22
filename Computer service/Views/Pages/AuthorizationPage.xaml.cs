using Computer_service.Models;
using Computer_service.Views.Pages;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Computer_service.Views
{
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void ShowPasswordCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (IsInitialized == false)
                return;

            if (ShowPasswordCheckBox.IsChecked.Value)
            {
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordPasswordBox.Visibility = Visibility.Collapsed;
                PasswordTextBox.Text = PasswordPasswordBox.Password;
            }
            else
            {
                PasswordTextBox.Visibility = Visibility.Collapsed;
                PasswordPasswordBox.Visibility = Visibility.Visible;
                PasswordPasswordBox.Password = PasswordTextBox.Text;
            }
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = App.Context.Employee.FirstOrDefault(x => x.Login == LoginTextBox.Text && (x.Password == PasswordPasswordBox.Password || x.Password == PasswordTextBox.Text));

            if (employee != null)
                NavigationService.Navigate(new ClientPage());
            else
                UIHelper.ShowException("войти в аккаунт");
        }
    }
}
