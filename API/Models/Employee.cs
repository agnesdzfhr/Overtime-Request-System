using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_employee")]
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
        public string ManagerID { get; set; }
        public string DepartmentID { get; set; }
        public string OvertimeLimitID { get; set; }
        [JsonIgnore]
        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        [ForeignKey("ManagerID")]
        public virtual Employee Manager { get; set; }
        [JsonIgnore]
        //[ForeignKey("NIK")]
        public virtual ICollection<Employee> Employees { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<OvertimeRequest> OvertimeSchedules { get; set; }

        [JsonIgnore]
        [ForeignKey("OvertimeLimitID")]
        public virtual OvertimeLimit OvertimeLimit { get; set; }



    }
    public enum Gender {
        Male = 0,
        Felame = 1
    }
}
