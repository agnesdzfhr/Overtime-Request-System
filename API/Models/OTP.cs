using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_OTP")]
    public class OTP
    {
        [Key]
        public int OtpID { get; set; }
        public int TokenOTP { get; set; }
        public DateTime ExpiredToken { get; set; }
        public bool? IsUsed { get; set; }
        public string AccountID { get; set; }
        [JsonIgnore]
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
    }
}