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
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Note { get; set; }
    }
}
