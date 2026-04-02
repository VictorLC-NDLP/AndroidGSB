using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

public partial class ListeEchantillonsPage : ContentPage
{
    public ListeEchantillonsPage()
    {
        InitializeComponent();
        BindingContext = new ListeEchantillonsViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListeEchantillonsViewModel vm)
        {
            _ = vm.ChargerEchantillonsCommand.ExecuteAsync(null);
        }
    }
}

