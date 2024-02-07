using FribergCarRentals.Models;

namespace FribergCarRentals.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetByEmail(string email);
        void Update(Admin admin);
    }
}
