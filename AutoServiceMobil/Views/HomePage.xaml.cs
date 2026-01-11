using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class HomePage : ContentPage
{
    private readonly DatabaseService _db;

    public List<Serviciu> Servicii { get; set; }
    public List<Mecanic> Mecanici { get; set; }

    public HomePage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Servicii = _db.ListaServicii.ToList();
        Mecanici = _db.ListaMecanici.ToList();

        BindingContext = this;
    }

    private async void OnLoginRegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

    private async void OnContactClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Contact", "Telefon: 07xx xxx xxx\nEmail: contact@autoservice.ro", "OK");
    }

    private async void OnAdminMecaniciClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AdminMecaniciPage));
    }

    private async void OnProgrameazaTeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("CreateAppointmentPage");
    }
}