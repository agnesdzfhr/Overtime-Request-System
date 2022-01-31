using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_finance_validation")]
    public class FinanceValidation
    {
        [Key]
        public int FinanceValidationID { get; set; }
        public float TotalFee { get; set; }
        public int OvertimeRequestID { get; set; }
        [JsonIgnore]
        [ForeignKey("OvertimeRequestID")]
        public virtual OvertimeRequest OvertimeRequest { get; set; }
        
    }
}
