using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Employee")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public float Salary { get; set; }
        public int WorkHourPerDay { get; set; }
        public int WorkDayPerMonth { get; set; }
        public string Department_ID { get; set; }
        public string Manager_ID { get; set; }
        public string Overtime_ID { get; set; }
        [JsonIgnore]
        [ForeignKey("Department_ID")]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        [ForeignKey("Manager_ID")]
        public virtual Employee Manager { get; set; }
        [JsonIgnore]
        //[ForeignKey("NIK")]
        public virtual ICollection<Employee> Employees { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<OvertimeSchedule> OvertimeSchedules { get; set; }

        [JsonIgnore]
        [ForeignKey("Overtime_ID")]
        public virtual Overtime Overtime { get; set; }



    }
    

    public enum Gender {
        Male = 0,
        Felame = 1
    }
}
