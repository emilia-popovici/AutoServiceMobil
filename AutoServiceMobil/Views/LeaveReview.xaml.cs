using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class LeaveReviewPage : ContentPage
{
    private readonly DatabaseService _data;
    private readonly Mecanic _mecanic;
    private readonly int _userId;

    public List<int> Ratings { get; } = new() { 1, 2, 3, 4, 5 };

    public LeaveReviewPage(DatabaseService dataService, Mecanic mecanic, int userId)
    {
        InitializeComponent();
        _data = dataService;
        _mecanic = mecanic;
        _userId = userId;

        BindingContext = this;

        LoadMechanicInfo();
    }

    private void LoadMechanicInfo()
    {
        MechanicImage.Source = _mecanic.PozaUrl;
        MechanicName.Text = _mecanic.NumeComplet;
        MechanicRating.Text = _mecanic.RatingText;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        if (RatingPicker.SelectedItem is not int rating)
        {
            await DisplayAlert("Eroare", "Selecteaza un rating!", "OK");
            return;
        }

        string comentariu = CommentEditor.Text?.Trim() ?? "";

        var review = new Review
        {
            UserId = _userId,
            MecanicId = _mecanic.Id,
            Rating = rating,
            Comentariu = comentariu
        };

        await _data.AdaugaReviewAsync(review);

        await DisplayAlert("Succes", "Review-ul a fost trimis!", "OK");
        await Shell.Current.GoToAsync("..");
    }
}