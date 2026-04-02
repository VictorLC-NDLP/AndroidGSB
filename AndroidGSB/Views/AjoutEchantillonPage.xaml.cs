using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

public partial class AjoutEchantillonPage : ContentPage
{
    public AjoutEchantillonPage()
    {
        InitializeComponent();
        BindingContext = new AjoutEchantillonViewModel();
    }
}

