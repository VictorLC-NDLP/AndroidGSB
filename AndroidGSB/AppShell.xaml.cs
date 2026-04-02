using AndroidGSB.Views;

namespace AndroidGSB;

/// <summary>
/// Shell de navigation de l'application.
/// Enregistre toutes les routes pour permettre la navigation entre les pages.
/// Le FlyoutBehavior est desactive (pas de menu lateral).
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Declaration des routes pour la navigation par URL
        Routing.RegisterRoute(nameof(AjoutEchantillonPage), typeof(AjoutEchantillonPage));
        Routing.RegisterRoute(nameof(ListeEchantillonsPage), typeof(ListeEchantillonsPage));
        Routing.RegisterRoute(nameof(MajEchantillonPage), typeof(MajEchantillonPage));
        Routing.RegisterRoute(nameof(ListeMouvementsPage), typeof(ListeMouvementsPage));
    }
}