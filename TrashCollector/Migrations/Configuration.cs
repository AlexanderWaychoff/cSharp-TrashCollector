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
                new Customer { FirstName = "Alex", LastName = "Waychoff", StreetAddress = "901 s 72nd st", City = cities.FirstOrDefault(p => p.Name == "West Allis").CityID, StateAbbreviation = states.FirstOrDefault(p => p.TwoLetterAbbreviation == "WI").StateAbbreviatedID, ZipCode = zipCodes.FirstOrDefault(p => p.FiveDigitCode == 53214).ZipCodeID, Email = "cabbagefat@gmail.com", IsOnVacation = false, RequestedPickUpDay = weekdays.FirstOrDefault(p => p.DayName == "Thursday").DayOfWeekID, ScheduledPickUpDay = weekdays.FirstOrDefault(p => p.DayName == "Thursday").DayOfWeekID, MonthlyCharge = 30, IsAdmin = true},
                new Customer { FirstName = "Bob", LastName = "Greene", StreetAddress = "15447 W Coffee Rd", City = cities.FirstOrDefault(p => p.Name == "New Berlin").CityID, StateAbbreviation = states.FirstOrDefault(p => p.TwoLetterAbbreviation == "WI").StateAbbreviatedID, ZipCode = zipCodes.FirstOrDefault(p => p.FiveDigitCode == 53151).ZipCodeID, Email = "lemongrab@gmail.com", IsOnVacation = false, RequestedPickUpDay = weekdays.FirstOrDefault(p => p.DayName == "Monday").DayOfWeekID, ScheduledPickUpDay = weekdays.FirstOrDefault(p => p.DayName == "Friday").DayOfWeekID, MonthlyCharge = 30, IsAdmin = true}

            };
            customers.ForEach(s => context.Customer.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();
        }
    }
}