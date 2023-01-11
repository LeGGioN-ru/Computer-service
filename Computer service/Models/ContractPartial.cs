namespace Computer_service.Models
{
    public partial class Contract
    {
        public string FullInfo => Contract_date.ToShortDateString() + " " + Client.Surname_client + " " + Contract_discription;
    }
}
