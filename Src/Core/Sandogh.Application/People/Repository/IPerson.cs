using Sandogh.Application.Interfaces;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.People.Repository
{
    public interface IPerson : IRepository<Person>
    {
    }

    public class PersonService : EfRepository<Person>, IPerson
    {
        public PersonService(IDataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
