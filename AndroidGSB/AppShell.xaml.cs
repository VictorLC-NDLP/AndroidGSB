﻿using AndroidGSB.Views;

namespace AndroidGSB;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Enregistrer les routes de navigation
        Routing.RegisterRoute(nameof(AjoutEchantillonPage), typeof(AjoutEchantillonPage));
        Routing.RegisterRoute(nameof(ListeEchantillonsPage), typeof(ListeEchantillonsPage));
        Routing.RegisterRoute(nameof(MajEchantillonPage), typeof(MajEchantillonPage));
        Routing.RegisterRoute(nameof(ListeMouvementsPage), typeof(ListeMouvementsPage));
    }
}