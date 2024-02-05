using FribergCarRentals.DataAccess.Database_Contexts;
using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.Models;

namespace FribergCarRentals.DataAccess.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VehicleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Create(Vehicle vehicle)
        {
            try
            {
                _applicationDbContext.Vehicles.Add(vehicle);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public void Delete(int id)
        {
            var vehicle = _applicationDbContext.Vehicles.Find(id);
            try
            {
                _applicationDbContext.Vehicles.Remove(vehicle);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public List<Vehicle> GetAll()
        {
            List<Vehicle> vehicles;
            try
            {
                if (!_applicationDbContext.Vehicles.Any())
                {
                    vehicles = Enumerable.Empty<Vehicle>().ToList();
                }
                else
                {
                    vehicles = _applicationDbContext.Vehicles.ToList();
                }
                return vehicles;
            }
            catch (Exception)
            {
                return null;    //TODO: Null-return
            }
        }

        public Vehicle GetById(int id)
        {
            if (!_applicationDbContext.Vehicles.Any())
            {
                return null;
            }
            else
            {
                var vehicle = _applicationDbContext.Vehicles.FirstOrDefault(x => x.VehicleId == id);
                return vehicle;
            }

        }

        public void Update(Vehicle vehicle)
        {
            try
            {
                _applicationDbContext.Vehicles.Update(vehicle);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
    }
}
