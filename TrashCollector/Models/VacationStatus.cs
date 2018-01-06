using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class VacationStatus
    {
        [Key]
        public int VacationStatusID { get; set; }
        public bool IsOnVacation { get; set; }
    }
}