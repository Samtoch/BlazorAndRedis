﻿using BlazorAndRedis.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorAndRedis.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) 
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
