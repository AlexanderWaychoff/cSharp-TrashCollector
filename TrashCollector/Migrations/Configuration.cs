namespace TrashCollector.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TrashCollector.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrashCollector.Models.ApplicationDbContext context)
        {
            var cities = new List<City>
            {
                new City { Name = "West Allis" },
                new City { Name = "New Berlin" },
                new City { Name = "Cleveland" },
                new City { Name = "South Euclid" }
            };
            cities.ForEach(s => context.City.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var states = new List<StateAbbreviated>
            {
                new StateAbbreviated { TwoLetterAbbreviation = "WI" },
                new StateAbbreviated { TwoLetterAbbreviation = "OH" }
            };
            states.ForEach(s => context.StateAbbreviated.AddOrUpdate(p => p.TwoLetterAbbreviation, s));
            context.SaveChanges();

            var weekdays = new List<Models.DayOfWeek>
            {
                new Models.DayOfWeek { DayName = "Sunday" },
                new Models.DayOfWeek { DayName = "Monday" },
                new Models.DayOfWeek { DayName = "Tuesday" },
                new Models.DayOfWeek { DayName = "Wednesday" },
                new Models.DayOfWeek { DayName = "Thursday" },
                new Models.DayOfWeek { DayName = "Friday" },
                new Models.DayOfWeek { DayName = "Saturday" },
            };
            weekdays.ForEach(s => context.DayOfWeek.AddOrUpdate(p => p.DayName, s));
            context.SaveChanges();

            var zipCodes = new List<ZipCode>
            {
                new ZipCode { FiveDigitCode = 53214 },
                new ZipCode { FiveDigitCode = 53151 },
                //no zipcode entered for Cleveland OH
                new ZipCode { FiveDigitCode = 44121 }
            };
            zipCodes.ForEach(s => context.ZipCode.AddOrUpdate(p => p.FiveDigitCode, s));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Alex", LastName = "Waychoff", StreetAddress = "901 s 72nd st", City = "West Allis", StateAbbreviated = "WI", ZipCode = 53214, IsOnVacation = false, RequestedPickUpDay = Weekday.Thursday, ScheduledPickUpDay = Weekday.Thursday, MonthlyCharge = 30, IsAdmin = true},
                new Customer { FirstName = "Bob", LastName = "Greene", StreetAddress = "14347 W Coffee Rd", City = "New Berlin", StateAbbreviated = "WI", ZipCode = 53151, IsOnVacation = false, RequestedPickUpDay = Weekday.Monday, ScheduledPickUpDay = Weekday.Friday, MonthlyCharge = 30, IsAdmin = false},
                new Customer { FirstName = "Blob", LastName = "Spleen", StreetAddress = "12347 W Coffee Rd", City = "New Berlin", StateAbbreviated = "WI", ZipCode = 53151, IsOnVacation = false, RequestedPickUpDay = Weekday.Tuesday, ScheduledPickUpDay = Weekday.Wednesday, MonthlyCharge = 40, IsAdmin = false},
                new Customer { FirstName = "Wasabobi", LastName = "Macaroni", StreetAddress = "16147 W Coffee Rd", City = "New Berlin", StateAbbreviated = "WI", ZipCode = 53151, IsOnVacation = true, RequestedPickUpDay = Weekday.Saturday, ScheduledPickUpDay = Weekday.Saturday, MonthlyCharge = 45, IsAdmin = false}

            };
            customers.ForEach(s => context.Customer.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();
        }
    }
}
