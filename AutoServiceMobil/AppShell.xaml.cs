using AutoServiceMobil.Views;

namespace AutoServiceMobil;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(AppointmentsPage), typeof(AppointmentsPage));
        Routing.RegisterRoute(nameof(CreateAppointmentPage), typeof(CreateAppointmentPage));

        Routing.RegisterRoute(nameof(AdminMecaniciPage), typeof(AdminMecaniciPage));
        Routing.RegisterRoute(nameof(AddMecanicPage), typeof(AddMecanicPage));
        Routing.RegisterRoute(nameof(EditMecanicPage), typeof(EditMecanicPage));

        Routing.RegisterRoute(nameof(EditAppointmentPage), typeof(EditAppointmentPage));
        Routing.RegisterRoute(nameof(MechanicDetailPage), typeof(MechanicDetailPage));
        Routing.RegisterRoute(nameof(LeaveReviewPage), typeof(LeaveReviewPage));
    }
}