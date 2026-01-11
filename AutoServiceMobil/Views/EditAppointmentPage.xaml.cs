using AutoServiceMobil.Models;
using AutoServiceMobil.Services;
using System.Collections.ObjectModel;

namespace AutoServiceMobil.Views;

[QueryProperty(nameof(AppointmentId), nameof(AppointmentId))]
public partial class EditAppointmentPage : ContentPage
{
    private readonly DatabaseService _data;

    public int AppointmentId { get; set; }

    public ObservableCollection<Serviciu> Servicii { get; set; }
    public ObservableCollection<Mecanic> Mecanici { get; set; }

    private Programare _currentAppointment;

    public EditAppointmentPage(DatabaseService data)
    {
        InitializeComponent();
        _data = data;

        Servicii = _data.ListaServicii;
        Mecanici = _data.ListaMecanici;

        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _currentAppointment = _data.ListaProgramari
            .FirstOrDefault(p => p.Id == AppointmentId);

        if (_currentAppointment == null)
        {
            DisplayAlert("Eroare", "Programarea nu a fost gãsitã.", "OK");
            Shell.Current.GoToAsync("..");
            return;
        }

        var selectedService = Servicii.FirstOrDefault(s => s.Nume == _currentAppointment.ServiceName);
        ServicePicker.SelectedItem = selectedService;

        var selectedMechanic = Mecanici.FirstOrDefault(m => m.Nume == _currentAppointment.MechanicName);
        MechanicPicker.SelectedItem = selectedMechanic;

        AppointmentDatePicker.MinimumDate = DateTime.Today;
        AppointmentDatePicker.Date = _currentAppointment.Date.Date;

        var slots = new List<TimeSpan>();
        var start = new TimeSpan(9, 0, 0);
        var end = new TimeSpan(17, 0, 0);

        while (start <= end)
        {
            slots.Add(start);
            start = start.Add(TimeSpan.FromMinutes(30));
        }

        TimePicker.ItemsSource = slots;

        var currentTime = _currentAppointment.Date.TimeOfDay;
        TimePicker.SelectedItem = slots.FirstOrDefault(t => t == currentTime);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (ServicePicker.SelectedItem is not Serviciu service ||
            MechanicPicker.SelectedItem is not Mecanic mechanic ||
            TimePicker.SelectedItem is not TimeSpan time)
        {
            await DisplayAlert("Atentie", "Selecteaza serviciu, mecanic si ora!", "OK");
            return;
        }

        var finalDate = AppointmentDatePicker.Date.Value + time;

        _currentAppointment.ServiceName = service.Nume;
        _currentAppointment.MechanicName = mechanic.Nume;
        _currentAppointment.Date = finalDate;

        await _data.UpdateProgramareAsync(_currentAppointment);

        await DisplayAlert("Succes", "Programarea a fost actualizata!", "OK");
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}