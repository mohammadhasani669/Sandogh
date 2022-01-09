using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Domain.Visitors
{
    public class Visitor
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string IP { get; set; }
        public string CurrentLink { get; set; }
        public string ReferrerLink { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public string PhisicalPath { get; set; }
        public Device Device { get; set; }
        public VisitorVersion Browser { get; set; }
        public VisitorVersion OS { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime  Time { get; set; }
        public string VisitorId { get; set; }
    }
}
