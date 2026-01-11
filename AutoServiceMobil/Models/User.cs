using SQLite;

namespace AutoServiceMobil.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nume { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Prenume { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefon { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Parola { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Rol { get; set; } = "user";

        public static bool EmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (email.Length > 100) return false;
            if (!email.Contains("@")) return false;
            if (!email.Contains(".")) return false;
            if (email.Contains(" ")) return false;

            var atIndex = email.IndexOf("@");
            var dotIndex = email.LastIndexOf(".");

            return atIndex > 0 && dotIndex > atIndex;
        }

        public static bool ParolaValida(string parola)
        {
            if (string.IsNullOrWhiteSpace(parola)) return false;
            if (parola.Length < 6) return false;
            if (parola.Length > 100) return false;

            bool areLitera = parola.Any(char.IsLetter);
            bool areCifra = parola.Any(char.IsDigit);

            return areLitera && areCifra;
        }

        public static bool TelefonValid(string telefon)
        {
            if (string.IsNullOrWhiteSpace(telefon)) return false;
            if (telefon.Length < 10 || telefon.Length > 15) return false;

            return telefon.All(char.IsDigit);
        }
    }
}