using Inventory.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace Inventory.DAL
{
    public class BowlContext :DbContext
    {
        public BowlContext() : base("BowlContext")
        {
        }


        public DbSet<Bowl> Bowls { get; set; }
        public DbSet<Bias> Biases { get; set; }
        public DbSet<BowlSize> Bowlsizes { get; set; }

        public DbSet<Weight> Weights { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        



    }
}