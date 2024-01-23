using LibraryApp.Models;
using LibraryApp.Models.Accounts;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Readers;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Authorize(Policy = "IsReader")]
    public class RentalsController : Controller
    {
        private readonly IReaderRepository _readersRepository;
        private readonly IRentalRepository _rentalRepository;

        public RentalsController(IReaderRepository readersRepository, IRentalRepository rentalRepository)
        {
            _readersRepository = readersRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<IActionResult> Index()
        {
            var reader = await _readersRepository.GetReaderByUsername(User.Identity?.Name);
            var rentals = (reader == null) ? new List<Rental>() : _rentalRepository.GetReaderRentals(reader.LibraryUserId);
            return View(rentals);
        }

        public IActionResult Renew(int rentalId)
        {
            return RedirectToAction("Index", "Renewal", new { rentalId } );
        }
    }
}
