using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Overtime")]
    public class Overtime
    {
        [Key]
        public string Overtime_ID { get; set; }
        public string Type { get; set; }
        public float CommisionPct { get; set; }
        public DateTime MaxOvertime { get; set; }
        [JsonIgnore]
        public virtual ICollection<OvertimeSchedule> OvertimeSchedules { get; set; }
    }
}
