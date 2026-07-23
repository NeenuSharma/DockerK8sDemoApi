using DockerK8sDemoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DockerK8sDemoApi.Context
{
    public class DataBaseDbContext : DbContext
    {
        public DataBaseDbContext(DbContextOptions<DataBaseDbContext> options) : base(options)
        {
        }
        public DbSet<Products> product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Database-first: map the entity to the existing [dbo].[Products] table
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("Products", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Description).HasColumnName("Description");
            });
        }
    }
}
