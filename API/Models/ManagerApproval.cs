using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_ManagerApproval")]
    public class ManagerApproval
    {
        [Key]
        public int ManagerApprovalID { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

        public ManagerApprovalStatus ManagerApprovalStatus { get; set; }
        public int OvertimeRequestID { get; set; }

        [JsonIgnore]
        [ForeignKey("OvertimeRequestID")]
        public virtual OvertimeRequest OvertimeRequest { get; set; }


    }
    public enum ManagerApprovalStatus
    {
        Rejected = 0,
        Accepted = 1
    }
}
