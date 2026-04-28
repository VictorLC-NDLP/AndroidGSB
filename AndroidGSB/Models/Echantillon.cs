using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndroidGSB.Models;

/// <summary>
/// Modele representant un echantillon de complement alimentaire.
/// Mappe sur la table SQLite "Echantillons".
/// Herite de ObservableObject pour notifier l'interface lors de changements de valeur.
/// </summary>
[Table("Echantillons")]
public class Echantillon : ObservableObject
{
    // Cle primaire auto-incrementee geree par SQLite
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    // Code unique du produit (ex: "A0001")
    [NotNull]
    public string CodeProduit { get; set; } = string.Empty;

    // Nom du produit affiche dans les listes
    [NotNull]
    public string LibelleProduit { get; set; } = string.Empty;

    // Les proprietes suivantes utilisent SetProperty pour declencher
    // la notification INotifyPropertyChanged et mettre a jour l'UI automatiquement

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _conseils = string.Empty;
    public string Conseils
    {
        get => _conseils;
        set => SetProperty(ref _conseils, value);
    }

    private string _dosage = string.Empty;
    public string Dosage
    {
        get => _dosage;
        set => SetProperty(ref _dosage, value);
    }

    private string _complement = string.Empty;
    public string Complement
    {
        get => _complement;
        set => SetProperty(ref _complement, value);
    }

    // Quantite actuellement en stock
    private float _stock;
    public float Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }

    // Seuil minimum de stock (alerte si le stock passe en dessous)
    private float _stockMini;
    public float StockMini
    {
        get => _stockMini;
        set => SetProperty(ref _stockMini, value);
    }
}
