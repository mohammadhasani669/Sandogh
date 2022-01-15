using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Admin.EndPoint.Models.VIewModels.BankAccounts;
using Sandogh.Application.BankAccounts.Command.Add;
using Sandogh.Application.BankAccounts.Queries.GetAll;
using Sandogh.Application.People.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPerson _personService;
        //private int pageSize = 2;

        public BankAccountsController(IMediator mediator, IPerson person)
        {
            _mediator = mediator;
            _personService = person;
        }

        public IActionResult Index(string search = "", int pageNumber = 1,int pageSize=10)
        {
            GetAllBankAccountQuery getAllBankAccountQuery = new GetAllBankAccountQuery()
            {
                pageSize = pageSize,
                pageNumber = pageNumber,
                Search = search
            };
            var PageSizeList = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "5"  ,Value = "5" },
                new SelectListItem(){ Text = "10" ,Value = "10" },
                new SelectListItem(){ Text = "20" ,Value = "20" },
                new SelectListItem(){ Text = "25" ,Value = "25" },
            };
            var viewModel = new BankAccountListViewModel() 
            {
                Data = _mediator.Send(getAllBankAccountQuery).Result,
                search = search,
                PageSize = pageSize,
                PageSizeOptions = PageSizeList
            };
          
            
            return View(viewModel);
        }


        public IActionResult Create()
        {
            AddBankAccountCommand addBankAccountCommand = new AddBankAccountCommand();
            addBankAccountCommand.People = _personService.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                }).ToList();

            return View(addBankAccountCommand);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddBankAccountCommand addBankAccountCommand)
        {
            _mediator.Send(addBankAccountCommand);

            return RedirectToAction(nameof(Index));
        }
    }
}
