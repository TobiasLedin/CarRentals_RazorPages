using FribergCarRentals.Services;

namespace FribergCarRentals.Interfaces
{
    public interface IAuthService
    {
        AuthResult AdminAuth(string email, string password);
        AuthResult CheckAdminAuth();
        void RemoveAdminAuth();
        AuthResult CustomerAuth(string email, string password);
        AuthResult CheckCustomerAuth();
        void RemoveCustomerAuth();

    }
}
