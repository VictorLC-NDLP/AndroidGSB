using AndroidGSB.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

public partial class MajEchantillonViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string codeProduit = string.Empty;

    [ObservableProperty]
    private string quantiteSaisie = string.Empty;

    [ObservableProperty]
    private string messageResultat = string.Empty;

    public MajEchantillonViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "MAJ d'un échantillon";
    }

    [RelayCommand]
    public async Task AjouterStock()
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

            if (!float.TryParse(QuantiteSaisie, out var quantite) || quantite <= 0)
            {
                MessageResultat = "Erreur : La quantité doit être un nombre positif";
                return;
            }

            // Vérifier que l'échantillon existe
            var echantillon = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (echantillon == null)
            {
                MessageResultat = $"Erreur : Échantillon avec le code {CodeProduit} non trouvé";
                return;
            }

            // Ajouter le stock
            var success = await _databaseService.AjouterStockAsync(CodeProduit, quantite);
            if (success)
            {
                MessageResultat = $"✓ Quantité ajoutée pour {CodeProduit} : +{quantite}";
                QuantiteSaisie = string.Empty;
            }
            else
            {
                MessageResultat = "Erreur : Impossible d'ajouter la quantité";
            }
        }
        catch (Exception ex)
        {
            MessageResultat = $"Erreur : {ex.Message}";
            Debug.WriteLine($"Erreur AjouterStock: {ex}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task SupprimerStock()
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

            if (!float.TryParse(QuantiteSaisie, out var quantite) || quantite <= 0)
            {
                MessageResultat = "Erreur : La quantité doit être un nombre positif";
                return;
            }

            // Vérifier que l'échantillon existe
            var echantillon = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (echantillon == null)
            {
                MessageResultat = $"Erreur : Échantillon avec le code {CodeProduit} non trouvé";
                return;
            }

            // Vérifier que le stock ne deviendra pas négatif
            if (echantillon.Stock < quantite)
            {
                MessageResultat = $"Erreur : Stock insuffisant (actuellement {echantillon.Stock})";
                return;
            }

            // Supprimer le stock
            var success = await _databaseService.SupprimerStockAsync(CodeProduit, quantite);
            if (success)
            {
                MessageResultat = $"✓ Quantité supprimée pour {CodeProduit} : -{quantite}";
                QuantiteSaisie = string.Empty;
            }
            else
            {
                MessageResultat = "Erreur : Impossible de supprimer la quantité";
            }
        }
        catch (Exception ex)
        {
            MessageResultat = $"Erreur : {ex.Message}";
            Debug.WriteLine($"Erreur SupprimerStock: {ex}");
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


