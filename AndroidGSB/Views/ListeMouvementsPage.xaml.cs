using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

/// <summary>
/// Code-behind de la page d'historique des mouvements de stock.
/// Declenche le chargement des mouvements a chaque affichage via OnAppearing.
/// </summary>
public partial class ListeMouvementsPage : ContentPage
{
    public ListeMouvementsPage()
    {
        InitializeComponent();
        BindingContext = new ListeMouvementsViewModel();
    }

    // Charge les mouvements a chaque ouverture de la page
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListeMouvementsViewModel vm)
        {
            _ = vm.ChargerMouvementsCommand.ExecuteAsync(null);
        }
    }
}
