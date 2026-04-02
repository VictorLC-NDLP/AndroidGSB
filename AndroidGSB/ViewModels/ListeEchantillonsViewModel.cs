using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

public partial class ListeEchantillonsViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private ObservableCollection<Echantillon> echantillons = new();

    public ListeEchantillonsViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "Liste des échantillons";
    }

    [RelayCommand]
    public async Task ChargerEchantillons()
    {
        try
        {
            IsBusy = true;
            var echantillons = await _databaseService.GetEchantillonsAsync();
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

    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}


