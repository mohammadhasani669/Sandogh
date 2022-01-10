using Microsoft.EntityFrameworkCore;
using Sandogh.Application.Interfaces.Contexts;
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


namespace Sandogh.Persistance.Contexts
{
    public class DatabaseContext : DbContext,IDataBaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        public DbSet<BankAccount> bankAccounts { get; set; }
        public DbSet<BankProfit> BankProfits { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<OperationsLog> OperationsLogs { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);


            base.OnModelCreating(modelBuilder); 
        }
    }

    
}
