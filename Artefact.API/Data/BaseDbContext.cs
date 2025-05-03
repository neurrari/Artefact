using Artefact.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Data
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) 
            : base(options) { }

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Caretaker> Caretakers { get; set; } = null!;
        public DbSet<Collection> Collections { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<DocumentType> DocumentTypes { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Exhibit> Exhibits { get; set; } = null!;
        public DbSet<Exhibition> Exhibitions { get; set; } = null!;
        public DbSet<Hall> Halls { get; set; } = null!;
        public DbSet<InventoryCypher> InventoryCyphers { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<Measure> Measures { get; set; } = null!;
        public DbSet<RecievingWay> RecievingWays { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Storage> Storages { get; set; } = null!;
        public DbSet<StoringType> StoringTypes { get; set; } = null!;
        public DbSet<Models.Task> Tasks { get; set; } = null!;
        public DbSet<Technique> Techniques { get; set; } = null!;



        public DbSet<MaterialExhibit> MaterialsExhibits { get; set; } = null!;
        public DbSet<StorageCollection> StoragesCollections { get; set; } = null!;
        public DbSet<ExhibitDocument> ExhibitsDocuments { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MaterialExhibit>().HasKey(me => new { me.IdExhibit, me.IdMaterial });
            modelBuilder.Entity<StorageCollection>().HasKey(sc => new { sc.IdCollection, sc.IdStorage });
            modelBuilder.Entity<ExhibitDocument>().HasKey(ed => new { ed.IdExhibit, ed.IdDocument });

            // Exhibit <-> Material
            modelBuilder.Entity<MaterialExhibit>()
                .HasOne(me => me.Exhibit)
                .WithMany(e => e.MaterialExhibits)
                .HasForeignKey(me => me.IdExhibit);
            modelBuilder.Entity<MaterialExhibit>()
                .HasOne(me => me.Material)
                .WithMany(m => m.MaterialExhibits)
                .HasForeignKey(me => me.IdMaterial);

            // Collection <-> Storage
            modelBuilder.Entity<StorageCollection>()
                .HasOne(sc => sc.Collection)
                .WithMany(c => c.StoragesCollections)
                .HasForeignKey(sc => sc.IdCollection);
            modelBuilder.Entity<StorageCollection>()
                .HasOne(sc => sc.Storage)
                .WithMany(s => s.StoragesCollections)
                .HasForeignKey(sc => sc.IdStorage);

            // Exhibit <-> Document
            modelBuilder.Entity<ExhibitDocument>()
                .HasOne(ed => ed.Exhibit)
                .WithMany(e => e.ExhibitDocuments)
                .HasForeignKey(ed => ed.IdExhibit);

            modelBuilder.Entity<ExhibitDocument>()
                .HasOne(ed => ed.Document)
                .WithMany(d => d.ExhibitDocuments)
                .HasForeignKey(ed => ed.IdDocument);

            // Default date constraints
            modelBuilder.Entity<Exhibit>()
                .Property(e => e.DateRecordCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Exhibit>()
                 .Property(e => e.DateRecordLastUpdated)
                 .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Account>()
                .Property(a => a.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Models.Task>()
                .Property(t => t.DateCreated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Document>()
                 .Property(d => d.DateCreated)
                 .HasDefaultValueSql("GETDATE()");
        }
    }
}