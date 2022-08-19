using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.ViewModel
{
    public class OvertimeSchedulesVM
    {
        public int OvertimeSchedule_ID { get; set; }
        public string NIK { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string DateStr { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobNote { get; set; }
        public string ApprovalStatus { get; set; }
        public string ManagerNote { get; set; }
        public static string GetApprovalStatus(int approvalStatus)
        {
            switch (approvalStatus)
            {
                case (int)Models.ManagerApprovalStatus.NeedApproval:
                    return "Need Approval";
                case (int)Models.ManagerApprovalStatus.Rejected:
                    return "Rejected";
                case (int)Models.ManagerApprovalStatus.Accepted:
                    return "Accepted";
                default:
                    return "Invalid Data For Gender";
            }
        }
    }
}
