using Microsoft.AspNetCore.Mvc.Filters;
using Sandogh.Application.Visitors.SaveVisitorInfo;
using System;
using UAParser;

namespace Sandogh.WebSite.EndPoint.Utilities.Filters
{
    public class SaveVisitorFilter : IActionFilter
    {
        private readonly SaveVisitorInfoService _saveVisitorInfoService;

        public SaveVisitorFilter(SaveVisitorInfoService saveVisitorInfoService)
        {
            _saveVisitorInfoService = saveVisitorInfoService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string ip = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var ControllerName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ControllerName;
            var userAgent = context.HttpContext.Request.Headers["User-Agent"];
            var uaparser = Parser.GetDefault();
            ClientInfo clientInfo = uaparser.Parse(userAgent);
            var referrer = context.HttpContext.Request.Headers["Referrer"].ToString();
            var currentUrl = context.HttpContext.Request.Path;
            string visitorId = context.HttpContext.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                visitorId = Guid.NewGuid().ToString();
                context.HttpContext.Response.Cookies.Append("VisitorId", visitorId,new Microsoft.AspNetCore.Http.CookieOptions 
                {
                    Path = "/",
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(30),
                });
            }

            _saveVisitorInfoService.Execute(new RequestSaveVisitorInfoDto
            {
                Browser = new VisitorVersionDto
                {
                    Family = clientInfo.UA.Family,
                    Version = $"{clientInfo.UA.Major}.{clientInfo.UA.Minor}.{clientInfo.UA.Patch}"
                },
                CurrentLink = currentUrl,
                Device  = new DeviceDto 
                {
                    Brand = clientInfo.Device.Brand,
                    Family = clientInfo.Device.Family,
                    IsSpider = clientInfo.Device.IsSpider,
                    Model   = clientInfo.Device.Model
                },
                IP = ip,
                Method = context.HttpContext.Request.Method,
                OS = new VisitorVersionDto 
                {
                    Family = clientInfo.OS.Family,
                    Version = $"{clientInfo.OS.Major}.{clientInfo.OS.Minor}.{clientInfo.OS.Patch}"
                },
                PhisicalPath = $"{ControllerName}/{actionName}",
                Protocol = context.HttpContext.Request.Protocol,
                ReferrerLink = referrer,
                VisitorId  = visitorId
            });
        }
    }
}
