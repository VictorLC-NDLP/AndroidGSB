# ⚙️ Guide de configuration et développement

## 🔧 Prérequis de développement

### Environnement obligatoire

- **JetBrains Rider** 2024.1 ou supérieur
- **.NET 10.0** SDK
- **Android SDK** (pour compilation Android)
  - Android API 21 (Android 5.0) minimum
  - Android SDK tools à jour

### Vérification des prérequis

```bash
# Vérifier .NET
dotnet --version

# Lister les SDK MAUI disponibles
dotnet workload list

# Installer MAUI si nécessaire
dotnet workload install maui
```

---

## 📂 Structure des dossiers

```
/AndroidGSB/
├── /AndroidGSB/               ← Projet principal
│   ├── /bin/                  ← Fichiers compilés
│   ├── /obj/                  ← Fichiers objets
│   ├── /Platforms/
│   │   └── /Android/          ← Configuration Android spécifique
│   ├── /Models/               ← Modèles de données
│   ├── /Data/                 ← Services de données
│   ├── /ViewModels/           ← ViewModels MVVM
│   ├── /Views/                ← Pages XAML
│   ├── /Converters/           ← Convertisseurs de valeurs
│   ├── /Resources/
│   │   ├── /AppIcon/
│   │   ├── /Fonts/
│   │   ├── /Images/
│   │   ├── /Splash/
│   │   └── /Styles/
│   ├── AndroidGSB.csproj      ← Configuration du projet
│   └── MauiProgram.cs         ← Bootstrapping MAUI
├── AndroidGSB.slnx            ← Solution
└── README.md                  ← Documentation générale
```

---

## 🚀 Compilation et déploiement

### Mode Debug

```bash
# Build pour Android (debug)
dotnet build -c Debug -f net10.0-android

# Ou depuis Rider
# Build > Build Solution
```

### Mode Release

```bash
# Build optimisé pour Android
dotnet build -c Release -f net10.0-android
```

### Générer APK (installation directe)

```bash
# APK non signé
dotnet publish -c Release -f net10.0-android \
  -p:AndroidPackageFormat=apk \
  -p:AndroidKeyStore=false

# Résultat : bin/Release/net10.0-android/com.companyname.androidgsb.apk
```

### Générer AAB (Google Play Store)

```bash
# Android App Bundle
dotnet publish -c Release -f net10.0-android \
  -p:AndroidPackageFormat=aab

# Résultat : bin/Release/net10.0-android/com.companyname.androidgsb.aab
```

### Installation sur émulateur/appareil

```bash
# Installer APK
adb install bin/Release/net10.0-android/com.companyname.androidgsb.apk

# Ou depuis Rider
# Run > Select Device > Run AndroidGSB
```

---

## 🐛 Débogage

### Logs et Debug.WriteLine()

Tous les `Debug.WriteLine()` apparaissent dans :
- **Rider** : Onglet "Debug" → "Android Logcat"
- **Visual Studio** : Fenêtre "Output"
- **Terminal** : `adb logcat`

### Inspection de la base de données

#### Option 1 : DB Browser for SQLite

1. Télécharger depuis https://sqlitebrowser.org/
2. Ouvrir le fichier : `/data/data/com.companyname.androidgsb/files/gsb_stock.db3`
3. Consulter les tables
4. Modifier les données directement

#### Option 2 : Plugin Database Navigator (Rider)

1. Installer le plugin dans Rider
2. View > Tool Windows > Database
3. Connecter à `/data/data/com.companyname.androidgsb/files/gsb_stock.db3`
4. Explorer et exécuter des requêtes SQL

#### Option 3 : Via ADB

```bash
# Copier la BD sur le PC
adb pull /data/data/com.companyname.androidgsb/files/gsb_stock.db3

# Ouvrir avec DB Browser
```

### Émulateur Android

#### Configuration dans Rider

1. **View** → **Tool Windows** → **Device Manager**
2. Créer un nouvel AVD (Android Virtual Device)
3. Sélectionner Android 5.0 (API 21) minimum
4. **Run** → **Edit Configurations** → Sélectionner l'AVD
5. Lancer l'app

#### Problèmes courants

| Problème | Solution |
|----------|----------|
| Émulateur lent | Activer GPU acceleration dans AVD settings |
| App crash au démarrage | Vérifier les logs via `adb logcat` |
| BD vide | Supprimer l'app et réinstaller |
| Port 5555 déjà utilisé | Redémarrer le service ADB |

---

## 📝 Convention de code

### Nommage

| Élément | Convention | Exemple |
|---------|-----------|---------|
| Namespace | PascalCase | `AndroidGSB.ViewModels` |
| Classe | PascalCase | `AjoutEchantillonViewModel` |
| Propriété publique | PascalCase | `CodeProduit` |
| Propriété privée | _camelCase | `_databaseService` |
| Constante | UPPER_SNAKE_CASE | `DATABASE_FILENAME` |
| Paramètre | camelCase | `codeProduit` |
| Variable locale | camelCase | `result` |

### Patterns

#### ObservableProperty (MVVM Toolkit)

```csharp
[ObservableProperty]
private string title = string.Empty;

// Génère automatiquement :
// - Property publique Title
// - INotifyPropertyChanged
// - SetProperty()
```

#### RelayCommand (MVVM Toolkit)

```csharp
[RelayCommand]
public async Task AjouterEchantillon()
{
    // Génère automatiquement AjouterEchantillonCommand
}
```

#### Try/Catch avec Debug

```csharp
try
{
    // Opération
}
catch (Exception ex)
{
    Debug.WriteLine($"Erreur : {ex.Message}");
    MessageResultat = $"Erreur : {ex.Message}";
}
```

---

## 🔀 Gestion des erreurs

