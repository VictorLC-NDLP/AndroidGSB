using AndroidGSB.Data;
using AndroidGSB.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace AndroidGSB.ViewModels;

/// <summary>
/// ViewModel du formulaire d'ajout d'un echantillon.
/// Gere la saisie, la validation et l'insertion en base d'un nouvel echantillon.
/// </summary>
public partial class AjoutEchantillonViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // Champs de saisie lies aux Entry de la vue via data binding
    [ObservableProperty]
    private string codeProduit = string.Empty;

    [ObservableProperty]
    private string libelleProduit = string.Empty;

    [ObservableProperty]
    private string stockSaisi = string.Empty;

    // Message affiche a l'utilisateur apres une tentative d'ajout (succes ou erreur)
    [ObservableProperty]
    private string messageResultat = string.Empty;

    public AjoutEchantillonViewModel()
    {
        _databaseService = ServiceHelper.GetService<DatabaseService>()!;
        Title = "Saisie d'un échantillon";
    }

    /// <summary>
    /// Valide les champs saisis puis insere l'echantillon en base.
    /// Controles effectues : champs non vides, stock numerique positif, code unique.
    /// </summary>
    [RelayCommand]
    public async Task AjouterEchantillon()
    {
        try
        {
            IsBusy = true;

            // Controle champ code produit
            if (string.IsNullOrWhiteSpace(CodeProduit))
            {
                MessageResultat = "Erreur : Le code produit ne peut pas être vide";
                return;
            }

            // Controle champ libelle
            if (string.IsNullOrWhiteSpace(LibelleProduit))
            {
                MessageResultat = "Erreur : Le libellé produit ne peut pas être vide";
                return;
            }

            // Controle format et valeur du stock
            if (!float.TryParse(StockSaisi, out var stock) || stock < 0)
            {
                MessageResultat = "Erreur : Le stock doit être un nombre positif";
                return;
            }

            // Verification d'unicite du code produit en base
            var existing = await _databaseService.GetEchantillonByCodeAsync(CodeProduit);
            if (existing != null)
            {
                MessageResultat = "Erreur : Ce code produit existe déjà";
                return;
            }

            // Creation de l'objet et insertion en base
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
                MessageResultat = $"Echantillon ajoute : {CodeProduit}, {LibelleProduit}, {StockSaisi}";

                // Reinitialisation des champs apres un ajout reussi
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

    // Retour a la page precedente
    [RelayCommand]
    public async Task Quitter()
    {
        await Shell.Current.GoToAsync("..");
    }
}
