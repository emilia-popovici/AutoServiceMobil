using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class EditMecanicPage : ContentPage
{
    private readonly DatabaseService _data;
    private readonly Mecanic _mecanic;

    public EditMecanicPage(DatabaseService dataService, Mecanic mecanic)
    {
        InitializeComponent();
        _data = dataService;
        _mecanic = mecanic;

        NumeEntry.Text = mecanic.Nume;
        PrenumeEntry.Text = mecanic.Prenume;
        PozaEntry.Text = mecanic.PozaUrl;
        RatingEntry.Text = mecanic.Rating.ToString();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _mecanic.Nume = NumeEntry.Text;
        _mecanic.Prenume = PrenumeEntry.Text;
        _mecanic.PozaUrl = PozaEntry.Text;

        double rating = 0;
        double.TryParse(RatingEntry.Text, out rating);
        _mecanic.Rating = rating;

        await _data.UpdateMecanicAsync(_mecanic);

        await DisplayAlert("Succes", "Modificarile au fost salvate!", "OK");
        await Navigation.PopAsync();
    }
}