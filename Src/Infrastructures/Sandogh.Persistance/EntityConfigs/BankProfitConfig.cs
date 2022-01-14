using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sandogh.Domain.BankProfits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.EntityConfigs
{
    public class BankProfitConfig : IEntityTypeConfiguration<BankProfit>
    {
        public void Configure(EntityTypeBuilder<BankProfit> builder)
        {
            
        }
    }
}
