using AutoServiceMobil;
using AutoServiceMobil.Services;
using AutoServiceMobil.Views;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<AppointmentsPage>();
        builder.Services.AddTransient<CreateAppointmentPage>();
        builder.Services.AddTransient<AdminMecaniciPage>();
        builder.Services.AddTransient<AddMecanicPage>();
        builder.Services.AddTransient<EditMecanicPage>();
        builder.Services.AddTransient<EditAppointmentPage>();
        builder.Services.AddTransient<MechanicDetailPage>();
        builder.Services.AddTransient<LeaveReviewPage>();

        return builder.Build();
    }
}