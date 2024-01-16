using LibraryApp.Models;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Accounts;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IAccountRepository accountRepository, IRentalRepository rentalRepository)
        {
            _accountRepository = accountRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var reader = await _accountRepository.GetReaderByUsername(User.Identity.Name);
            var rentals = (reader == null) ? new List<Rental>() : _rentalRepository.GetReaderRentals(reader.LibraryUserId);
            return View(new RentalsViewModel(rentals));
        }
    }
}
