namespace Computer_service.Models
{
    public partial class Client
    {
        public string FullNameClient => Surname_client + " " + Client_name + ReturnPatronymicName();


        private string ReturnPatronymicName()
        {
            if (string.IsNullOrEmpty(Patronymic_client))
                return string.Empty;
            else
                return " " + Patronymic_client;
        }
    }
}
