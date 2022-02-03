using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_overtime_request")]
    public class OvertimeRequest
    {
        [Key]
        public int OvertimeRequestID { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TotalTime { get; set; }
        public string JobNote { get; set; }
        public string NIK { get; set; }

        [JsonIgnore]
        [ForeignKey("NIK")]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual FinanceValidation FinanceValidation { get; set; }

        [JsonIgnore]
        public virtual ManagerApproval ManagerApproval { get; set; }

    }
}
