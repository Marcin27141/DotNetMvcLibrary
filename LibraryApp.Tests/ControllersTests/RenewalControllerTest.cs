using FakeItEasy;
using FluentAssertions;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Database;
using LibraryApp.Models.Repositories.Renewals.RenewalCreator;
using LibraryApp.Models.Repositories.Renewals.RenewalValidators;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Specifications.RenewalSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Controllers;
using LibraryApp.Models.Repositories.Rentals;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Models.ViewModels;

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
    }
}
