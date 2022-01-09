using MongoDB.Driver;
using Sandogh.Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Persistance.Contexts
{
    public class MongoContext<T> : IMongoDbContext<T>
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T>  mongoCollection;

        public MongoContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("VisitorDb");
            mongoCollection = db.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollection()
        {
            return mongoCollection;
        }
    }
}
