using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_service.Models
{
    public partial class Employee
    {
        public string FullName => Employee_surname + " " + Employee_name + ReturnPatronymicName();

        private string ReturnPatronymicName()
        {
            if (string.IsNullOrEmpty(Employee_patronymic))
                return string.Empty;
            else
                return " " + Employee_patronymic;
        }
    }
}
