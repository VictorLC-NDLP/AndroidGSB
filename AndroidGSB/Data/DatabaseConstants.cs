using SQLite;

namespace AndroidGSB.Data;

/// <summary>
/// Constantes de configuration pour la connexion SQLite.
/// Centralise le nom du fichier, les flags d'ouverture et le chemin de stockage.
/// </summary>
public static class DatabaseConstants
{
    // Nom du fichier de base de donnees stocke sur l'appareil
    public const string DatabaseFilename = "gsb_stock.db3";

    // Options d'ouverture : lecture/ecriture, creation automatique, cache partage
    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    // Chemin complet vers le fichier, dans le repertoire prive de l'application
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
