using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_FinanceValidation")]
    public class FinanceValidation
    {
        [Key]
        public int FinanceValidationID { get; set; }
        public string TotalFee { get; set; }
        public int OvertimeRequestID { get; set; }
        [JsonIgnore]
        [ForeignKey("OvertimeRequestID")]
        public virtual OvertimeRequest OvertimeRequest { get; set; }
        
    }
}
