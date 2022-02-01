using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.BankProfits;
using Sandogh.Domain.BlackLists;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Common;
using Sandogh.Domain.Emails;
using Sandogh.Domain.LoanRepayments;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Logs;
using Sandogh.Domain.Orders;
using Sandogh.Domain.People;
using Sandogh.Domain.Products;
using Sandogh.Domain.Transactions;
using Sandogh.Domain.Users;
using System;
using System.Linq;
using System.Linq.Expressions;

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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            #region Global QueryFilter for All Entity

           
            // define your filter expression tree
            Expression<Func<Entity, bool>> filterExpr = bm => !bm.IsRemoved;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(Entity)))
                {
                    // modify expression to handle correct child type
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    // set filter
                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }
            //modelBuilder.Entity<Brand>().HasQueryFilter(x => !x.IsRemoved);
            #endregion

            //باعث میشه که آدرس رو به عنوان یه جدول دیگه در نظر نگیره
            modelBuilder.Entity<Order>().OwnsOne(p => p.Address);

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
