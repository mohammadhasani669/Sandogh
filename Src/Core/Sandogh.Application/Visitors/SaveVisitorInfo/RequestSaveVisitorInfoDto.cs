namespace Sandogh.Application.Visitors.SaveVisitorInfo
{
    public class RequestSaveVisitorInfoDto
    {
        public string IP { get; set; }
        public string CurrentLink { get; set; }
        public string ReferrerLink { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public string PhisicalPath { get; set; }
        public DeviceDto Device { get; set; }
        public VisitorVersionDto Browser { get; set; }
        public VisitorVersionDto OS { get; set; }
        public string VisitorId { get; set; }
    }
}
