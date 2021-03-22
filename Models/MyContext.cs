using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StreetASP.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Street> Streets { get; set; }
    }
}