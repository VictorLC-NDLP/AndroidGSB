using AndroidGSB.ViewModels;

namespace AndroidGSB;

/// <summary>
/// Code-behind de la page d'accueil.
/// Associe le MainViewModel comme contexte de donnees pour le data binding.
/// </summary>
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
}
