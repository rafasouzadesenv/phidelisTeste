using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using phidelisApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phidelisApi.Context
{
    public class PhidelisDbContext : DbContext
    {
       
        public PhidelisDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public virtual DbSet<Enrollment> Enrols { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
