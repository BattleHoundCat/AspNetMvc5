using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace StreetASP.Models
{
    public class DBInitializer : DropCreateDatabaseAlways<MyContext>
    {
        protected override void Seed(MyContext context)
        {

            var country1 = new Country() { Name = "USA" };
            var country2 = new Country() { Name = "Germany" };
            var country3 = new Country() { Name = "Russia" };

            context.Countries.Add(country1);
            context.Countries.Add(country2);
            context.Countries.Add(country3);

            context.Streets.Add(new Street() { Country = country1, Name = "Central Avenue New York" });
            context.Streets.Add(new Street() { Country = country1, Name = "Delaware Avenue New York" });
            context.Streets.Add(new Street() { Country = country2, Name = "Legiendamm Berlin" });
            context.Streets.Add(new Street() { Country = country2, Name = "Unter den Linden Berlin" });
            context.Streets.Add(new Street() { Country = country3, Name = "Arbat Street Moscow" });
            context.Streets.Add(new Street() { Country = country3, Name = "Shabolovka Street Moscow" });
            base.Seed(context);
        }
    }
}