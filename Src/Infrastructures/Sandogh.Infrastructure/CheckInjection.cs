using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandogh.Infrastructure
{
    public class CheckInjection
    {
        List<string> blackList = new List<string>()
            {
                "--",
                "or",
                "=",
                "and",
                 ";",
                "/*",
                "*/",
                "@@",
                "char",
                "nchar",
                "varchar",
                "nvarchar",
                "alter",
                "begin",
                "cast",
                "create",
                "cursor",
                "declare",
                "delete",
                "drop",
                "end",
                "exec",
                "execute",
                "fetch",
                "insert",
                "kill",
                "open",
                "select",
                "sys",
                "sysobjects",
                "syscolumns",
                "table",
                "update",
            };


        public bool IsInjection(string inputText)
        {
            var inputArray = inputText.Split(new char[] { ' ' });
            foreach (var input in inputArray)
            {
                if (blackList.Any(p => input.ToUpper().Equals(p.ToUpper())))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
