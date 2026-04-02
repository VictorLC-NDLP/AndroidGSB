using SQLite;

namespace AndroidGSB.Data;

public static class DatabaseConstants
{
    public const string DatabaseFilename = "gsb_stock.db3";

    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}

