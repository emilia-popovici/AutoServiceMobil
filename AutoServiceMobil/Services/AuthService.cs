using AutoServiceMobil.Models;

namespace AutoServiceMobil.Services
{
    public class AuthService : IAuthService
    {
        private readonly DatabaseService _db;

        public User UserCurent { get; private set; }

        public bool IsLoggedIn => UserCurent != null;

        public AuthService(DatabaseService db)
        {
            _db = db;
        }

        public async Task<bool> LoginAsync(string email, string parola)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
                return false;

            var user = await _db.GetUserByEmailAsync(email);

            if (user == null)
                return false;

            if (user.Parola != parola)
                return false;

            UserCurent = user;
            return true;
        }

        public async Task<bool> RegisterAsync(string nume, string email, string parola)
        {
            if (string.IsNullOrWhiteSpace(nume) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(parola))
                return false;

            var existent = await _db.GetUserByEmailAsync(email);
            if (existent != null)
                return false;

            var user = new User
            {
                Nume = nume,
                Prenume = "",
                Email = email,
                Telefon = "",
                Parola = parola,
                Rol = "user"   // ← AICI e corect pentru modelul tău
            };

            await _db.SaveUserAsync(user);
            UserCurent = user;
            return true;
        }

        public void Logout()
        {
            UserCurent = null;
        }
    }
}