using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.People;
using Sandogh.Persistance.Common;
using Sandogh.Persistance.Contexts;

namespace Sandogh.Application.People.Repository
{
    public class PersonService : EfRepository<Person>, IPerson
    {
        public PersonService(DatabaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}
