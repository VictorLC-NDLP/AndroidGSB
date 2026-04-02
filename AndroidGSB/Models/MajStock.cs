using SQLite;

namespace AndroidGSB.Models;

/// <summary>
/// Modele representant un mouvement de stock (ajout ou suppression).
/// Chaque modification de stock genere une ligne dans cette table
/// pour garder un historique complet des operations.
/// </summary>
[Table("MajStock")]
public class MajStock
{
    // Cle primaire auto-incrementee
    [PrimaryKey]
    [AutoIncrement]
    public int Cle { get; set; }

    // Code du produit concerne par le mouvement
    public string CodeProduit { get; set; } = string.Empty;

    // Quantite ajoutee ou retiree
    public float Quantite { get; set; }

    // Type de mouvement : "ajout" ou "suppression"
    public string Mouvement { get; set; } = string.Empty;

    // Date et heure du mouvement
    public DateTime Date { get; set; }
}
