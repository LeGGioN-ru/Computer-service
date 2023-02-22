using Computer_service.Models;
using System.Windows;

namespace Computer_service
{
    public partial class App : Application
    {
        public static ComputerServiceDBEntities Context = new ComputerServiceDBEntities();
    }
}
