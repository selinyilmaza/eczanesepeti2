using eczanesepeti2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eczanesepeti2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Kisi>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Ilac> Ilac { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Eczane> Eczane { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<IlacEczane> IlacEczane { get; set; }
        public DbSet<Sepet> Sepet { get; set; }
        public DbSet<eczanesepeti2.Models.Il> Il { get; set; }



    }
}
