﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("TB_M_Overtime")]
    public class Overtime
    {
        [Key]
        public string Overtime_ID { get; set; }
        public TimeSpan MaxOvertime { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
