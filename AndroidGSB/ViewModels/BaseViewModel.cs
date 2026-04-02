using CommunityToolkit.Mvvm.ComponentModel;

namespace AndroidGSB.ViewModels;

/// <summary>
/// Classe de base pour tous les ViewModels de l'application.
/// Fournit les proprietes communes IsBusy (chargement en cours) et Title.
/// Herite de ObservableObject du CommunityToolkit pour le data binding.
/// </summary>
public abstract class BaseViewModel : ObservableObject
{
    // Indique si une operation asynchrone est en cours (affiche un indicateur de chargement)
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    // Titre affiche dans la barre de navigation de la page
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    // Propriete inverse de IsBusy, utile pour activer/desactiver des elements dans l'interface
    public bool IsNotBusy => !IsBusy;
}
