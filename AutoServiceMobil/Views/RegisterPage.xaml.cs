using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly IAuthService _auth;

        public RegisterPage(IAuthService auth)
        {
            InitializeComponent();
            _auth = auth;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string nume = NumeEntry.Text?.Trim() ?? "";
            string prenume = PrenumeEntry.Text?.Trim() ?? "";
            string email = EmailEntry.Text?.Trim() ?? "";
            string telefon = TelefonEntry.Text?.Trim() ?? "";
            string parola = ParolaEntry.Text?.Trim() ?? "";
            string confirmare = ConfirmParolaEntry.Text?.Trim() ?? "";


            if (string.IsNullOrWhiteSpace(nume))
            {
                await DisplayAlert("Eroare", "Introdu numele.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(prenume))
            {
                await DisplayAlert("Eroare", "Introdu prenumele.", "OK");
                return;
            }

            if (!User.EmailValid(email))
            {
                await DisplayAlert("Eroare", "Email invalid.", "OK");
                return;
            }

            if (!User.TelefonValid(telefon))
            {
                await DisplayAlert("Eroare", "Telefon invalid. Foloseste doar cifre (10–15 caractere).", "OK");
                return;
            }

            if (!User.ParolaValida(parola))
            {
                await DisplayAlert("Eroare", "Parola trebuie sa aiba minim 6 caractere, o litera si o cifra.", "OK");
                return;
            }

            if (parola != confirmare)
            {
                await DisplayAlert("Eroare", "Parolele nu coincid.", "OK");
                return;
            }

            bool ok = await _auth.RegisterAsync(nume, email, parola);

            if (!ok)
            {
                await DisplayAlert("Eroare", "Exista deja un cont cu acest email.", "OK");
                return;
            }

            _auth.UserCurent.Prenume = prenume;
            _auth.UserCurent.Telefon = telefon;

            var db = new DatabaseService();
            await db.SaveUserAsync(_auth.UserCurent);

            await DisplayAlert("Succes", "Cont creat cu succes!", "OK");

            await Shell.Current.GoToAsync(nameof(HomePage));
        }

        private async void OnLoginRedirectClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}