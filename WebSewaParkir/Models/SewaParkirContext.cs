using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebSewaParkir.Models;

namespace WebSewaParkir.Data
{
    public class SewaParkirContext : DbContext
    {
        // Konstruktor memanggil connection string di Web.config
        public SewaParkirContext() : base("MyDbConnection")
        {
        }

        // Representasi tabel di database
        public DbSet<User> Users { get; set; }

        // Jika kamu nanti punya tabel lain (misal Parkir, Kendaraan), tambahkan di sini:
        // public DbSet<Parkir> Parkirs { get; set; }
    }
}