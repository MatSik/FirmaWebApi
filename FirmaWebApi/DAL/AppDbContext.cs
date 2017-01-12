using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FirmaWebApi.Models;

namespace FirmaWebApi.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Worker> Workers { get; set; }
    }
}