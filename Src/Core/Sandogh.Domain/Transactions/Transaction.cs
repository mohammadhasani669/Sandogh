using Sandogh.Domain.Common;
using Sandogh.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Transactions
{
    public class Transaction : Entity
    {
        public int PersonId { get; set; }
        public string NotebookId { get; set; }
        public float Amount { get; set; }
        public DateTime ReciveDate { get; set; }
        public string Description { get; set; }
        public bool depositOrWithdraw { get; set; }

        public virtual Person Person { get; set; }
    }
}
