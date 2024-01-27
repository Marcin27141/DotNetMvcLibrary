using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Controllers;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.ViewModels;
using LibraryApp.Models.Renewals.RenewalErrors;
using LibraryApp.Models.Renewals.Contracts;
using LibraryApp.Models.Repositories.Renewals.Contracts;

namespace LibraryApp.Tests.ControllersTests
{
    public class RenewalControllerTest
    {
        private IRenewalRepository _renewalRepository;
        private IRentalRepository _rentalRepository;
        public RenewalControllerTest()
        {
            _renewalRepository = A.Fake<IRenewalRepository>();
            _rentalRepository = A.Fake<IRentalRepository>();
        }

        private RenewalController GetRenewalController() =>
            new RenewalController(_renewalRepository, _rentalRepository);

        [Fact]
        public void RenewalController_Index_NotFoundOnNullRental()
        {
            //Arrange
            int rentalId = -1;
            A.CallTo(() => _rentalRepository.GetRentalById(rentalId)).Returns(null);
            var controller = GetRenewalController();

            //Act
            var actionResult = controller.Index(rentalId);

            //Assert
            Assert.Equal(typeof(NotFoundResult), actionResult.GetType());
        }

        [Fact]
        public void RenewalController_Index_CorrectViewWithPassedRental()
        {
            //Arrange
            var rental = new Rental() { RentalId = -1, ReaderId = Guid.NewGuid().ToString(), BookCopyId = -1 };
            A.CallTo(() => _rentalRepository.GetRentalById(rental.RentalId)).Returns(rental);
            var controller = GetRenewalController();

            //Act
            var actionResult = controller.Index(rental.RentalId);

            //Assert
            Assert.Equal(typeof(ViewResult), actionResult.GetType());
            if (actionResult is ViewResult)
            {
                var viewResult = actionResult as ViewResult;
                var model = (ConfirmRenewalViewModel)viewResult?.ViewData?.Model;
                Assert.Equal(model?.Rental, rental);
            }
                
        }

        [Fact]
        public async Task RenewalController_Renew_NotFoundOnNonExisting()
        {
            //Arrange
            int rentalId = -1;
            A.CallTo(() => _rentalRepository.GetRentalById(rentalId)).Returns(null);
            var controller = GetRenewalController();

            //Act
            var actionResult = await controller.Renew(rentalId);

            //Assert
            Assert.Equal(typeof(NotFoundResult), actionResult.GetType());
        }

        [Fact]
        public async Task RenewalController_Renew_RedirectToSuccess()
        {
            //Arrange
            var rental = new Rental() { RentalId = -1, ReaderId = Guid.NewGuid().ToString(), BookCopyId = -1 };
            A.CallTo(() => _rentalRepository.GetRentalById(rental.RentalId)).Returns(rental);
            A.CallTo(() => _renewalRepository.IsValidForRenewal(rental)).Returns(RenewalValidityCheck.Success());
            var controller = GetRenewalController();

            //Act
            var actionResult = await controller.Renew(rental.RentalId);

            //Assert
            Assert.Equal(typeof(RedirectToActionResult), actionResult.GetType());
            if (actionResult is RedirectToActionResult)
            {
                var redirectResult = actionResult as RedirectToActionResult;
                Assert.Equal(nameof(RenewalController.Success), redirectResult?.ActionName);
                var rentalIdRouteValue = redirectResult?.RouteValues?["rentalId"];
                rentalIdRouteValue.Should().Be(rental.RentalId);
            }

        }

        [Theory]
        [InlineData(typeof(RenewalsLimitReachedError), nameof(RenewalController.RenewalsLimit))]
        [InlineData(typeof(HasUnpaidPenaltiesError), nameof(RenewalController.UnpaidPenalties))]
        public async Task RenewalController_Renew_RedirectToErrorView(Type errorType, string expectedAction)
        {
            //Arrange
            var rental = new Rental() { RentalId = -1, ReaderId = Guid.NewGuid().ToString(), BookCopyId = -1 };
            A.CallTo(() => _rentalRepository.GetRentalById(rental.RentalId)).Returns(rental);
            A.CallTo(() => _renewalRepository.IsValidForRenewal(rental)).Returns(
                RenewalValidityCheck.Fail([(RenewalError)Activator.CreateInstance(errorType)]));
            var controller = GetRenewalController();

            //Act
            var actionResult = await controller.Renew(rental.RentalId);

            //Assert
            Assert.Equal(typeof(RedirectToActionResult), actionResult.GetType());
            if (actionResult is RedirectToActionResult)
            {
                var redirectResult = actionResult as RedirectToActionResult;
                Assert.Equal(expectedAction, redirectResult?.ActionName);
            }

        }
    }
}
