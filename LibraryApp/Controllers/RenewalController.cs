using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Renewals.RenewalErrors;
using LibraryApp.Models.Repositories.Rentals;
using LibraryApp.Models.ViewModels;
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
            if (rental == null)
                return NotFound();
            else
            {
                var viewModel = new ConfirmRenewalViewModel()
                {
                    Rental = rental,
                    RemainingRenewals = _renewalRepository.GetRemainingRenewals(rental),
                    RenewalSpanInDays = _renewalRepository.GetRenewalSpan()
                };
                return View(viewModel);
            }            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Renew(int rentalId)
        {
            var rental = _rentalRepository.GetRentalById(rentalId);
            if (rental == null) return NotFound();

            var validityCheck = _renewalRepository.IsValidForRenewal(rental);
            if (validityCheck.IsValidForRenewal)
            {
                await _renewalRepository.RenewRental(rentalId);
                return RedirectToAction(nameof(Success), new { rentalId });
            }
            else return DisplayViewForErrors(validityCheck.Errors);
        }

        private ActionResult DisplayViewForErrors(List<RenewalError> errors)
        {
            if (!errors.Any())
                return NotFound();

            var error = errors.First();
            string? viewName = error switch
            {
                _ when error is HasUnpaidPenaltiesError => nameof(UnpaidPenalties),
                _ when error is RenewalsLimitReachedError => nameof(RenewalsLimit),
                _ => null
            };

            return viewName == null ? NotFound() : RedirectToAction(viewName);
        }

        public IActionResult Success(int rentalId)
        {
            var rental = _rentalRepository.GetRentalById(rentalId);
            return rental == null ? NotFound() : View(rental);
        }

        public IActionResult UnpaidPenalties() => View();
        public IActionResult RenewalsLimit() => View();

    }
}