### Erreurs attendues

- **Validation métier** : Affichage utilisateur dans `MessageResultat`
- **Erreurs BD** : Logging + message générique

### Exemple : AjoutEchantillonViewModel

```csharp
[RelayCommand]
public async Task AjouterEchantillon()
{
    try
    {
        IsBusy = true;

        // Validations
        if (string.IsNullOrWhiteSpace(CodeProduit))
        {
            MessageResultat = "Erreur : Code vide";
            return;
        }

        // Opération BD
        var result = await _databaseService.InsererEchantillonAsync(echantillon);
        
        if (result > 0)
        {
            MessageResultat = "✓ Succès";
        }
        else
        {
            MessageResultat = "Erreur BD";
        }
    }
    catch (Exception ex)
    {
        MessageResultat = $"Erreur : {ex.Message}";
        Debug.WriteLine($"Erreur AjouterEchantillon: {ex}");
    }
    finally
    {
        IsBusy = false;
    }
}
```

---

## 📦 Gestion des dépendances

### Injection de dépendances (DI)

Configurée dans `MauiProgram.cs` :

```csharp
// Singleton (une instance pour tout l'app)
builder.Services.AddSingleton<DatabaseService>();

// Transient (nouvelle instance à chaque fois)
builder.Services.AddTransient<AjoutEchantillonPage>();
builder.Services.AddTransient<AjoutEchantillonViewModel>();
```

### Récupération d'un service

```csharp
var databaseService = ServiceHelper.GetService<DatabaseService>();
```

---

## 🔄 Flux de données

### Ajout de produit (flux complet)

```
MainPage.xaml
    ↓
Command NavigateToAjouterCommand
    ↓
Shell.GoToAsync("AjoutEchantillonPage")
    ↓
AjoutEchantillonPage.xaml.cs
    ↓ BindingContext
AjoutEchantillonViewModel
    ↓
Entry (CodeProduit) ←→ {Binding CodeProduit}
    ↓
Button "Ajouter"
    ↓
Command AjouterEchantillonCommand
    ↓
DatabaseService.InsererEchantillonAsync()
    ↓
SQLiteAsyncConnection.InsertAsync()
    ↓
table "Echantillons" (BD SQLite)
    ↓
MessageResultat = "✓ ..."
    ↓
Label {Binding MessageResultat}
```

---

## 🧪 Tests unitaires

### Structure pour ajouter des tests

Créer un projet `AndroidGSB.Tests` :

```csharp
// DatabaseServiceTests.cs
[TestClass]
public class DatabaseServiceTests
{
    private DatabaseService _service;

    [TestInitialize]
    public void Setup()
    {
        _service = new DatabaseService();
    }

    [TestMethod]
    public async Task InsererEchantillon_Success()
    {
        // Arrange
        var echantillon = new Echantillon { ... };

        // Act
        var result = await _service.InsererEchantillonAsync(echantillon);

        // Assert
        Assert.IsTrue(result > 0);
    }
}
```

---

## 📱 Permissions Android

Actuellement **aucune permission spéciale** requise :
- FileSystem.AppDataDirectory gère l'accès au stockage automatiquement
- SQLite fonctionne sans permission additionnelle

**Si besoin futur** : Ajouter dans `AndroidManifest.xml` :

```xml
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

---

## 🔐 Sécurité

### Points d'attention

1. **Paramètres SQL** : Utiliser SQLite-net pour éviter les injections
2. **Données sensibles** : Aucune données critique ne sont stockées
3. **Logs** : Pas d'affichage de données sensibles en production

### Bonnes pratiques appliquées

✅ Utilisation de `SQLiteAsyncConnection` paramétrée  
✅ Validation de toutes les entrées utilisateur  
✅ Try/catch sur les opérations BD  
✅ Aucun hardcodage de chemins sensitifs

---

## 📊 Performance

### Optimisations appliquées

- **Async/Await** : Opérations BD non-bloquantes
- **CollectionView** : Virtualisation automatique des listes
- **Lazy loading** : Chargement à la demande
- **Singleton BD** : Une seule connexion partagée

### Recommandations

- Pour > 1000 produits : Ajouter pagination dans `ListeEchantillonsPage`
- Pour requêtes complexes : Ajouter indexation dans la BD

---

## 🔄 Git / Version control

### .gitignore recommandé

```
bin/
obj/
*.csproj.user
*.apk
*.aab
.DS_Store
.idea/
gsb_stock.db3
```

### Commits recommandés

```
feat: Ajouter page d'ajout de produit
fix: Corriger validation du code produit
refactor: Simplifier logique de MAJ stock
test: Ajouter tests DatabaseService
docs: Mettre à jour le README
```

---

## 📚 Ressources utiles

### Documentation officielle

- [MAUI Docs](https://learn.microsoft.com/en-us/dotnet/maui/)
- [MVVM Toolkit](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/)
- [SQLite-net](https://github.com/praeclarum/sqlite-net)
- [Android Docs](https://developer.android.com/)

### Tutoriels pratiques

- Shell navigation : https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/
- Data binding : https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/
- Collections : https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/collectionview/

---

## ✅ Checklist avant la production

- [ ] Compiler en Release mode
- [ ] Tester sur plusieurs appareils/émulateurs
- [ ] Valider tous les messages d'erreur
- [ ] Vérifier la BD ne contient pas de données sensibles
- [ ] Mettre à jour le numéro de version
- [ ] Signer l'APK/AAB avec clé de production
- [ ] Tester l'installation via APK
- [ ] Valider les permissions requises
- [ ] Documenter les changements

---

**Version** : 1.0  
**Dernière mise à jour** : Mars 2026  
**Environnement** : JetBrains Rider + Android SDK

