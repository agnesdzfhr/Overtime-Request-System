using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class OvertimeFinanceApprovalVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string JobNote { get; set; }
        public float Salary { get; set; }
        public float TotalFee { get; set; }
    }
}
