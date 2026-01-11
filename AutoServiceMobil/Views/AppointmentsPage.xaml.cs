using AutoServiceMobil.Models;
using AutoServiceMobil.Services;

namespace AutoServiceMobil.Views;

public partial class AppointmentsPage : ContentPage
{
    private readonly DatabaseService _data;

    public AppointmentsPage(DatabaseService data)
    {
        InitializeComponent();
        _data = data;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadAppointments();
    }

    private void LoadAppointments()
    {
        AppointmentsCollectionView.ItemsSource = null;
        AppointmentsCollectionView.ItemsSource = _data.ListaProgramari
            .OrderBy(p => p.Date)
            .ToList();
    }

    private async void OnAppointmentTapped(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Programare programare)
            return;

        AppointmentsCollectionView.SelectedItem = null;

        await Shell.Current.GoToAsync($"{nameof(EditAppointmentPage)}?AppointmentId={programare.Id}");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton btn && btn.BindingContext is Programare programare)
        {
            bool confirm = await DisplayAlert( "Confirmare", "Sigur vrei sa stergi aceasta programare?", "Da", "Nu");

            if (!confirm)
                return;

            await _data.StergeProgramareAsync(programare.Id);
            LoadAppointments();
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateAppointmentPage));
    }
}