using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sandogh.Application.BankAccounts.Command.Add;
using Sandogh.Application.BankAccounts.Queries.GetAll;
using Sandogh.Application.People.Repository;
using System.Linq;

namespace Sandogh.Admin.EndPoint.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPerson _personService;

        public BankAccountsController(IMediator mediator, IPerson person)
        {
            _mediator = mediator;
            _personService = person;
        }

        public IActionResult Index()
        {
            GetAllBankAccountQuery  getAllBankAccountQuery = new GetAllBankAccountQuery();
            var result = _mediator.Send(getAllBankAccountQuery).Result;
            return View(result);
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
        public IActionResult Create(AddBankAccountCommand addBankAccountCommand)
        {
            _mediator.Send(addBankAccountCommand);

            return RedirectToAction(nameof(Index));
        }
    }
}
