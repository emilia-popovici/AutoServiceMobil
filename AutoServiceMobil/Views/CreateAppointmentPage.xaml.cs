using AutoServiceMobil.Services;
using AutoServiceMobil.Models;

namespace AutoServiceMobil.Views;

public partial class CreateAppointmentPage : ContentPage
{
    private readonly DatabaseService _data;

    public CreateAppointmentPage(DatabaseService dataService)
    {
        InitializeComponent();
        _data = dataService;

        LoadInitialData();
    }

    private void LoadInitialData()
    {
        ServicePicker.ItemsSource = _data.ListaServicii;
        MechanicPicker.ItemsSource = _data.ListaMecanici;

        TimePicker.ItemsSource = new List<TimeSpan>();
    }

    private void OnSelectionChanged(object sender, EventArgs e)
    {
        UpdateAvailableSlots();
    }

    private void OnDateChanged(object sender, DateChangedEventArgs e)
    {
        UpdateAvailableSlots();
    }

    private void UpdateAvailableSlots()
    {
        TimePicker.ItemsSource = new List<TimeSpan>();

        if (ServicePicker.SelectedItem is not Serviciu service ||
            MechanicPicker.SelectedItem is not Mecanic mechanic)
            return;

        var slots = new List<TimeSpan>();
        var start = new TimeSpan(9, 0, 0);
        var end = new TimeSpan(17, 0, 0);
        int duration = ParseDuration(service.Durata);

        while (start + TimeSpan.FromMinutes(duration) <= end)
        {
            bool occupied = _data.ListaProgramari.Any(a =>
                a.MechanicName == mechanic.Nume &&
                a.Date.Date == DatePicker.Date &&
                start < a.Date.TimeOfDay.Add(TimeSpan.FromMinutes(GetDuration(a.ServiceName))) &&
                start + TimeSpan.FromMinutes(duration) > a.Date.TimeOfDay
            );

            if (!occupied)
                slots.Add(start);

            start = start.Add(TimeSpan.FromMinutes(30));
        }

        TimePicker.ItemsSource = slots;
    }

    private int ParseDuration(string durata)
    {
        if (string.IsNullOrWhiteSpace(durata))
            return 30;

        int total = 0;
        var parts = durata.Split(' ');

        foreach (var p in parts)
        {
            if (p.EndsWith("h") && int.TryParse(p.TrimEnd('h'), out int h))
                total += h * 60;
            else if (p.EndsWith("min") && int.TryParse(p.Replace("min", ""), out int m))
                total += m;
        }

        return total > 0 ? total : 30;
    }

    private int GetDuration(string serviceName)
    {
        var s = _data.ListaServicii.FirstOrDefault(x => x.Nume == serviceName);
        return ParseDuration(s?.Durata);
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

        var selectedDate = DatePicker.Date ?? DateTime.Today;
        var finalDate = selectedDate.Add(time);

        var appointment = new Programare
        {
            ServiceName = service.Nume,
            MechanicName = mechanic.Nume,
            Date = finalDate,
            IsCompleted = false,
            HasReview = false
        };

        await _data.AdaugaProgramareAsync(appointment);

        await DisplayAlert("Succes", "Programarea a fost creata!", "OK");
        await Shell.Current.GoToAsync("..");
    }
}