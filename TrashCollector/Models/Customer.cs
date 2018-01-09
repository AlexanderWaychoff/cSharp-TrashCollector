using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public enum Weekday
    {
        Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
    }
    public class Customer
    {


        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }


        public string City { get; set; }


        public string StateAbbreviated { get; set; }


        public int ZipCode { get; set; }

        public bool IsOnVacation { get; set; }  
        public Weekday? RequestedPickUpDay { get; set; }
        public Weekday? ScheduledPickUpDay { get; set; }
        public float? MonthlyCharge { get; set; }
        public bool IsAdmin { get; set; }
        public int AccountID { get; set; }
        public Customer()
        {
            this.IsOnVacation = false;
            this.IsAdmin = false;
            this.AccountID = CustomerID;
        }
    }
}