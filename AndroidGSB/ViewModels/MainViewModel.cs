using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AndroidGSB.ViewModels;

/// <summary>
/// ViewModel de la page d'accueil. Gere la navigation vers les differentes
/// fonctionnalites de l'application via les commandes liees aux boutons.
/// </summary>
public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        Title = "Gestion des échantillons GSB";
    }

    // Navigation vers la page d'ajout d'un echantillon
    [RelayCommand]
    public async Task NavigateToAjouter()
    {
        await Shell.Current.GoToAsync("AjoutEchantillonPage");
    }

    // Navigation vers la liste des echantillons
    [RelayCommand]
    public async Task NavigateToListe()
    {
        await Shell.Current.GoToAsync("ListeEchantillonsPage");
    }

    // Navigation vers la page de mise a jour du stock
    [RelayCommand]
    public async Task NavigateToMaj()
    {
        await Shell.Current.GoToAsync("MajEchantillonPage");
    }

    // Navigation vers l'historique des mouvements (avec filtre par defaut "tous")
    [RelayCommand]
    public async Task NavigateToMouvements()
    {
        await Shell.Current.GoToAsync("ListeMouvementsPage?filtre=tous");
    }

    // Ferme l'application
    [RelayCommand]
    public void Quitter()
    {
        Application.Current?.Quit();
    }
}
