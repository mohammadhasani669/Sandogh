using Microsoft.EntityFrameworkCore;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.BankProfits;
using Sandogh.Domain.BlackLists;
using Sandogh.Domain.Common;
using Sandogh.Domain.Emails;
using Sandogh.Domain.LoanRepayments;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Logs;
using Sandogh.Domain.People;
using Sandogh.Domain.Products;
using Sandogh.Domain.Transactions;
using System.Linq;


namespace Sandogh.Persistance.Contexts
{
    public class DatabaseContext : DbContext,IDataBaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankProfit> BankProfits { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<OperationsLog> OperationsLogs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            modelBuilder.Entity<ProductBrand>().HasQueryFilter(x => !x.IsRemoved);

            base.OnModelCreating(modelBuilder); 
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted
                );
            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

                if (item.State == EntityState.Added)
                {
                    
                }
                if (item.State == EntityState.Modified)
                {
                   
                }

                if (item.State == EntityState.Deleted)
                {
                  
                    item.Property("IsRemoved").CurrentValue = true;
                    item.State = EntityState.Modified;
                }
            }
            return base.SaveChanges();
        }
    }

    
}
