using FribergCarRentals.Models;

namespace FribergCarRentals.Interfaces
{
    public interface IBookingRepository
    {
        int Create(Booking booking);
        Booking GetById(int id);
        List<Booking> GetAll();
        List<Booking> GetAllByCustomer(int id);
        void Update(Booking booking);
        void Delete(int id);
    }
}
