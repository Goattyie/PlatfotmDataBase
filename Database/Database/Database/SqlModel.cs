using Microsoft.EntityFrameworkCore;
using Database.Model.Database.Tables;


namespace Database.Model.Database
{
    class SqlModel : DbContext
    {
        public SqlModel()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Project.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(a => new { a.Name }).IsUnique(true);
            modelBuilder.Entity<Card>()
                .HasIndex(a => new { a.Name }).IsUnique(true);
            modelBuilder.Entity<Client>()
                .HasIndex(a => new { a.Phone }).IsUnique(true);
            modelBuilder.Entity<Profile>()
               .HasIndex(a => new { a.Name }).IsUnique(true);
            modelBuilder.Entity<Deliver>()
               .HasIndex(a => new { a.Name }).IsUnique(true);
            modelBuilder.Entity<Availability>().HasIndex(a => new { a.ProductId, a.ProfileId }).IsUnique(true);

            //Ограничения на NOT NULL
            modelBuilder.Entity<Product>()
                .Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.SellCost).IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.DeliverCost).IsRequired();
            modelBuilder.Entity<Card>()
                .Property(c => c.Name).IsRequired();
            modelBuilder.Entity<Deliver>()
                .Property(d => d.Name).IsRequired();
            modelBuilder.Entity<DeliverProduct>()
                .Property(d => d.ProductId).IsRequired();
            modelBuilder.Entity<DeliverProduct>()
                .Property(d => d.DeliverId).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.ProductId).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.DeliverId).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.Count).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.OrderCost).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.DeliverCost).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.SummCost).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.CurrentCost).IsRequired();
            modelBuilder.Entity<OrderNode>()
                .Property(d => d.CurrentCount).IsRequired();
            modelBuilder.Entity<Profile>()
                .Property(d => d.Name).IsRequired();
            modelBuilder.Entity<Sell>()
                .Property(d => d.SellDate).IsRequired();
            modelBuilder.Entity<Sell>()
                .Property(d => d.Count).IsRequired();
            modelBuilder.Entity<Sell>()
                .Property(d => d.Profit).IsRequired();
        }

        public DbSet<Availability> Availability { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Deliver> Delivers { get; set; }
        public DbSet<DeliverProduct> DeliversProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderNode> OrderNodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Sell> Sells { get; set; }
    }
}
