namespace Sandogh.Application.Visitors.GetToDayReport
{
    public class TodayDto
    {
        public long PageViews { get; set; }
        public long Visitors { get; set; }
        public float ViewsPerVisitors { get; set; }
        public VisitorCountDto VisitorPerHour  { get; set; }
    }
}
