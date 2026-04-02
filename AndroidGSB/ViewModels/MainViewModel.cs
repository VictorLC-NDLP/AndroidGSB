using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AndroidGSB.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        Title = "Gestion des échantillons GSB";
    }

    [RelayCommand]
    public async Task NavigateToAjouter()
    {
        await Shell.Current.GoToAsync("AjoutEchantillonPage");
    }

    [RelayCommand]
    public async Task NavigateToListe()
    {
        await Shell.Current.GoToAsync("ListeEchantillonsPage");
    }

    [RelayCommand]
    public async Task NavigateToMaj()
    {
        await Shell.Current.GoToAsync("MajEchantillonPage");
    }

    [RelayCommand]
    public async Task NavigateToMouvements()
    {
        await Shell.Current.GoToAsync("ListeMouvementsPage?filtre=tous");
    }

    [RelayCommand]
    public void Quitter()
    {
        Application.Current?.Quit();
    }
}


