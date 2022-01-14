using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Common;
using Sandogh.Domain.BankAccounts;
using System.Collections.Generic;

namespace Sandogh.Admin.EndPoint.Models.VIewModels.BankAccounts
{
    public class BankAccountListViewModel
    {
        public PagedData<BankAccount>  Data{ get; set; }
        public string search { get; set; }
        public int PageSize { get; set; }
        public List<SelectListItem> PageSizeOptions { get; set; }
    }
}
