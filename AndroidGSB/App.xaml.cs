using Microsoft.Extensions.DependencyInjection;

namespace AndroidGSB;

/// <summary>
/// Classe principale de l'application MAUI.
/// Cree la fenetre racine contenant le Shell de navigation.
/// </summary>
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    // Cree la fenetre principale au lancement, avec AppShell comme contenu
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
