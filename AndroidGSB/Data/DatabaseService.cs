using AndroidGSB.Models;
using SQLite;
using System.Diagnostics;

namespace AndroidGSB.Data;

/// <summary>
/// Service de persistance des donnees. Gere l'ensemble des operations
/// sur la base SQLite locale : CRUD echantillons, gestion du stock
/// et historique des mouvements. Enregistre comme singleton dans l'application.
/// </summary>
public class DatabaseService
{
    private SQLiteAsyncConnection? _database;
    private bool _initialized;

    public DatabaseService()
    {
    }

    /// <summary>
    /// Initialise la connexion et cree les tables si necessaire.
    /// Utilise un flag pour ne s'executer qu'une seule fois (initialisation paresseuse).
    /// </summary>
    private async Task EnsureInitAsync()
    {
        if (_initialized)
            return;

        try
        {
            // Ouverture de la connexion asynchrone vers le fichier SQLite
            _database = new SQLiteAsyncConnection(
                DatabaseConstants.DatabasePath,
                DatabaseConstants.Flags);

            // Creation des tables si elles n'existent pas encore
            await _database.CreateTableAsync<Echantillon>();
            await _database.CreateTableAsync<MajStock>();

            _initialized = true;

            // Insertion du jeu d'essai au premier lancement
            await InsererDonneesTestAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Erreur lors de l'initialisation de la base de donnees: {ex.Message}");
        }
    }

    // Point d'entree public pour forcer l'initialisation si besoin
    public async Task InitAsync()
    {
        await EnsureInitAsync();
    }

    // --- CRUD Echantillon ---

    // Recupere la liste complete des echantillons
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
            Debug.WriteLine($"Erreur lors de la recuperation des echantillons: {ex.Message}");
            return [];
        }
    }

    // Recherche un echantillon par son code produit
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
            Debug.WriteLine($"Erreur lors de la recuperation de l'echantillon: {ex.Message}");
            return null;
        }
    }

    // Insere un nouvel echantillon en base. Retourne le nombre de lignes affectees.
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
            Debug.WriteLine($"Erreur lors de l'insertion de l'echantillon: {ex.Message}");
            return -1;
        }
    }

    // Met a jour un echantillon existant (identifie par son Id)
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
            Debug.WriteLine($"Erreur lors de la mise a jour de l'echantillon: {ex.Message}");
            return -1;
        }
    }

    // Supprime un echantillon a partir de son code produit
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
            Debug.WriteLine($"Erreur lors de la suppression de l'echantillon: {ex.Message}");
            return -1;
        }
    }

    // --- Gestion du stock ---

    /// <summary>
    /// Ajoute une quantite au stock d'un echantillon.
    /// Met a jour la table Echantillons et insere une ligne de tracabilite dans MajStock.
    /// </summary>
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

            // Incrementation du stock
            echantillon.Stock += quantite;
            await UpdateEchantillonAsync(echantillon);

            // Enregistrement du mouvement dans l'historique
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

    /// <summary>
    /// Retire une quantite du stock d'un echantillon.
    /// Le stock ne peut pas descendre en dessous de zero (plancher a 0 avec Math.Max).
    /// Insere egalement une ligne de tracabilite dans MajStock.
    /// </summary>
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

            // Decrementation du stock avec un plancher a 0
            echantillon.Stock = Math.Max(0, echantillon.Stock - quantite);
            await UpdateEchantillonAsync(echantillon);

            // Enregistrement du mouvement dans l'historique
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

    // --- Consultation des mouvements ---

    // Recupere tous les mouvements, tries du plus recent au plus ancien
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
            Debug.WriteLine($"Erreur lors de la recuperation des mouvements: {ex.Message}");
            return [];
        }
    }

    // Recupere uniquement les mouvements de type "ajout"
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
            Debug.WriteLine($"Erreur lors de la recuperation des ajouts: {ex.Message}");
            return [];
        }
    }

    // Recupere uniquement les mouvements de type "suppression"
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
            Debug.WriteLine($"Erreur lors de la recuperation des suppressions: {ex.Message}");
            return [];
        }
    }

    // --- Jeu d'essai ---

    /// <summary>
    /// Insere des donnees de test au premier lancement de l'application.
    /// Ne s'execute que si la table Echantillons est vide.
    /// </summary>
    private async Task InsererDonneesTestAsync()
    {
        try
        {
            if (_database == null)
                return;

            var count = await _database.Table<Echantillon>().CountAsync();
            if (count > 0)
                return; // Des donnees existent deja, on ne reinitialise pas

            var echantillons = new List<Echantillon>
            {
                new()
                {
                    CodeProduit = "A0001",
                    LibelleProduit = "Acai Bio",
                    Description = "Riche en antioxydants naturels",
                    Dosage = "1 gélule par jour",
                    Composition = "Extrait d'açaí 500 mg, maltodextrine",
                    Conseils = "Prendre de préférence avec un repas",
                    Complement = "Riche en anthocyanes et oméga-9",
                    Stock = 10f,
                    StockMini = 3f
                },
                new()
                {
                    CodeProduit = "A0002",
                    LibelleProduit = "Aloe Vera",
                    Description = "Apaisant et hydratant",
                    Dosage = "2 gélules matin et soir",
                    Composition = "Gel d'aloe vera 300 mg, amidon de maïs",
                    Conseils = "À conserver au frais après ouverture",
                    Complement = "Sans conservateurs ni colorants artificiels",
                    Stock = 5f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "B0001",
                    LibelleProduit = "Baobab Poudre",
                    Description = "Source naturelle de vitamine C",
                    Dosage = "1 cuillère à café par jour (5 g)",
                    Composition = "Poudre de fruit de baobab 100 %",
                    Conseils = "Mélanger dans un yaourt ou un smoothie",
                    Complement = "Source naturelle de calcium et de fibres",
                    Stock = 7f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "B0002",
                    LibelleProduit = "Bacopa Monnieri",
                    Description = "Améliore la mémoire et la concentration",
                    Dosage = "2 gélules par jour",
                    Composition = "Extrait de Bacopa 300 mg, 50 % bacosides",
                    Conseils = "Prendre de préférence le soir au dîner",
                    Complement = "Cure de 3 mois recommandée",
                    Stock = 6f,
                    StockMini = 2f
                },
                new()
                {
                    CodeProduit = "M0001",
                    LibelleProduit = "Moringa Bio",
                    Description = "Multivitaminé naturel complet",
                    Dosage = "3 gélules par jour en une prise",
                    Composition = "Feuilles de moringa bio 400 mg",
                    Conseils = "Ne pas dépasser la dose journalière conseillée",
                    Complement = "Certifié Agriculture Biologique AB",
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
            Debug.WriteLine($"Erreur lors de l'insertion des donnees de test: {ex.Message}");
        }
    }
}
