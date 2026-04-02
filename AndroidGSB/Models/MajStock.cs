using SQLite;

namespace AndroidGSB.Models;

[Table("MajStock")]
public class MajStock
{
    [PrimaryKey]
    [AutoIncrement]
    public int Cle { get; set; }

    public string CodeProduit { get; set; } = string.Empty;

    public float Quantite { get; set; }

    public string Mouvement { get; set; } = string.Empty; // "ajout" ou "suppression"

    public DateTime Date { get; set; }
}

