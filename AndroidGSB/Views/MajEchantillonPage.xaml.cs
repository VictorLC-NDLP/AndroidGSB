using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

public partial class MajEchantillonPage : ContentPage
{
    public MajEchantillonPage()
    {
        InitializeComponent();
        BindingContext = new MajEchantillonViewModel();
    }
}

