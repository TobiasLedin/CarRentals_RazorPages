using FribergCarRentals.Models;

namespace FribergCarRentals.DataAccess.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetByEmail(string email);
        void Update(Admin admin);
    }
}
