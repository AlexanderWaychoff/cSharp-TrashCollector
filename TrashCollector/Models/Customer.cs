using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public int City { get; set; }    //FK
        public int StateAbbreviation { get; set; }   //FK
        public int ZipCode { get; set; }    //FK
        public string Email { get; set; }
        public int IsOnVacation { get; set; }  //FK
        public int RequestedPickUpDay { get; set; } //FK
        public int ScheduledPickUpDay { get; set; } //FK
        public float MonthlyCharge { get; set; }
        public bool IsAdmin { get; set; }
    }
}