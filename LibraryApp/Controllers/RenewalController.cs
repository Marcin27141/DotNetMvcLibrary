using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class RenewalController : Controller
    {
        private readonly IRenewalRepository _renewalRepository;

        public RenewalController(IRenewalRepository renewalRepository)
        {
            _renewalRepository = renewalRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
