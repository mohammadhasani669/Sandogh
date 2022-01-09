using System.Collections.Generic;
using System.Linq;

namespace Sandogh.WebSite.EndPoint.Utilities.Convertors
{
    public static class ConvertPersianNumbers
    {
        public static string PersianToEnglish(this string persianStr)
        {
           
            persianStr = persianStr.Replace('۰', '0');
            persianStr = persianStr.Replace('۱', '1');
            persianStr = persianStr.Replace('۲', '2');
            persianStr = persianStr.Replace('۳', '3');
            persianStr = persianStr.Replace('۴', '4');
            persianStr = persianStr.Replace('٤', '4');
            persianStr = persianStr.Replace('۵', '5');
            persianStr = persianStr.Replace('٥', '5');
            persianStr = persianStr.Replace('۶', '6');
            persianStr = persianStr.Replace('٦', '6');
            persianStr = persianStr.Replace('۷', '7');
            persianStr = persianStr.Replace('۸', '8');
            persianStr = persianStr.Replace('۹', '9');
          
         
            return persianStr;
        }
    }
}
