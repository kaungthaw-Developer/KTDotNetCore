using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTDotNetCore.ATMWebApp.Models
{
    [Table("User_History")]
    public class UserHistoryModel
    {
        [Key]
        public string HistoryID {  get; set; }
        public string UserID {  get; set; }
        public DateTime LastUpdate {  get; set; }
        public double CashOutAmount { get; set; }

        public string Status { get; set; }
    }
}
