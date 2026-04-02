using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

/// <summary>
/// ViewModel de la page d'historique des mouvements de stock.
/// Permet d'afficher tous les mouvements ou de filtrer par type (ajouts/suppressions).
/// Le filtre peut etre passe en parametre lors de la navigation via QueryProperty.
/// </summary>
[QueryProperty(nameof(Filtre), "filtre")]
public partial class ListeMouvementsViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // Collection des mouvements affiches dans la CollectionView
    [ObservableProperty]
    private ObservableCollection<MajStock> mouvements = new();

    // Libelle du filtre actif affiche dans l'interface
    [ObservableProperty]
    private string filtreActif = "Tous";

    // Filtre recu en parametre de navigation (via l'URL "?filtre=ajouts")
    // La modification de cette propriete declenche automatiquement le filtrage
    private string _filtre = "tous";
    public string Filtre
    {
        get => _filtre;
        set
        {
            _filtre = value;
            _ = AppliquerFiltreAsync();
        }
    }

    public ListeMouvementsViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "Mouvements de stock";
    }

    // Charge tous les mouvements sans filtre
    [RelayCommand]
    public async Task ChargerMouvements()
    {
        try
        {
            IsBusy = true;
            var mouvements = await _databaseService.GetMouvementsAsync();
            Mouvements = new ObservableCollection<MajStock>(mouvements);
            FiltreActif = "Tous";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur ChargerMouvements: {ex}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Affiche uniquement les mouvements de type "ajout"
    [RelayCommand]
    public async Task FiltrerAjouts()
    {
        try
        {
            IsBusy = true;
            var mouvements = await _databaseService.GetAjoutsAsync();
            Mouvements = new ObservableCollection<MajStock>(mouvements);
            FiltreActif = "Ajouts";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur FiltrerAjouts: {ex}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Affiche uniquement les mouvements de type "suppression"
    [RelayCommand]
    public async Task FiltrerSuppressions()
    {
        try
        {
            IsBusy = true;
            var mouvements = await _databaseService.GetSuppressionsAsync();
            Mouvements = new ObservableCollection<MajStock>(mouvements);
            FiltreActif = "Suppressions";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur FiltrerSuppressions: {ex}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Raccourci pour recharger sans filtre
    [RelayCommand]
    public async Task AfficherTous()
    {
        await ChargerMouvementsCommand.ExecuteAsync(null);
    }

    // Applique le filtre recu en parametre de navigation
    private async Task AppliquerFiltreAsync()
    {
        if (_filtre == "ajouts")
        {
            await FiltrerAjoutsCommand.ExecuteAsync(null);
        }
        else if (_filtre == "suppressions")
        {
            await FiltrerSuppressionsCommand.ExecuteAsync(null);
        }
        else
        {
            await ChargerMouvementsCommand.ExecuteAsync(null);
        }
    }

    // Retour a la page precedente
    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}
