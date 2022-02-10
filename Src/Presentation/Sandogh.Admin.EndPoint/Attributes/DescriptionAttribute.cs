using System;

namespace Sandogh.Admin.EndPoint.Attributes
{
    public class PermissionAttribute : Attribute
    {
        public string Title { get; set; }
    }
}
