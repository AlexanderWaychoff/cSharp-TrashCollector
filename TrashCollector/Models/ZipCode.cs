using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class ZipCode
    {
        [Key]
        public int ZipCodeID { get; set; }
        public int FiveDigitCode { get; set; }
    }
}