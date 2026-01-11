using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class AddMecanicPage : ContentPage
{
    private readonly DatabaseService _data;

    public AddMecanicPage(DatabaseService dataService)
    {
        InitializeComponent();
        _data = dataService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NumeEntry.Text) ||
            string.IsNullOrWhiteSpace(PrenumeEntry.Text))
        {
            await DisplayAlert("Eroare", "Completeaza numele si prenumele!", "OK");
            return;
        }

        double rating = 0;
        double.TryParse(RatingEntry.Text, out rating);

        var mecanic = new Mecanic
        {
            Nume = NumeEntry.Text,
            Prenume = PrenumeEntry.Text,
            PozaUrl = PozaEntry.Text,
            Rating = rating
        };

        await _data.AdaugaMecanicAsync(mecanic);

        await DisplayAlert("Succes", "Mecanicul a fost adaugat!", "OK");
        await Navigation.PopAsync();
    }
}