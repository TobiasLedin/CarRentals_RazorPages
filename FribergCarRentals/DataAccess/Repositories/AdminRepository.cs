using FribergCarRentals.DataAccess.Database_Contexts;
using FribergCarRentals.DataAccess.Interfaces;
using FribergCarRentals.Models;

namespace FribergCarRentals.DataAccess.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdminRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Admin GetByEmail(string email)
        {
            var admin = _applicationDbContext.Admins.FirstOrDefault(x => x.Email == email);
            return admin;
        }

        public void Update(Admin admin)
        {
            _applicationDbContext.Admins.Update(admin);
            _applicationDbContext.SaveChanges();
        }
    }
}
