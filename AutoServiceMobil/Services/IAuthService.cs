using AutoServiceMobil.Models;

namespace AutoServiceMobil.Services
{
    public interface IAuthService
    {
        User UserCurent { get; }

        bool IsLoggedIn { get; }

        Task<bool> LoginAsync(string email, string parola);

        Task<bool> RegisterAsync(string nume, string email, string parola);

        void Logout();
    }
}