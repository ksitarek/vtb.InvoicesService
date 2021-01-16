using Microsoft.EntityFrameworkCore;
using vtb.InvoicesService.DataAccess.Mappings;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.DataAccess
{
    public class InvoicesContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public InvoicesContext(DbContextOptions<InvoicesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new InvoiceMapping());
        }
    }
}