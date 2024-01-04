namespace KTDotNetCore.ATMWebApp.Models
{
    public class CashOutModel
    {
        public string UserID { get; set; }
        public string CardNumber { get; set; }
        public int PinCode { get; set; }
        public double MoneyAmount { get; set; }
        public bool NeedResult { get; set; }
    }
}
