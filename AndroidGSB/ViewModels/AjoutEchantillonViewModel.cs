using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

public partial class AjoutEchantillonViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string codeProduit = string.Empty;

    [ObservableProperty]
    private string libelleProduit = string.Empty;

    [ObservableProperty]
    private string stockSaisi = string.Empty;

    [ObservableProperty]
    private string messageResultat = string.Empty;

    public AjoutEchantillonViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "Saisie d'un échantillon";
    }

    [RelayCommand]
    public async Task AjouterEchantillon()
    {
        try
        {
            IsBusy = true;

            // Validation
            if (string.IsNullOrWhiteSpace(CodeProduit))
            {
                MessageResultat = "Erreur : Le code produit ne peut pas être vide";
                return;
            }

            if (string.IsNullOrWhiteSpace(LibelleProduit))
            {
                MessageResultat = "Erreur : Le libellé produit ne peut pas être vide";
                return;
            }

            if (!float.TryParse(StockSaisi, out var stock) || stock < 0)
            {
                MessageResultat = "Erreur : Le stock doit être un nombre positif";
                return;
            }

            // Vérifier si le code existe déjà
            var existing = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (existing != null)
            {
                MessageResultat = "Erreur : Ce code produit existe déjà";
                return;
            }

            // Créer et insérer l'échantillon
            var echantillon = new Echantillon
            {
                CodeProduit = CodeProduit,
                LibelleProduit = LibelleProduit,
                Stock = stock,
                StockMini = 2f
            };

            var result = await _databaseService.InsererEchantillonAsync(echantillon);
            if (result > 0)
            {
                MessageResultat = $"✓ Échantillon ajouté : {CodeProduit}, {LibelleProduit}, {StockSaisi}";
                // Réinitialiser les champs
                CodeProduit = string.Empty;
                LibelleProduit = string.Empty;
                StockSaisi = string.Empty;
            }
            else
            {
                MessageResultat = "Erreur : Impossible d'ajouter l'échantillon";
            }
        }
        catch (Exception ex)
        {
            MessageResultat = $"Erreur : {ex.Message}";
            Debug.WriteLine($"Erreur AjouterEchantillon: {ex}");
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


