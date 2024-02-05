using FribergCarRentals.Models;

namespace FribergCarRentals.DataAccess.Interfaces
{
    public interface IBookingRepository
    {
        int Create(Booking booking);
        Booking GetById(int id);
        List<Booking> GetAll();
        void Update(Booking booking);
        void Delete(int id);
    }
}
