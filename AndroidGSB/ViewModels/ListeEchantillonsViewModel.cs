using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

/// <summary>
/// ViewModel de la page liste des echantillons.
/// Charge les echantillons depuis la base et les expose dans une collection observable.
/// </summary>
public partial class ListeEchantillonsViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // Collection liee a la CollectionView de l'interface
    [ObservableProperty]
    private ObservableCollection<Echantillon> echantillons = new();

    public ListeEchantillonsViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "Liste des échantillons";
    }

    // Charge tous les echantillons depuis la base SQLite et leurs composants associes
    // Appelee dans OnAppearing de la page pour rafraichir les donnees a chaque affichage
    [RelayCommand]
    public async Task ChargerEchantillons()
    {
        try
        {
            IsBusy = true;
            var echantillons = await _databaseService.GetEchantillonsAsync();

            foreach (var e in echantillons)
            {
                var composants = await _databaseService.GetComposantsByEchantillonIdAsync(e.Id);
                e.ListeComposants = composants
                    .Select(c => $"- {c.Composant.Nom} ({c.Quantite})")
                    .ToList();
            }

            Echantillons = new ObservableCollection<Echantillon>(echantillons);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur ChargerEchantillons: {ex}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // Retour a la page precedente
    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}
