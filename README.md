# Application GSB Stock Échantillons - .NET MAUI

## 📋 Description

Application mobile Android développée avec **.NET MAUI** (C#) permettant aux commerciaux GSB de gérer leur stock personnel d'échantillons de compléments alimentaires.

**Technologie** : .NET MAUI 10.0 avec SQLite pour la persistance locale  
**Base de données** : SQLite (sqlite-net-pcl)  
**Pattern MVVM** : CommunityToolkit.Mvvm  
**Plateforme cible** : Android 5.0+ (API 21)

---

## 📁 Structure du projet

```
AndroidGSB/
├── Models/
│   ├── Echantillon.cs          # Modèle produit avec propriétés observables
│   └── MajStock.cs             # Modèle mouvement de stock
│
├── Data/
│   ├── DatabaseService.cs      # Service d'accès aux données SQLite
│   ├── DatabaseConstants.cs    # Constantes de configuration BD
│   └── ServiceHelper.cs        # Utilitaire d'injection de dépendances
│
├── ViewModels/
│   ├── BaseViewModel.cs        # Base MVVM avec pattern RelayCommand
│   ├── MainViewModel.cs        # Page d'accueil - navigation
│   ├── AjoutEchantillonViewModel.cs    # Saisie nouveau produit
│   ├── ListeEchantillonsViewModel.cs   # Affichage liste produits
│   ├── MajEchantillonViewModel.cs      # Ajout/suppression stock
│   └── ListeMouvementsViewModel.cs     # Historique mouvements
│
├── Views/
│   ├── MainPage.xaml / .cs              # Accueil avec 5 boutons de navigation
│   ├── AjoutEchantillonPage.xaml / .cs  # Saisie produit
│   ├── ListeEchantillonsPage.xaml / .cs # Liste scrollable
│   ├── MajEchantillonPage.xaml / .cs    # Maj stock
│   └── ListeMouvementsPage.xaml / .cs   # Historique avec filtres
│
├── Converters/
│   └── MouvementColorConverter.cs       # Colorisation mouvements
│
├── Resources/
│   └── Styles/
│       └── Colors.xaml                  # Palette GSB sombre
│
├── App.xaml / App.xaml.cs              # Configuration app
├── AppShell.xaml / AppShell.xaml.cs    # Routes de navigation
├── MainPage.xaml / MainPage.xaml.cs    # Page d'accueil
├── MauiProgram.cs                      # Configuration DI et init BD
└── AndroidGSB.csproj                   # Configuration projet
```

---

## 🗄️ Modèles de données

### Echantillon.cs
- **Id** (clé primaire, auto-incrémentée)
- **CodeProduit** (notNull) - ex: "A0001"
- **LibelleProduit** (notNull)
- **Description**
- **Conseils**
- **Dosage**
- **Composition**
- **Complement**
- **Stock** (float)
- **StockMini** (float) - seuil d'alerte

**Table** : `Echantillons`

### MajStock.cs
- **Cle** (clé primaire, auto-incrémentée)
- **CodeProduit**
- **Quantite** (float)
- **Mouvement** ("ajout" | "suppression")
- **Date** (DateTime)

**Table** : `MajStock`

---

## 🔧 Services

### DatabaseService
Singleton gérant toutes les opérations CRUD :

**Méthodes Echantillon:**
- `GetEchantillonsAsync()` - Récupère tous les produits
- `GetEchantillonByCodeAsync(code)` - Recherche par code
- `InsererEchantillonAsync(echantillon)` - Créer
- `UpdateEchantillonAsync(echantillon)` - Mettre à jour
- `SupprimerEchantillonAsync(code)` - Supprimer

**Gestion Stock:**
- `AjouterStockAsync(code, quantite)` - Incrémente stock + historique
- `SupprimerStockAsync(code, quantite)` - Décrémente stock + historique

**Mouvements:**
- `GetMouvementsAsync()` - Tous les mouvements
- `GetAjoutsAsync()` - Filtre ajouts
- `GetSuppressionsAsync()` - Filtre suppressions

**Initialisation:**
- `InitAsync()` - Crée les tables et insère données de test

---

## 🧠 ViewModels (MVVM)

Tous héritent de `BaseViewModel` qui expose :
- `IsBusy` (bool) - Indicateur d'activité
- `IsNotBusy` (bool) - Propriété calculée
- `Title` (string)

### MainViewModel
Commandes RelayCommand :
- `NavigateToAjouterCommand`
- `NavigateToListeCommand`
- `NavigateToMajCommand`
- `NavigateToMouvementsCommand`
- `QuitterCommand`

### AjoutEchantillonViewModel
Propriétés observables :
- `CodeProduit`
- `LibelleProduit`
- `StockSaisi`
- `MessageResultat`

Commandes :
- `AjouterEchantillonCommand` - Validation et insertion
- `QuitterCommand`

### ListeEchantillonsViewModel
- `Echantillons` (ObservableCollection<Echantillon>)
- `ChargerEchantillonsCommand`
- `QuitterCommand`

### MajEchantillonViewModel
Propriétés :
- `CodeProduit`
- `QuantiteSaisie`
- `MessageResultat`

Commandes :
- `AjouterStockCommand`
- `SupprimerStockCommand`
- `QuitterCommand`

### ListeMouvementsViewModel
Propriétés :
- `Mouvements` (ObservableCollection<MajStock>)
- `FiltreActif` (string)

Commandes :
- `ChargerMouvementsCommand`
- `FiltrerAjoutsCommand`
- `FiltrerSuppressionsCommand`
- `AfficherTousCommand`
- `QuitterCommand`

Support de QueryProperty pour le filtre via URL

---

## 🎨 Design

### Palette GSB
```
PrimaryColor     = #1a1a2e   (bleu nuit)
SecondaryColor   = #16213e   (bleu profond)
AccentColor      = #0f3460   (bleu moyen)
HighlightColor   = #e94560   (rose/rouge GSB)
TextPrimary      = #ffffff
TextSecondary    = #a8b2d8
SuccessColor     = #4caf50   (vert)
DangerColor      = #f44336   (rouge)
```

### Composants
- Boutons arrondis (CornerRadius=8)
- Fond sombre (#1a1a2e)
- CollectionView pour listes scrollables
- ActivityIndicator pour opérations async

---

## 📦 Packages NuGet

```xml
<PackageReference Include="sqlite-net-pcl" Version="1.9.172"/>
<PackageReference Include="SQLitePCLRaw.provider.dynamic_cdecl" Version="2.1.6"/>
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2"/>
<PackageReference Include="CommunityToolkit.Maui" Version="7.0.1"/>
```

---

## 🎯 Fonctionnalités principales

### 1. Page d'accueil (MainPage)
5 boutons de navigation :
- Ajouter un nouvel échantillon
- Liste des échantillons
- Maj d'un échantillon
- Liste des ajouts
- Liste des suppressions

### 2. Ajout de produit
- Saisie : Code, Libellé, Stock initial
- Validation : champs non vides, unicité du code, format numérique
- Message de confirmation avec les données saisies

### 3. Liste des produits
- CollectionView avec affichage Code, Libellé, Stock, StockMini
- Fond gris foncé (#16213e)
- Rechargement au OnAppearing()

### 4. Maj de stock
- Recherche par code
- Deux boutons : Ajouter / Supprimer quantité
- Validation : stock ne peut pas être négatif
- Enregistrement automatique dans MajStock

### 5. Historique des mouvements
- 3 boutons filtre : Tous / Ajouts / Suppressions
- Affichage : Code, Quantité, Mouvement (coloré), Date
- Tri par date décroissante
- Support QueryProperty pour pré-charger un filtre

---

## 🔀 Navigation (Shell)

Routes enregistrées dans AppShell.xaml.cs :
```csharp
Routing.RegisterRoute(nameof(AjoutEchantillonPage), typeof(AjoutEchantillonPage));
Routing.RegisterRoute(nameof(ListeEchantillonsPage), typeof(ListeEchantillonsPage));
Routing.RegisterRoute(nameof(MajEchantillonPage), typeof(MajEchantillonPage));
Routing.RegisterRoute(nameof(ListeMouvementsPage), typeof(ListeMouvementsPage));
```

Appels de navigation :
```csharp
await Shell.Current.GoToAsync("AjoutEchantillonPage");
await Shell.Current.GoToAsync("ListeMouvementsPage?filtre=ajouts");
```

---

## ⚙️ Configuration

### MauiProgram.cs
- Enregistrement du DatabaseService comme singleton
- Enregistrement de toutes les Pages et ViewModels en transient
- Appel à InitAsync() pour créer les tables et données de test
- Configuration CommunityToolkit.Maui

### AppShell.xaml
- FlyoutBehavior="Disabled" (pas de menu latéral)
- ShellContent point vers MainPage

---

## 🧪 Données de test

Automatiquement insérées au démarrage si la table est vide :

```
A0001 - Acai Bio         - Stock: 10, Mini: 3
A0002 - Aloe Vera        - Stock: 5,  Mini: 2
B0001 - Baobab Poudre    - Stock: 7,  Mini: 2
B0002 - Bacopa Monnieri  - Stock: 6,  Mini: 2
M0001 - Moringa Bio      - Stock: 12, Mini: 4
```

---

## 🚀 Déploiement

### Compilation
```bash
dotnet build -c Release -f net10.0-android
```

### Fichier APK
```bash
dotnet publish -c Release -f net10.0-android -p:AndroidPackageFormat=apk
```

### Fichier AAB (Play Store)
```bash
dotnet publish -c Release -f net10.0-android -p:AndroidPackageFormat=aab
```

### Résultats
- APK : `bin/Release/net10.0-android/com.companyname.androidgsb.apk`
- AAB : `bin/Release/net10.0-android/com.companyname.androidgsb.aab`

---

## 📝 Notes de développement

1. **Permissions Android** : L'accès aux fichiers est géré automatiquement par FileSystem.AppDataDirectory
2. **BD SQLite** : Stockée dans `/data/data/com.companyname.androidgsb/files/gsb_stock.db3`
3. **Async/Await** : Toutes les opérations BD sont asynchrones
4. **Gestion erreurs** : Try/catch avec messages utilisateur appropriés
5. **Validation métier** :
   - Code unique pour chaque produit
   - Stock ne peut pas être négatif
   - Quantités doivent être positives

---

## 🔍 Debugging

### Console de débogage
Tous les Debug.WriteLine() sortent dans la fenêtre de débogage de Rider

### Inspection BD
Utiliser "DB Browser for SQLite" ou le plugin "Database Navigator" de Rider

### Émulateur Android
Configuration des Run dans Rider : Run > Edit Configurations > Sélectionner AVD

---

## ✅ Checklist d'implémentation

- [x] Modèles de données (Echantillon, MajStock)
- [x] Service de base de données avec CRUD complet
- [x] ViewModels avec RelayCommands
- [x] Pages XAML avec data binding
- [x] Navigation Shell
- [x] Validation métier
- [x] Données de test
- [x] Design thème GSB sombre
- [x] Support async/await
- [x] Gestion erreurs
- [x] Historique mouvements avec filtres
- [x] Convertisseur de couleurs personnalisé

---

**Version** : 1.0  
**Date** : Mars 2026  
**Auteur** : GitHub Copilot  
**Cible** : SIO 2ème année SLAM - BLOC2 GSB

