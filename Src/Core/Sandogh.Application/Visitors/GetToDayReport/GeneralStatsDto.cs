namespace Sandogh.Application.Visitors.GetToDayReport
{
    public class GeneralStatsDto
    {
        public long TotalPageViews { get; set; }
        public long TotalVisitors { get; set; }
        public float PageViewPerVisit { get; set; }
        public VisitorCountDto VisitorPerDay { get; set; }
    }
}
