using AutoServiceMobil.Views;
using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class AdminMecaniciPage : ContentPage
{
    private readonly DatabaseService _data;

    public AdminMecaniciPage(DatabaseService dataService)
    {
        InitializeComponent();
        _data = dataService;

        LoadMechanics();
    }

    private void LoadMechanics()
    {
        MechanicsList.ItemsSource = null;
        MechanicsList.ItemsSource = _data.ListaMecanici;
    }

    private async void OnAddMecanicClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMecanicPage(_data));
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Mecanic mecanic)
        {
            await Navigation.PushAsync(new EditMecanicPage(_data, mecanic));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is Mecanic mecanic)
        {
            bool confirm = await DisplayAlert(
                "Confirmare",
                $"Stergi mecanicul '{mecanic.NumeComplet}'?",
                "Da", "Nu");

            if (!confirm)
                return;

            await _data.StergeMecanicAsync(mecanic.Id);
            LoadMechanics();
        }
    }
}