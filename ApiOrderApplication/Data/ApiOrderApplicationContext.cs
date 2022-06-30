using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiOrderApplication.Models;

namespace ApiOrderApplication.Data
{
    public class ApiOrderApplicationContext : DbContext
    {
        public ApiOrderApplicationContext (DbContextOptions<ApiOrderApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<ApiOrderApplication.Models.Order>? Order { get; set; }

        public DbSet<ApiOrderApplication.Models.Dessert>? Dessert { get; set; }

        public DbSet<ApiOrderApplication.Models.Dish>? Dish { get; set; }

        public DbSet<ApiOrderApplication.Models.Drink>? Drink { get; set; }

        public DbSet<ApiOrderApplication.Models.Entry>? Entry { get; set; }
    }
}
