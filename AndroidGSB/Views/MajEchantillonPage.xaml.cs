using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

/// <summary>
/// Code-behind de la page de mise a jour du stock.
/// Associe le ViewModel correspondant pour le data binding.
/// </summary>
public partial class MajEchantillonPage : ContentPage
{
    public MajEchantillonPage()
    {
        InitializeComponent();
        BindingContext = new MajEchantillonViewModel();
    }
}
