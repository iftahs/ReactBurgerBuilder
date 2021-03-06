﻿using Microsoft.EntityFrameworkCore;
using ReactBurgerBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBurgerBuilder.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Clients { get; set; }
        public DbSet<Ingreident> Ingreidents { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
