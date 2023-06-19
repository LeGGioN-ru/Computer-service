namespace Computer_service.Models1
{
    public partial class Contract
    {
        public string FullInfo => Contract_date.ToShortDateString() + " " + Client.Surname_client + " " + Contract_discription;
    }
}
