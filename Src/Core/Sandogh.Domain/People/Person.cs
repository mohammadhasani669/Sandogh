using Sandogh.Domain.Common;
using Sandogh.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.People
{
    public class Person : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string HomeAddress { get; set; }
        public string WorkAddress { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public bool IsActive { get; set; }
        
        public List<Loan> Loans { get; set; }
    }
}
