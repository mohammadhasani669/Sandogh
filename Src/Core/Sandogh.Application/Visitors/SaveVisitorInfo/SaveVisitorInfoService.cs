using MongoDB.Driver;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.Visitors;

namespace Sandogh.Application.Visitors.SaveVisitorInfo
{
    public class SaveVisitorInfoService : ISaveVisitorInfoService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _visitorMongoCollection;

        public SaveVisitorInfoService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _visitorMongoCollection = _mongoDbContext.GetCollection();
        }


        public void Execute(RequestSaveVisitorInfoDto request)
        {
            _visitorMongoCollection.InsertOne(new Visitor
            {
                Browser = new VisitorVersion
                {
                    Family = request.Browser.Family,
                    Version = request.Browser.Version,
                },
                CurrentLink = request.CurrentLink,
                Device = new Device
                {
                    Brand = request.Device.Brand,
                    Family = request.Device.Family,
                    IsSpider = request.Device.IsSpider,
                    Model = request.Device.Model,
                },
                IP = request.IP,
                Method = request.Method,
                OS = new VisitorVersion
                {
                    Family = request.OS.Family,
                    Version = request.OS.Version,
                },
                PhisicalPath = request.PhisicalPath,
                Protocol = request.Protocol,
                ReferrerLink = request.ReferrerLink,
                VisitorId = request.VisitorId,
                Time = System.DateTime.Now,
            });
        }
    }
}
