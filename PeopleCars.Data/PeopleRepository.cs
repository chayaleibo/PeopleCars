using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleCars.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<People> GetAll()
        {
            using (var context = new PeopleCarsDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<People>(p => p.Cars);
                context.LoadOptions = loadOptions;
                return context.Peoples.ToList();
            }
            
        }

        public void Add(People person)
        {
            using (var context = new PeopleCarsDbDataContext(_connectionString))
            {
                context.Peoples.InsertOnSubmit(person);
                context.SubmitChanges();
            }
        }

        public void AddCar(Car c)
        {
            using (var context = new PeopleCarsDbDataContext(_connectionString))
            {
                context.Cars.InsertOnSubmit(c);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Car> GetCars(int personId)
        {
            using (var context = new PeopleCarsDbDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Car>(c => c.People);
                context.LoadOptions = loadOptions;
                return context.Cars.Where(c => c.PersonId == personId).ToList();
            }
        }

    }
}
