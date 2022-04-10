using Labote.Core.Constants;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Labote.Core
{
    public class LaboteContext : IdentityDbContext<LaboteUser, UserRole, Guid>
    {

        public IConfiguration Configuration { get; }



        public LaboteContext()
        {

        }

        public LaboteContext(DbContextOptions<LaboteContext> options) : base(options)
        {


        }

        public DbSet<MenuModule> MenuModules { get; set; }
        public DbSet<UserMenuModule> UserMenuModules { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<LaboratoryUser> LaboratoryUsers { get; set; }
        public DbSet<UserTopic> UserTopics { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<LaboratoryDevice> LaboratoryDevices { get; set; }

        public DbSet<DeviceResultValueType> DeviceResultValueTypes { get; set; }
        public DbSet<DeviceResultValueSampleUnitReference> DeviceResultValueSampleUnitReferences { get; set; }

        public DbSet<SampleExamination> SampleExaminations { get; set; }
        public DbSet<SampleExaminationDevice> SampleExaminationDevice { get; set; }
        public DbSet<SampleExaminationPriceCurrency> SampleExaminationPriceCurrencies { get; set; }
        public DbSet<SampleExaminationResultValueType> SampleExaminationResultValueTypes { get; set; }
        public DbSet<CurrentCustomer> CurrentCustomers { get; set; }
        public DbSet<CurrentCustomerBankAccountInfo> CurrentCustomerBankAccountInfos { get; set; }
        public DbSet<CurrentCustomerContactInfo> CurrentCustomerContactInfos { get; set; }
        public DbSet<SampleAccept> SampleAccepts { get; set; }
        public DbSet<SampleExaminationSampleAccept> SampleExaminationSampleAccepts { get; set; }
        public DbSet<AnalisysCreateRecord> AnalisysCreateRecords { get; set; }
        public DbSet<AnalisysRecord> AnalisysRecords { get; set; }
        public DbSet<AnalisysRecordDeviceValue> AnalisysRecordDeviceValues { get; set; }
        public DbSet<AnalisysRecordSampleExaminationResultValue> AnalisysRecordSampleExaminationResultValues { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

                string json = File.ReadAllText("appsettings.json");
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                string connectionString = jsonObj.ConnectionStrings.LaboteConnection.ToString();
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SampleExamination>()
       .HasOne(p => p.UserTopic)
       .WithMany(b => b.SampleExaminations).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SampleAccept>()
       .HasOne(p => p.ConfirmToGetLaboratoryUser)
       .WithMany(b => b.SampleAcceptForConfirms).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SampleAccept>()
       .HasOne(p => p.LaboteUser)
       .WithMany(b => b.SampleAccepts).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Laboratory>()
 .HasMany(p => p.SampleAccepts)
  .WithOne(b => b.Laboratory).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AnalisysRecordDeviceValue>()
     .HasOne(p => p.DeviceResultValueType)
      .WithMany(b => b.AnalisysRecordDeviceValues).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SampleExamination>()
      .HasOne(p => p.Laboratory)
       .WithMany(b => b.SampleExaminations).OnDelete(DeleteBehavior.Restrict);
        }






        public override bool Equals(object obj)
        {
            return obj is LaboteContext context &&
                   base.Equals(obj) &&
                   EqualityComparer<IConfiguration>.Default.Equals(Configuration, context.Configuration);
        }
    }

}

