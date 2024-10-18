using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.Persistence
{
    public class AppDbContext: DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Domain.Entities.TaskStatus> TaskStatus { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<InteractionTypes> InteractionTypes { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<CampaignTypes> CampaignTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserID);
                entity.Property(t => t.UserID).ValueGeneratedOnAdd();

                entity.Property(c => c.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
                entity.Property(c => c.Email).IsRequired().HasColumnType("nvarchar").HasMaxLength(255);


                entity.HasMany<Tasks>(u => u.Tasks)
                .WithOne(Tasks => Tasks.User)
                .HasForeignKey(Tasks => Tasks.AssignedTo);

                entity.HasData(
                new Users { UserID = 1, Name = "Joe Done", Email = "jdone@marketing.com" },
                new Users { UserID = 2, Name = "Nill Amstrong", Email = "namstrong@marketing.com" },
                new Users { UserID = 3, Name = "Marlyn Morales", Email = "mmorales@marketing.com" },
                new Users { UserID = 4, Name = "Antony Orué", Email = "aorue@marketing.com" },
                new Users { UserID = 5, Name = "Jazmin Fernandez", Email = "jfernandez@marketing.com" }
                );

            });

            // TaskStatus
            modelBuilder.Entity<Domain.Entities.TaskStatus>(entity =>
            {
                entity.ToTable("TaskStatus");
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(c => c.Name).IsRequired().HasColumnType("varchar").HasMaxLength(25);

                entity.HasMany<Tasks>(Ts => Ts.Tasks)
                .WithOne(Tasks => Tasks.TaskStatus)
                .HasForeignKey(Tasks => Tasks.Status);

                entity.HasData(
                new Domain.Entities.TaskStatus { Id = 1, Name = "Pending"},
                new Domain.Entities.TaskStatus { Id = 2, Name = "In Progress"},
                new Domain.Entities.TaskStatus { Id = 3, Name = "Blocked"},
                new Domain.Entities.TaskStatus { Id = 4, Name = "Done"},
                new Domain.Entities.TaskStatus { Id = 5, Name = "Cancel"}
                );


            });


            // Tasks
            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("Tasks");
                entity.HasKey(e => e.TaskID);
                entity.Property(t => t.TaskID).ValueGeneratedOnAdd();

                entity.Property(c => c.TaskID).HasColumnType("char(36)");
                entity.Property(c => c.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(255);
                entity.Property(c => c.DueDate).IsRequired().HasColumnType("datetime");
                entity.Property(c => c.CreateDate).IsRequired().HasColumnType("datetime");
                entity.Property(c => c.UpdateDate).IsRequired().HasColumnType("datetime");



                entity.HasOne<Users>(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignedTo);

                entity.HasOne<Domain.Entities.TaskStatus>(t => t.TaskStatus)
                .WithMany(ts => ts.Tasks)
                .HasForeignKey(t => t.Status);


                entity.HasOne<Projects>(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID);

            });


            // Projects
            modelBuilder.Entity<Projects>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(e => e.ProjectID);
                entity.Property(t => t.ProjectID).ValueGeneratedOnAdd();

                entity.Property(c => c.ProjectID).HasColumnType("char(36)");
                entity.Property(c => c.ProjectName).IsRequired().HasColumnType("varchar").HasMaxLength(255);
                entity.Property(c => c.StartDate).IsRequired().HasColumnType("datetime");
                entity.Property(c => c.EndDate).IsRequired().HasColumnType("datetime");



                // a muchos
                entity.HasMany<Tasks>(p => p.Tasks)
                .WithOne(Tasks => Tasks.Project)
                .HasForeignKey(Tasks => Tasks.ProjectID);

                entity.HasMany<Interactions>(p => p.Interactions)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectID);


                // a uno
                entity.HasOne<CampaignTypes>(p => p.CampaignTypeObj)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.CampaignType);

                entity.HasOne<Clients>(p => p.Client)
                .WithMany(c => c.Projects)
                .HasForeignKey(p => p.ClientID);


            });


            //InteractionTypes
            modelBuilder.Entity<InteractionTypes>(entity =>
            {
                entity.ToTable("InteractionTypes");
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(c => c.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(25);

                entity.HasMany<Interactions>(it => it.Interactions)
                .WithOne(i => i.InteractionTypesObj)
                .HasForeignKey(i => i.InteractionType);

                entity.HasData(
                new InteractionTypes { Id = 1, Name = "Initial Meeting" },
                new InteractionTypes { Id = 2, Name = "Phone call" },
                new InteractionTypes { Id = 3, Name = "Email" },
                new InteractionTypes { Id = 4, Name = "Presentation of Results" }

                );

            });



            //Interactions
            modelBuilder.Entity<Interactions>(entity =>
            {
                entity.ToTable("Interactions");
                entity.HasKey(e => e.InteractionID);
                entity.Property(t => t.InteractionID).ValueGeneratedOnAdd();

                entity.Property(c => c.InteractionID).HasColumnType("char(36)");
                entity.Property(c => c.Date).IsRequired().HasColumnType("datetime");
                entity.Property(c => c.Notes).IsRequired().HasColumnType("varchar").HasMaxLength(255);



                entity.HasOne<Projects>(i => i.Project)
                .WithMany(p => p.Interactions)
                .HasForeignKey(i  => i.ProjectID);

                entity.HasOne<InteractionTypes>(i => i.InteractionTypesObj)
                .WithMany(it => it.Interactions)
                .HasForeignKey(i => i.InteractionType);


            });


            //Clients
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(e => e.ClientID);
                entity.Property(t => t.ClientID).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasColumnType("varchar").HasMaxLength(255);
                entity.Property(c => c.Email).IsRequired().HasColumnType("varchar").HasMaxLength(255);
                entity.Property(c => c.Phone).IsRequired().HasColumnType("varchar").HasMaxLength(255);
                entity.Property(c => c.Company).IsRequired().HasColumnType("varchar").HasMaxLength(100);
                entity.Property(c => c.Address).IsRequired().HasColumnType("varchar").HasMaxLength(255);


                entity.HasMany<Projects>(c => c.Projects)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientID);

                entity.HasData(
                new Clients {ClientID=1 ,Name = "Ivan Lapegna", Email = "ivanlapegna@yahoo.com", Company = "Carniceria El torito", Phone = "11 5752-8181", Address = "Berazategui C.19 2977" },
                new Clients {ClientID = 2, Name = "Franco Giordano", Email = "fgiordano@hotmail.com",Company = "FarmaPlus", Phone = "11 3626-6171", Address = "Ezpeleta C.Honduras 5580" },
                new Clients {ClientID = 3, Name = "Virginia Casero", Email = "virginiacasero@gmail.com", Company = "El Bazar Digital", Phone = "11 6589-3432", Address = "Florencio Varela C.Neuquén 1955" },
                new Clients {ClientID = 4, Name = "Marcos Negrotto", Email = "mnegrotto@gmail.com", Company = "Panaderia Estilo", Phone = "11 6798-1235", Address = "La Plata C.48 920 " },
                new Clients {ClientID = 5, Name = "Alex San German", Email = "ASanGerman@yahoo.com", Company = "DreamGym", Phone = "11 6752-4164", Address = "Belgrano C.Moldes 2139" }


                );
            });


            //CampaignTypes
            modelBuilder.Entity<CampaignTypes>(entity =>
            {
                entity.ToTable("CampaignTypes");
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(c => c.Name).IsRequired().HasColumnType("varchar").HasMaxLength(25);


                entity.HasMany<Projects>(c => c.Projects)
                .WithOne(p => p.CampaignTypeObj)
                .HasForeignKey(p => p.CampaignType);

                entity.HasData(
                new CampaignTypes { Id = 1, Name = "SEO" },
                new CampaignTypes { Id = 2, Name = "PPC" },
                new CampaignTypes { Id = 3, Name = "Social Media" },
                new CampaignTypes { Id = 4, Name = "Email Marketing" }
                );


            });




        }



    }
}
