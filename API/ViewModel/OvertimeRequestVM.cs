using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.ViewModel
{
    public class OvertimeRequestVM
    {
        public string NIK { get; set; }

        //public ICollection<OvertimeSchedule> OvertimeSchedules { get; set; }
        public List<OvertimeSchedule> OvertimeSchedules { get; set; }



        //public string Fullname { get; set; }
        //public string Role { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }
        ///*public TimeSpan StartTime { get; set; }
        //public TimeSpan EndTime { get; set; }*/
        //public string Note { get; set; }
    }
}
