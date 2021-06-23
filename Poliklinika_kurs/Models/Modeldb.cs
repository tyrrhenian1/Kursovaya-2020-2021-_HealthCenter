namespace Poliklinika_kurs.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Modeldb : DbContext
    {
        public Modeldb()
            : base("name=Modeldb")
        {
        }

        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<HistoryDes> HistoryDes { get; set; }
        public virtual DbSet<Pacients> Pacients { get; set; }
        public virtual DbSet<RegistrationDoc> RegistrationDoc { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctors>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Doctors>()
                .Property(e => e.specialization)
                .IsUnicode(false);

            modelBuilder.Entity<Doctors>()
                .Property(e => e.expirience)
                .IsUnicode(false);

            modelBuilder.Entity<HistoryDes>()
                .Property(e => e.date)
                .IsUnicode(false);

            modelBuilder.Entity<HistoryDes>()
                .Property(e => e.doctor)
                .IsUnicode(false);

            modelBuilder.Entity<HistoryDes>()
                .Property(e => e.pacient)
                .IsUnicode(false);

            modelBuilder.Entity<HistoryDes>()
                .Property(e => e.verdict)
                .IsUnicode(false);

            modelBuilder.Entity<Pacients>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Pacients>()
                .Property(e => e.adress)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationDoc>()
                .Property(e => e.date)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationDoc>()
                .Property(e => e.time)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationDoc>()
                .Property(e => e.doctor)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationDoc>()
                .Property(e => e.pacient)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
