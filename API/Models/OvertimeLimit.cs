using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_overtime_limit")]
    public class OvertimeLimit
    {
        [Key]
        public string OvertimeLimitID { get; set; }
        public string Type { get; set; }
        public TimeSpan MaxOvertime { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
