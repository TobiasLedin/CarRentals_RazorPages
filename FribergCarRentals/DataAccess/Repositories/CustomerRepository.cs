using FribergCarRentals.DataAccess.Database_Contexts;
using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;

namespace FribergCarRentals.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Create(Customer customer)
        {
            try
            {
                _applicationDbContext.Customers.Add(customer);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Delete(int id)
        {
            var customer = _applicationDbContext.Customers.Find(id);
            try
            {
                _applicationDbContext.Customers.Remove(customer);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers;
            try
            {
                if (!_applicationDbContext.Customers.Any())
                {
                    customers = Enumerable.Empty<Customer>().ToList();
                }
                else
                {
                    customers = _applicationDbContext.Customers.ToList();
                }
                return customers;
            }
            catch (Exception)
            {
                return null;    //TODO: Null-return
            }
        }

        public Customer GetByEmail(string email)
        {
            var customer = _applicationDbContext.Customers.FirstOrDefault(x => x.Email == email);
            return customer;
        }

        public Customer GetById(int id)
        {
            if (!_applicationDbContext.Customers.Any())
            {
                return null;
            }
            else
            {
                var customer = _applicationDbContext.Customers.FirstOrDefault(y => y.CustomerId == id); // TODO: Fix include ( Include(x => x.Bookings) )
                return customer;
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                _applicationDbContext.Customers.Update(customer);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
