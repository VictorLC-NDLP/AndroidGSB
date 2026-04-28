using SQLite;

namespace AndroidGSB.Models;

// Table de liaison N:N entre Echantillon et Composant
[Table("Contient")]
public class Contient
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    public int EchantillonId { get; set; }
    public int ComposantId { get; set; }

    // Quantité du composant dans le produit (ex: "500 mg", "100 %")
    public string Quantite { get; set; } = string.Empty;
}
