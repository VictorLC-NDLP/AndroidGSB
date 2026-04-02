using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

/// <summary>
/// Code-behind de la page liste des echantillons.
/// Declenche le chargement des donnees a chaque affichage de la page via OnAppearing.
/// </summary>
public partial class ListeEchantillonsPage : ContentPage
{
    public ListeEchantillonsPage()
    {
        InitializeComponent();
        BindingContext = new ListeEchantillonsViewModel();
    }

    // Appele automatiquement par MAUI a chaque fois que la page s'affiche
    // Permet de rafraichir la liste avec les donnees les plus recentes
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListeEchantillonsViewModel vm)
        {
            _ = vm.ChargerEchantillonsCommand.ExecuteAsync(null);
        }
    }
}
