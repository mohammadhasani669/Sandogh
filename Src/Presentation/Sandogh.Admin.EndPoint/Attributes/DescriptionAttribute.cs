using System;

namespace Sandogh.Admin.EndPoint.Attributes
{
    public class PermissionNameAttribute : Attribute
    {
        public string Title { get; set; }
    }
}
