using FribergCarRentals.Models;

namespace FribergCarRentals.Interfaces
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        Customer GetById(int id);
        Customer GetByEmail(string email);
        List<Customer> GetAll();
        void Update(Customer customer);
        void Delete(int id);
    }
}
