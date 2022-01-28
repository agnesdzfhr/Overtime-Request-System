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
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Note { get; set; }
        public bool IsApprove { get; set; }
        public float TotalBonus { get; set; }
        public string NIK { get; set; }

        [JsonIgnore]
        [ForeignKey("NIK")]
        public virtual Employee Employee { get; set; }
        
    }
}
