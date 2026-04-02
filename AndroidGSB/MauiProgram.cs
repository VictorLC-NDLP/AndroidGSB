﻿using Microsoft.Extensions.Logging;
using AndroidGSB.Data;
using AndroidGSB.Views;
using AndroidGSB.ViewModels;

namespace AndroidGSB;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Enregistrer le DatabaseService comme singleton
        builder.Services.AddSingleton<DatabaseService>();

        // Enregistrer les Pages
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AjoutEchantillonPage>();
        builder.Services.AddTransient<ListeEchantillonsPage>();
        builder.Services.AddTransient<MajEchantillonPage>();
        builder.Services.AddTransient<ListeMouvementsPage>();

        // Enregistrer les ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<AjoutEchantillonViewModel>();
        builder.Services.AddTransient<ListeEchantillonsViewModel>();
        builder.Services.AddTransient<MajEchantillonViewModel>();
        builder.Services.AddTransient<ListeMouvementsViewModel>();


#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Stocker le service provider pour ServiceHelper
        ServiceHelper.Services = app.Services;

        return app;
    }
}