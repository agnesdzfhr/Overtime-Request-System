using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_TR_AccountRole")]
    public class AccountRole
    {
        [Key]
        public int AccountRole_ID { get; set; }
        public string Role_ID { get; set; }
        public string Account_ID { get; set; }
        [JsonIgnore]
        [ForeignKey("Role_ID")]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        [ForeignKey("Account_ID")]
        public virtual Account Account { get; set; }
        
    }
}
