using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _auth;

    public LoginPage(IAuthService auth)
    {
        InitializeComponent();
        _auth = auth;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string parola = ParolaEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
        {
            await DisplayAlert("Eroare", "Completeaza toate campurile.", "OK");
            return;
        }

        bool ok = await _auth.LoginAsync(email, parola);

        if (!ok)
        {
            await DisplayAlert("Eroare", "Email sau parola incorecta.", "OK");
            return;
        }

        await Shell.Current.GoToAsync($"//HomePage");
    }

    private async void OnRegisterRedirectClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}