using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class MechanicDetailPage : ContentPage
{
    private readonly DatabaseService _data;
    private readonly Mecanic _mecanic;
    private readonly int _userId;

    public MechanicDetailPage(DatabaseService dataService, Mecanic mecanic, int userId)
    {
        InitializeComponent();
        _data = dataService;
        _mecanic = mecanic;
        _userId = userId;

        LoadMechanicInfo();
        LoadReviews();
    }

    private void LoadMechanicInfo()
    {
        MechanicImage.Source = _mecanic.PozaUrl;
        MechanicName.Text = _mecanic.NumeComplet;
        MechanicRating.Text = _mecanic.RatingText;
    }

    private void LoadReviews()
    {
        var reviews = _data.ListaReviewuri
            .Where(r => r.MecanicId == _mecanic.Id)
            .ToList();

        ReviewsList.ItemsSource = reviews;
    }

    private async void OnLeaveReviewClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LeaveReviewPage(_data, _mecanic, _userId));
    }
}