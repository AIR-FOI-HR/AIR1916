﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FOIKnjiznicaWebServis.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FOIKnjiznicaEntities : DbContext
    {
        public FOIKnjiznicaEntities()
            : base("name=FOIKnjiznicaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Auth_Protocol> Auth_Protocol { get; set; }
        public virtual DbSet<Autori> Autori { get; set; }
        public virtual DbSet<Clanovi> Clanovi { get; set; }
        public virtual DbSet<Clanovi_Auth_Protocol> Clanovi_Auth_Protocol { get; set; }
        public virtual DbSet<Izdavaci> Izdavaci { get; set; }
        public virtual DbSet<Kategorije> Kategorije { get; set; }
        public virtual DbSet<Kopija_Publikacije> Kopija_Publikacije { get; set; }
        public virtual DbSet<Publikacije> Publikacije { get; set; }
        public virtual DbSet<Stanje_Publikacije> Stanje_Publikacije { get; set; }
        public virtual DbSet<Vrsta_Statusa> Vrsta_Statusa { get; set; }
    }
}
