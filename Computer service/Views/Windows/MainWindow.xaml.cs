using Computer_service.Views;
using System.Windows;

namespace Computer_service
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizationPage());
        }
    }
}
