using System.Data.Entity;
using TDDDemoApp.DAL.Entities;

namespace TDDDemoApp.DAL
{
    public class TDDDemoAppContext : DbContext
    {
        public TDDDemoAppContext()
            : base("Data Source=localhost;Initial Catalog=TDDDemoAppDb;Integrated Security=SSPI;")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageEntity>().ToTable("Messages");
        }

        public DbSet<MessageEntity> Messages { get; set; } 
    }
}