using Microsoft.EntityFrameworkCore;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.BankProfits;
using Sandogh.Domain.BlackLists;
using Sandogh.Domain.Carts;
using Sandogh.Domain.Emails;
using Sandogh.Domain.LoanRepayments;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Logs;
using Sandogh.Domain.People;
using Sandogh.Domain.Products;
using Sandogh.Domain.Transactions;
using Sandogh.Domain.Users;
using System.Threading;
using System.Threading.Tasks;


namespace Sandogh.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {

        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<BankProfit> BankProfits { get; set; }
        DbSet<BlackList> BlackLists { get; set; }
        DbSet<Email> Emails { get; set; }
        DbSet<LoanRepayment> LoanRepayments { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<OperationsLog> OperationsLogs { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<AdminMenu> AdminMenus { get; set; }

        DbSet<Product> Products { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<ProductFeature> ProductFeatures { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Size> Sizes { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<UserAddress> UserAddresses { get; set; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;

    }
}
