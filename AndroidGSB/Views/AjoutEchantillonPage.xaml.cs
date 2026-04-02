using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

/// <summary>
/// Code-behind de la page d'ajout d'echantillon.
/// Associe le ViewModel correspondant pour le data binding.
/// </summary>
public partial class AjoutEchantillonPage : ContentPage
{
    public AjoutEchantillonPage()
    {
        InitializeComponent();
        BindingContext = new AjoutEchantillonViewModel();
    }
}
