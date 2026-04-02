using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndroidGSB.Models;

[Table("Echantillons")]
public class Echantillon : ObservableObject
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string CodeProduit { get; set; } = string.Empty;

    [NotNull]
    public string LibelleProduit { get; set; } = string.Empty;

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

    private string _composition = string.Empty;
    public string Composition
    {
        get => _composition;
        set => SetProperty(ref _composition, value);
    }

    private string _complement = string.Empty;
    public string Complement
    {
        get => _complement;
        set => SetProperty(ref _complement, value);
    }

    private float _stock;
    public float Stock
    {
        get => _stock;
        set => SetProperty(ref _stock, value);
    }

    private float _stockMini;
    public float StockMini
    {
        get => _stockMini;
        set => SetProperty(ref _stockMini, value);
    }
}

