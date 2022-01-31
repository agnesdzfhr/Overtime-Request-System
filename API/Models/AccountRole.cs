using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_account_role")]
    public class AccountRole
    {
        [Key]
        public int AccountRoleID { get; set; }
        public string RoleID { get; set; }
        public string AccountID { get; set; }
        [JsonIgnore]
        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
        
    }
}
