using LibraryApp.Models.Database.Entities;
using LibraryApp.Models.Repositories.Rentals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Tests.FakeModels.FakeRepositories
{
    public class FakeRentalsRepo : IRentalRepository
    {
        private readonly Dictionary<string, List<Rental>> _usersRentals;
        private readonly List<Rental> _allRentals;

        public FakeRentalsRepo(Dictionary<string, List<Rental>> usersRentals) {
            _usersRentals = usersRentals;
            _allRentals = usersRentals.SelectMany(kvp => kvp.Value).ToList();
        }
        public List<Rental> GetReaderRentals(string readerId)
        {
            return _usersRentals.TryGetValue(readerId, out List<Rental>? value) ? value :
                new List<Rental>();
        }

        public Rental? GetRentalById(int id)
        {
            return _allRentals.FirstOrDefault(r => r.RentalId == id);
        }
    }
}
