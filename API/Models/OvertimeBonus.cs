using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_OvertimeBonus")]
    public class OvertimeBonus
    {
        [Key]
        public int OvertimeBonus_ID { get; set; }
        public float TotalBonus { get; set; }
        [JsonIgnore]
        public virtual ICollection<OvertimeSchedule> OvertimeSchedules { get; set; }
    }
}
