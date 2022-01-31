using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_overtime_bonus")]
    public class OvertimeBonus
    {
        [Key]
        public string OvertimeBonusID { get; set; }
        public string Hour { get; set; }
        public float CommisionPct { get; set; }
    }
}
