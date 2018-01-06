using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class DayOfWeek
    {
        [Key]
        public int DayOfWeekID { get; set; }
        public string DayName { get; set; }
    }
}