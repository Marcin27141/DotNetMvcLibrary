using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class RenewalController : Controller
    {
        private readonly IRenewalRepository _renewalRepository;
        private readonly IRentalRepository _rentalRepository;

        public RenewalController(
            IRenewalRepository renewalRepository,
            IRentalRepository rentalRepository)
        {
            _renewalRepository = renewalRepository;
            _rentalRepository = rentalRepository;
        }
        public IActionResult Index(int rentalId)
        {
            var rental = _rentalRepository.GetRentalById(rentalId);
            return rental == null ? NotFound() : View(rental);
        }
    }
}
