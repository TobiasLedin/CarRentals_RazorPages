using FribergCarRentals.DataAccess.Database_Contexts;
using FribergCarRentals.Interfaces;
using FribergCarRentals.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentals.DataAccess.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public int Create(Booking booking)
        {
            try
            {
                _applicationDbContext.Bookings.Add(booking);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
            return booking.BookingId;
        }

        public void Delete(int id)
        {
            var booking = _applicationDbContext.Bookings.Find(id);
            try
            {
                _applicationDbContext.Bookings.Remove(booking);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public List<Booking> GetAll()
        {
            List<Booking> bookings;
            try
            {
                if (!_applicationDbContext.Bookings.Any())
                {
                    bookings = Enumerable.Empty<Booking>().ToList();
                }
                else
                {
                    bookings = _applicationDbContext.Bookings.Include(x => x.Customer).Include(x => x.Vehicle).OrderBy(x => x.BookingStart).ToList();
                }
                return bookings;
            }
            catch (Exception)
            {
                return null;    //TODO: Null-return
            }
        }

        public List<Booking> GetAllByCustomer(int id)
        {
            List<Booking> bookings;
            try
            {
                if (!_applicationDbContext.Bookings.Any())
                {
                    bookings = Enumerable.Empty<Booking>().ToList();
                }
                else
                {
                    bookings = _applicationDbContext.Bookings.Where(x => x.CustomerId == id).Include(x => x.Customer).Include(x => x.Vehicle).OrderBy(x => x.BookingStart).ToList();
                }
                return bookings;
            }
            catch (Exception)
            {
                return null;    //TODO: Null-return
            }
        }

        public Booking GetById(int id)
        {
            if (!_applicationDbContext.Bookings.Any())
            {
                return null;
            }
            else
            {
                var booking = _applicationDbContext.Bookings.Include(x => x.Customer).Include(x => x.Vehicle).FirstOrDefault(z => z.BookingId == id); // TODO: Fix include ( Include(x => x.Vehicle).Include(y => y.Customer) )
                return booking;
            }
        }

        public void Update(Booking booking)
        {
            try
            {
                _applicationDbContext.Bookings.Update(booking);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
