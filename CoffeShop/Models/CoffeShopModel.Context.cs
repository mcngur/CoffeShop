﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeShop.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class coffemvcdbEntities : DbContext
    {
        public coffemvcdbEntities()
            : base("name=coffemvcdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<hakkimizda> hakkimizda { get; set; }
        public DbSet<kullanicilar> kullanicilar { get; set; }
        public DbSet<urunler> urunler { get; set; }
    }
}
