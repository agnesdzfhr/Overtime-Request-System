using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class OvertimeHistoryVM
    {
        public DateTime Date { get; set; }
        public string DateStr { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TotalHour { get; set; }
        public string JobNote { get; set; }
        public string ApprovalStatus { get; set; }
        public string ManagerNote { get; set; }
    }
}
