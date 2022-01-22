using Sandogh.Domain.Common;
using System.Collections;
using System.Collections.Generic;

namespace Sandogh.Domain.Products
{
    public interface IBrand : IRepository<Brand>
    {
        IEnumerable<Brand> GetAllForDDL();
    }
}
