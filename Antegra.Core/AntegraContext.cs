using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Labote.Core
{
    public class AntegraContext : IdentityDbContext<AntegraUser, UserRole, Guid>
    {




        public AntegraContext()
        {

        }

        public AntegraContext(DbContextOptions<AntegraContext> options) : base(options)
        {

           
        }

        public DbSet<MenuModule> MenuModules { get; set; }
        public DbSet<UserMenuModule> UserMenuModules { get; set; }

        //public DbSet<JobScheduleTime> JobScheduleTimes { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AntegraDb;Integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //     builder.Entity<AntegraUser>()
            //.HasOne(p => p.AppInfo)
            //.WithMany(b => b.AntegraUsers).OnDelete(DeleteBehavior.Restrict);


        }

    }
}

