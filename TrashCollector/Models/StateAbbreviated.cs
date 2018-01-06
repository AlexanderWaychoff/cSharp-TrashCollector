using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class StateAbbreviated
    {
        [Key]
        public int StateAbbreviatedID { get; set; }
        public string TwoLetterAbbreviation { get; set; }
    }
}