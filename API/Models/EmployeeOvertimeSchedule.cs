using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_TR_EmployeeOvertimeSchedule")]
    public class EmployeeOvertimeSchedule
    {
        [Key]
        public int EmployeeOvertimeSchedule_ID { get; set; }
        public int OvertimeSchedule_ID { get; set; }
        public string NIK { get; set; }
        [JsonIgnore]
        [ForeignKey("NIK")]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        [ForeignKey("OvertimeSchedule_ID")]
        public virtual OvertimeSchedule OvertimeSchedule { get; set; }
        
    }
}
