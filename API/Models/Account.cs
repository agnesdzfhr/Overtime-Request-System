using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_account")]
    public class Account
    {
        [Key]
        public string AccountID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        public string NIK { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        [JsonIgnore]
        public virtual ICollection<OTP> Otp { get; set; }
    }
}
