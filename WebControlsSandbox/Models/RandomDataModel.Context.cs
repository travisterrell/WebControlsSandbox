﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TypeAhead.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WebControlsEntities : DbContext
    {
        public WebControlsEntities()
            : base("name=WebControlsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdTag> AdTags { get; set; }
        public virtual DbSet<AdUnit> AdUnits { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
    }
}
