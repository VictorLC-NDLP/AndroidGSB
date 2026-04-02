using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

[QueryProperty(nameof(Filtre), "filtre")]
public partial class ListeMouvementsViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private ObservableCollection<MajStock> mouvements = new();

    [ObservableProperty]
    private string filtreActif = "Tous";

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

    [RelayCommand]
    public async Task AfficherTous()
    {
        await ChargerMouvementsCommand.ExecuteAsync(null);
    }

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

    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}


