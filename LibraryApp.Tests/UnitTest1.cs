using FakeItEasy;
using LibraryApp.Controllers;
using LibraryApp.Models.Database;
using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Renewals;
using LibraryApp.Models.Repositories.Rentals;
using LibraryApp.Tests.FakeModels.FakeRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LibraryApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var controller = new HomeController();
            var actionResult = controller.Terms();
            Assert.Equal(typeof(ViewResult), actionResult.GetType());
        }

        //[Fact]
        //public void Test2()
        //{
        //    var myDbContextMock = new Mock<LibraryDbContext>();

        //    var fakeRentalRepo = A.Fake<RentalRepository>(op => op.WithArgumentsForConstructor(new object[] { myDbContextMock.Object }));
        //    var fakeRenewalRepo = A.Fake<RenewalRepository>();
        //    int id = 0;
        //    A.CallTo(() => fakeRentalRepo.GetRentalById(id)).Returns(null);

        //    var controller = new RenewalController(fakeRenewalRepo, fakeRentalRepo);
        //    var actionResult = controller.Index(id);

        //    Assert.Equal(typeof(NotFoundResult), actionResult.GetType());
        //}

        //[Fact]
        //public void Test3()
        //{
        //    var userToRental = new Dictionary<string, List<Rental>>();
        //    var rentalRepository = new FakeRentalsRepo(userToRental);
        //    var renewalRepository = new FakeRenewalsRepo();
        //    var controller = new RenewalController(renewalRepository, rentalRepository);
        //    int rentalId = 0;
        //    var actionResult = controller.Index(rentalId);
        //    Assert.Equal(typeof(NotFoundResult), actionResult.GetType());
        //}
    }
}