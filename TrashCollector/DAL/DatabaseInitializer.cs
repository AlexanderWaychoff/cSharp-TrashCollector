using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrashCollector.Models;

namespace TrashCollector.DAL
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var cities = new List<City>
            {
                new City { Name = "West Allis" },
                new City { Name = "New Berlin" },
                new City { Name = "Cleveland" }
            };
            var states = new List<StateAbbreviated>
            {
                new StateAbbreviated { TwoLetterAbbreviation = "WI" },
                new StateAbbreviated { TwoLetterAbbreviation = "OH" }
            };
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
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Alex", LastName = "Waychoff", StreetAddress = "901 s 72nd st", City = cities.FirstOrDefault(p => p.Name == "West Allis").CityID, }

            };
        }
    }
}