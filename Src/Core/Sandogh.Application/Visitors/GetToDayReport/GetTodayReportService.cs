using MongoDB.Driver;
using Sandogh.Application.Interfaces.Contexts;
using Sandogh.Domain.Visitors;
using System;
using System.Linq;

namespace Sandogh.Application.Visitors.GetToDayReport
{
    public class GetTodayReportService : IGetTodayReportService
    {
        private readonly IMongoDbContext<Visitor> _mongoDbContext;
        private readonly IMongoCollection<Visitor> _visitorMongoCollection;

        public GetTodayReportService(IMongoDbContext<Visitor> mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _visitorMongoCollection = _mongoDbContext.GetCollection();
        }


        public ResultTodayReportDto Execute()
        {
            var start = DateTime.Today;
            var End = DateTime.UtcNow.AddDays(1);

            var TodayPageViewCount = _visitorMongoCollection.AsQueryable()
                .Where(x => x.Time >= start && x.Time < End).LongCount();
            var TodayVisitorCount = _visitorMongoCollection.AsQueryable()
                .Where(x => x.Time >= start && x.Time < End).GroupBy(x => x.VisitorId)
                .LongCount();

            var AllPageViewCount = _visitorMongoCollection.AsQueryable().LongCount();
            var AllVisitorCount = _visitorMongoCollection.AsQueryable().GroupBy(x => x.VisitorId).LongCount();

            var TodayPageViewList = _visitorMongoCollection.AsQueryable()
                .Where(x => x.Time >= start && x.Time < End)
                .Select(x => x.Time).ToList();

            VisitorCountDto VisitPerHour = new VisitorCountDto
            {
                Display = new string[24],
                Value = new int[24],
            };

            VisitorCountDto visitPerDay = GetVisitPerDay();

            for (int i = 0; i <= 23; i++)
            {
                VisitPerHour.Display[i] = $"{i}";
                VisitPerHour.Value[i] = TodayPageViewList.Where(x => x.Hour == i).Count();
            }

            return new ResultTodayReportDto
            {
                generalStats = new GeneralStatsDto
                {
                    TotalVisitors = AllVisitorCount,
                    TotalPageViews = AllPageViewCount,
                    PageViewPerVisit = GetAvg(AllPageViewCount, AllVisitorCount),
                    VisitorPerDay = visitPerDay

                },
                ToDay = new TodayDto
                {
                    PageViews = TodayPageViewCount,
                    Visitors = TodayVisitorCount,
                    ViewsPerVisitors = GetAvg(TodayPageViewCount, TodayVisitorCount),
                    VisitorPerHour = VisitPerHour
                }
            };
        }

        private VisitorCountDto GetVisitPerDay()
        {
            DateTime MonthStart = DateTime.Now.Date.AddDays(-30);
            DateTime MonthEnds = DateTime.Now.Date.AddDays(1);
            var Month_PageViewList = _visitorMongoCollection.AsQueryable()
                .Where(p => p.Time >= MonthStart && p.Time < MonthEnds)
                .Select(p => new { p.Time })
                .ToList();
            VisitorCountDto visitPerDay = new VisitorCountDto() { Display = new string[31], Value = new int[31] };
            for (int i = 0; i <= 30; i++)
            {
                var currentday = DateTime.Now.AddDays(i * (-1));
                visitPerDay.Display[i] = i.ToString();
                visitPerDay.Value[i] = Month_PageViewList.Where(p => p.Time.Date == currentday.Date).Count();
            }

            return visitPerDay;
        }

        private float GetAvg(long visitPage, long visitor)
        {
            if (visitor == 0)
            {
                return 0;
            }
            else
            {
                return visitPage / visitor;
            }

        }

    }
}
