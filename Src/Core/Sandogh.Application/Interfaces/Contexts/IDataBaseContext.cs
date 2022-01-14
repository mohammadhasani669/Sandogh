using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Sandogh.Domain.AdminMenu;
using Sandogh.Domain.BankAccounts;
using Sandogh.Domain.BankProfits;
using Sandogh.Domain.BlackLists;
using Sandogh.Domain.Emails;
using Sandogh.Domain.LoanRepayments;
using Sandogh.Domain.Loans;
using Sandogh.Domain.Logs;
using Sandogh.Domain.People;
using Sandogh.Domain.Transactions;
using Sandogh.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Sandogh.Application.Interfaces.Contexts
{
    public interface IDataBaseContext
    {

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

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<TEntity> Set<TEntity>(string name) where TEntity : class;
       
    }
}
