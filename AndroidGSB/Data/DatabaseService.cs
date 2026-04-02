using AndroidGSB.Models;
using SQLite;
using System.Diagnostics;

namespace AndroidGSB.Data;

public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private bool _initialized;

    public DatabaseService()
    {
    }

    private async Task EnsureInitAsync()
    {
        if (_initialized)
            return;

        try
        {
            _database = new SQLiteAsyncConnection(
                DatabaseConstants.DatabasePath,
                DatabaseConstants.Flags);

            await _database.CreateTableAsync<Echantillon>();
            await _database.CreateTableAsync<MajStock>();

            _initialized = true;
            await InsererDonneesTestAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de l'initialisation de la base de données: {ex.Message}");
        }
    }

    public async Task InitAsync()
    {
        await EnsureInitAsync();
    }

    // Méthodes CRUD pour Echantillon
    public async Task<List<Echantillon>> GetEchantillonsAsync()
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return [];

            return await _database.Table<Echantillon>().ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la récupération des échantillons: {ex.Message}");
            return [];
        }
    }

    public async Task<Echantillon?> GetEchantillonByCodeAsync(string code)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return null;

            return await _database.Table<Echantillon>()
                .FirstOrDefaultAsync(e => e.CodeProduit == code);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la récupération de l'échantillon: {ex.Message}");
            return null;
        }
    }

    public async Task<int> InsererEchantillonAsync(Echantillon echantillon)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return -1;

            return await _database.InsertAsync(echantillon);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de l'insertion de l'échantillon: {ex.Message}");
            return -1;
        }
    }

    public async Task<int> UpdateEchantillonAsync(Echantillon echantillon)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return -1;

            return await _database.UpdateAsync(echantillon);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la mise à jour de l'échantillon: {ex.Message}");
            return -1;
        }
    }

    public async Task<int> SupprimerEchantillonAsync(string code)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return -1;

            var echantillon = await GetEchantillonByCodeAsync(code);
            if (echantillon == null)
                return -1;

            return await _database.DeleteAsync(echantillon);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la suppression de l'échantillon: {ex.Message}");
            return -1;
        }
    }

    // Gestion du stock
    public async Task<bool> AjouterStockAsync(string code, float quantite)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return false;

            var echantillon = await GetEchantillonByCodeAsync(code);
            if (echantillon == null)
                return false;

            echantillon.Stock += quantite;
            await UpdateEchantillonAsync(echantillon);

            var majStock = new MajStock
            {
                CodeProduit = code,
                Quantite = quantite,
                Mouvement = "ajout",
                Date = DateTime.Now
            };
            await _database.InsertAsync(majStock);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de l'ajout de stock: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SupprimerStockAsync(string code, float quantite)
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return false;

            var echantillon = await GetEchantillonByCodeAsync(code);
            if (echantillon == null)
                return false;

            echantillon.Stock = Math.Max(0, echantillon.Stock - quantite);
            await UpdateEchantillonAsync(echantillon);

            var majStock = new MajStock
            {
                CodeProduit = code,
                Quantite = quantite,
                Mouvement = "suppression",
                Date = DateTime.Now
            };
            await _database.InsertAsync(majStock);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la suppression de stock: {ex.Message}");
            return false;
        }
    }

    // Mouvements
    public async Task<List<MajStock>> GetMouvementsAsync()
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return [];

            return await _database.Table<MajStock>()
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la récupération des mouvements: {ex.Message}");
            return [];
        }
    }

    public async Task<List<MajStock>> GetAjoutsAsync()
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return [];

            return await _database.Table<MajStock>()
                .Where(m => m.Mouvement == "ajout")
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la récupération des ajouts: {ex.Message}");
            return [];
        }
    }

    public async Task<List<MajStock>> GetSuppressionsAsync()
    {
        try
        {
            await EnsureInitAsync();
            if (_database == null)
                return [];

            return await _database.Table<MajStock>()
                .Where(m => m.Mouvement == "suppression")
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de la récupération des suppressions: {ex.Message}");
            return [];
        }
    }

    // Données de test
    private async Task InsererDonneesTestAsync()
    {
        try
        {
            if (_database == null)
                return;

            var count = await _database.Table<Echantillon>().CountAsync();
            if (count > 0)
                return; // Les données existent déjà

            var echantillons = new List<Echantillon>
            {
                new()
                {
                    CodeProduit = "A0001",
                    LibelleProduit = "Acai Bio",
                    Description = "Riche en antioxydants",
                    Stock = 10f,
                    StockMini = 3f
                },
                new()
                {
                    CodeProduit = "A0002",
                    LibelleProduit = "Aloe Vera",
                    Description = "Apaisant et hydratant",
                    Stock = 5f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "B0001",
                    LibelleProduit = "Baobab Poudre",
                    Description = "Source de vitamine C",
                    Stock = 7f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "B0002",
                    LibelleProduit = "Bacopa Monnieri",
                    Description = "Améliore la mémoire",
                    Stock = 6f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "M0001",
                    LibelleProduit = "Moringa Bio",
                    Description = "Multivitaminé naturel",
                    Stock = 12f,
                    StockMini = 4f
                }
            };

            foreach (var echantillon in echantillons)
            {
                await _database.InsertAsync(echantillon);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de l'insertion des données de test: {ex.Message}");
        }
    }
}
