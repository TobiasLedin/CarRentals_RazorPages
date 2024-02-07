using FribergCarRentals.Interfaces;

namespace FribergCarRentals.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _session;
        private readonly IAdminRepository _adminRepo;
        private readonly ICustomerRepository _customerRepo;
        private const string _admin = "_admin";
        private const string _customer = "_customer";

        public AuthService(IAdminRepository adminRepo, ICustomerRepository customerRepo, IHttpContextAccessor session)
        {
            _session = session;
            _adminRepo = adminRepo;
            _customerRepo = customerRepo;
        }

        public AuthResult CheckAdminAuth()
        {
            if (_session.HttpContext.Session.TryGetValue(_admin, out _))
            {
                return new AuthResult { Success = true };
            }
            else
            {
                return new AuthResult { Success = false, Message = "Administrator session has expired!" };
            }
        }

        public AuthResult CheckCustomerAuth()
        {
            if (_session.HttpContext.Session.TryGetValue(_customer, out _))
            {
                var id = (int)_session.HttpContext.Session.GetInt32(_customer);

                return new AuthResult { Success = true, Id = id };
            }
            else
            {
                return new AuthResult { Success = false, Message = "Customer session has expired!" };
            }
        }

        public AuthResult AdminAuth(string email, string password)
        {
            var admin = _adminRepo.GetByEmail(email);

            if (admin != null && admin.Password == password)
            {
                if (_session.HttpContext.Session.TryGetValue(_customer, out _))
                {
                    _session.HttpContext.Session.Remove(_customer);
                }

                _session.HttpContext.Session.SetInt32(_admin, admin.AdminId);

                return new AuthResult { Success = true };
            }
            else if (admin != null && admin.Password != password)
            { 
                return new AuthResult { Success = false, Message = "The password is incorrect" };
            }
            else
            {
                return new AuthResult { Success = false, Message = "The is no account with this email" };
            }
        }

        public AuthResult CustomerAuth(string email, string password)
        {
            var customer = _customerRepo.GetByEmail(email);

            if (customer != null && customer.Password == password)
            {
                if (_session.HttpContext.Session.TryGetValue(_admin, out _))
                {
                    _session.HttpContext.Session.Remove(_admin);
                }

                _session.HttpContext.Session.SetInt32(_customer, customer.CustomerId);

                return new AuthResult { Success = true, Id = customer.CustomerId };
            }
            else if (customer != null && customer.Password != password)
            {
                return new AuthResult { Success = false, Message = "The password is incorrect" };
            }
            else
            {
                return new AuthResult { Success = false, Message = "The is no account with this email" };
            }
        }

        public void RemoveAdminAuth()
        {
            _session.HttpContext.Session.Remove(_admin);
        }

        public void RemoveCustomerAuth()
        {
            _session.HttpContext.Session.Remove(_customer);
        }
    }

    public class AuthResult()
    {
        public bool Success { get; set; } = default!;
        public int Id { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
            
}
