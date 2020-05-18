using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DatabaseConnector
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Scientist> Scientists { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QN9HQ65\\SQLEXPRESS;Database=practicedb;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=practicedb;Trusted_Connection=True;");
        }
        //DESKTOP-QN9HQ65\SQLEXPRESS

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScientistConference>().HasKey(t => new { t.ScientistId, t.ConferenceId });

            modelBuilder.Entity<ScientistConference>()
                .HasOne(sc => sc.Scientist)
                .WithMany(s => s.ScientistConference)
                .HasForeignKey(sc => sc.ScientistId);

            modelBuilder.Entity<ScientistConference>()
                .HasOne(sc => sc.Conference)
                .WithMany(c => c.ScientistConference)
                .HasForeignKey(sc => sc.ConferenceId);

            modelBuilder.Entity<ScientistOrganization>().HasKey(t => new { t.ScientistId, t.OrganizationId });

            modelBuilder.Entity<ScientistOrganization>()
                .HasOne(sc => sc.Scientist)
                .WithMany(s => s.ScientistOrganization)
                .HasForeignKey(sc => sc.ScientistId);

            modelBuilder.Entity<ScientistOrganization>()
                .HasOne(sc => sc.Organization)
                .WithMany(s => s.ScientistOrganization)
                .HasForeignKey(sc => sc.OrganizationId);

            modelBuilder.Entity<Role>()
                .HasIndex(u => u.Name)
                .IsUnique();

            modelBuilder.Entity<Country>()
                .HasIndex(u => u.CountryName)
                .IsUnique();

            modelBuilder.Entity<Location>()
                .HasIndex(u => u.LocationName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<Organization>()
                .HasIndex(u => u.OrganizationName)
                .IsUnique();

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "ADMIN" },
                new Role() { Id = 2, Name = "USER" }
                );

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, UserName = "Иван Иванович Иванов", Login = "admin", Password = "admin", RoleId = 1 },
                new { Id = 2, UserName = "Василий Васильевич Васильев", Login = "operator", Password = "operator", RoleId = 2 }
                );

            modelBuilder.Entity<Country>().HasData(
                new { Id = 1, CountryName = "Россия"},
                new { Id = 2, CountryName = "США" }
                );

            modelBuilder.Entity<Location>().HasData(
                new { Id = 1, LocationName = "Moscow City", LocationDescription = "Самый центр Москвы", CountryId = 1 },
                new { Id = 2, LocationName = "Brighton Beach", LocationDescription = "Русский район в Нью-Йорке", CountryId = 2 }
                );

            modelBuilder.Entity<Scientist>().HasData(
                new { Id = 1, Name = "Дмитрий", LastName = "Менделеев", CountryId = 1 },
                new { Id = 2, Name = "Абрахам", LastName = "Маслоу", CountryId = 2 }
                );

            modelBuilder.Entity<Report>().HasData(
                new { Id = 1, ReportName = "Таблица Менделеева", Text = "Периоди́ческая система химических элементов", ReportDate = new DateTime(2022, 8, 16), IsPublished = false, ScientistId = 1 },
                new { Id = 2, ReportName = "Мотивация и личность", Text = "Книга, несомненно, представляет интерес для читателя, особенно для приверженцев гуманистической психологии",
                ReportDate = new DateTime(1999, 5, 21), IsPublished = true, ScientistId = 2}
                );

            modelBuilder.Entity<Organization>().HasData(
                new { Id = 1, OrganizationName = "Dow Chemical" },
                new { Id = 2, OrganizationName = "Гарвардский университет" }
                );

            modelBuilder.Entity<Conference>().HasData(
                new { Id = 1, ConferenceName = "Конференция о таблице Менделеева", ConferenceDescription = "Конференция об открытии таблицы Менделеева", StartOfConference = new DateTime(2024, 3, 12),
                LocationId = 1},
                new { Id = 2, ConferenceName = "Конференция о психологии", ConferenceDescription = "Дебаты о психологии", StartOfConference = new DateTime(1954, 7, 9), LocationId = 2}
                );

            modelBuilder.Entity<ScientistConference>().HasData(
                new { ScientistId = 1, ConferenceId = 1},
                new { ScientistId = 2, ConferenceId = 1},
                new { ScientistId = 2, ConferenceId = 2}
                );

            modelBuilder.Entity<ScientistOrganization>().HasData(
                new { ScientistId = 1, OrganizationId = 1},
                new { ScientistId = 1, OrganizationId = 2},
                new { ScientistId = 2, OrganizationId = 2}
                );



            base.OnModelCreating(modelBuilder);
        }


    }
}
