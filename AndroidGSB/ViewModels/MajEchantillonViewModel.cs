using AndroidGSB.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

/// <summary>
/// ViewModel de la page de mise a jour du stock.
/// Permet d'ajouter ou de retirer une quantite au stock d'un echantillon existant.
/// Chaque operation est tracee dans la table MajStock.
/// </summary>
public partial class MajEchantillonViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // Code du produit a modifier, saisi par l'utilisateur
    [ObservableProperty]
    private string codeProduit = string.Empty;

    // Quantite a ajouter ou retirer, saisie par l'utilisateur
    [ObservableProperty]
    private string quantiteSaisie = string.Empty;

    // Message de retour affiche apres l'operation
    [ObservableProperty]
    private string messageResultat = string.Empty;

    public MajEchantillonViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "MAJ d'un échantillon";
    }

    /// <summary>
    /// Ajoute la quantite saisie au stock du produit.
    /// Controles : code non vide, quantite positive, produit existant en base.
    /// </summary>
    [RelayCommand]
    public async Task AjouterStock()
    {
        try
        {
            IsBusy = true;

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

            // Verification que le produit existe en base
            var echantillon = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (echantillon == null)
            {
                MessageResultat = $"Erreur : Échantillon avec le code {CodeProduit} non trouvé";
                return;
            }

            var success = await _databaseService.AjouterStockAsync(CodeProduit, quantite);
            if (success)
            {
                MessageResultat = $"Quantite ajoutee pour {CodeProduit} : +{quantite}";
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

    /// <summary>
    /// Retire la quantite saisie du stock du produit.
    /// Controles supplementaires : le stock resultant ne doit pas etre negatif.
    /// </summary>
    [RelayCommand]
    public async Task SupprimerStock()
    {
        try
        {
            IsBusy = true;

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

            var echantillon = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (echantillon == null)
            {
                MessageResultat = $"Erreur : Échantillon avec le code {CodeProduit} non trouvé";
                return;
            }

            // Verification que le stock est suffisant pour la suppression
            if (echantillon.Stock < quantite)
            {
                MessageResultat = $"Erreur : Stock insuffisant (actuellement {echantillon.Stock})";
                return;
            }

            var success = await _databaseService.SupprimerStockAsync(CodeProduit, quantite);
            if (success)
            {
                MessageResultat = $"Quantite supprimee pour {CodeProduit} : -{quantite}";
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

    // Retour a la page precedente
    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}
