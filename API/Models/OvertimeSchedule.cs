using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_OvertimeSchedule")]
    public class OvertimeSchedule
    {
        [Key]
        public int OvertimeSchedule_ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Overtime_ID { get; set; }
        public int OvertimeBonus_ID { get; set; }
        [JsonIgnore]
        public virtual ICollection<EmployeeOvertimeSchedule> EmployeeOvertimeSchedules { get; set; }
        [JsonIgnore]
        [ForeignKey("Overtime_ID")]
        public virtual Overtime Overtime { get; set; }
        
        [JsonIgnore]
        [ForeignKey("OvertimeBonus_ID")]
        public virtual OvertimeBonus OvertimeBonus { get; set; }
        
    }
}
