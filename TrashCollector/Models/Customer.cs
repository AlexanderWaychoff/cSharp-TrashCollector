using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class Customer
    {
        public enum Weekday
        {
            Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
        }


        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }

        public int CityID { get; set; }    //FK
        [ForeignKey("CityID")]
        public City City { get; set; }

        public int StateAbbreviatedID { get; set; }   //FK
        [ForeignKey("StateAbbreviationID")]
        public StateAbbreviated StateAbbreviated { get; set; }

        public int ZipCodeID { get; set; }    //FK
        [ForeignKey("ZipCodeID")]
        public ZipCode ZipCode { get; set; }

        public string Email { get; set; }
        public bool IsOnVacation { get; set; }  
        public Weekday? RequestedPickUpDay { get; set; } //FK
        public Weekday? ScheduledPickUpDay { get; set; } //FK
        public float? MonthlyCharge { get; set; }
        public bool IsAdmin { get; set; }
        public Customer()
        {
            this.IsOnVacation = false;
            this.IsAdmin = false;
        }
    }
}