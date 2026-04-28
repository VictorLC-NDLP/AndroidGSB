using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace AndroidGSB.Models;

[Table("Composants")]
public class Composant : ObservableObject
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Nom { get; set; } = string.Empty;

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }
}
