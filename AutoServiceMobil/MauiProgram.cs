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

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<RegisterPage>();
        builder.Services.AddSingleton<AppointmentsPage>();
        builder.Services.AddSingleton<CreateAppointmentPage>();
        builder.Services.AddSingleton<AdminMecaniciPage>();

        builder.Services.AddSingleton<AdminMecaniciPage>();
        builder.Services.AddTransient<AddMecanicPage>();
        builder.Services.AddTransient<EditMecanicPage>();

        return builder.Build();
    }
}