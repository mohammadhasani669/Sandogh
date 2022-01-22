using MediatR;
using Sandogh.Common;
using Sandogh.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Application.Products.Queries.PDP
{
    public class PDPQuery : IRequest<PDPQuery>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGrouping<string, ProductFeature>> Features { get; set; }
        public List<SimilarProducts> SimilarCatalogs { get; set; }
    }

    public class SimilarProducts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Images { get; set; }
    }
}
