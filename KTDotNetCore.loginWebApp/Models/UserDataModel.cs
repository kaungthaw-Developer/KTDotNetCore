using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTDotNetCore.loginWebApp.Models
{
    [Table("User_Register")]
    public class UserDataModel
    {

        [Key]
        public string UserID { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PinCode { get; set; }
        public double TotalAmount { get; set; }
        public string CardNumber { get; set; }

        [Column("Active")]
        public bool IsActive { get; set; }

    }
}
