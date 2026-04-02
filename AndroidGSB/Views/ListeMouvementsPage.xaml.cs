using AndroidGSB.ViewModels;

namespace AndroidGSB.Views;

public partial class ListeMouvementsPage : ContentPage
{
    public ListeMouvementsPage()
    {
        InitializeComponent();
        BindingContext = new ListeMouvementsViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ListeMouvementsViewModel vm)
        {
            _ = vm.ChargerMouvementsCommand.ExecuteAsync(null);
        }
    }
}

