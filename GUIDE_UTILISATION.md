# 📖 Guide d'utilisation - Application GSB Stock Échantillons

## 🚀 Démarrage rapide

### Première utilisation

1. **Lancer l'application**
   - L'app affiche la page d'accueil avec 5 boutons principaux
   - Les données de test sont automatiquement créées au premier lancement

### Page d'accueil

**5 boutons de navigation** :

#### 1️⃣ "Ajouter un nouvel échantillon"
Permet d'enregistrer un nouveau produit en stock.

**Champs à saisir** :
- **Code du produit** (ex: "V0001") - Code unique, obligatoire
- **Libellé du produit** (ex: "Vitamine D") - Nom du produit, obligatoire  
- **Stock initial** (ex: "10") - Nombre d'unités, obligatoire

**Actions** :
- ✅ Bouton **"Ajouter"** - Valide et enregistre le produit
- ❌ Bouton **"Quitter"** - Retour à l'accueil

**Validations** :
- Code ne peut pas être vide
- Libellé ne peut pas être vide
- Stock doit être un nombre positif ou zéro
- Code ne doit pas déjà exister
- ⚠️ Message d'erreur en bas si problème

**Message de succès** :
```
✓ Échantillon ajouté : V0001, Vitamine D, 10
```

---

#### 2️⃣ "Liste des échantillons"
Affiche tous les produits en stock avec leurs détails.

**Affichage** :
- **Code** : Code unique (en rose)
- **Libellé** : Nom complet du produit
- **Stock** : Quantité actuelle (en vert)
- **Stock mini** : Niveau d'alerte (en gris)

**Fonctionnalités** :
- ℹ️ Liste scrollable si beaucoup de produits
- 🔄 Se rafraîchit automatiquement à chaque visite
- ❌ Bouton **"Quitter"** pour revenir

---

#### 3️⃣ "Maj d'un échantillon"
Augmente ou diminue le stock d'un produit existant.

**Champs à saisir** :
- **Code du produit** (ex: "V0001") - Produit à modifier
- **Quantité** (ex: "5") - Nombre d'unités à ajouter/retirer

**Boutons d'action** :
- ✅ **"Ajouter"** (vert) - Ajoute la quantité au stock
  - Message : `✓ Quantité ajoutée pour V0001 : +5`
  
- ❌ **"Supprimer"** (rouge) - Retranche la quantité du stock
  - Message : `✓ Quantité supprimée pour V0001 : -5`

**Fonctionnement** :
1. Saisir le code du produit à modifier
2. Entrer la quantité à ajouter/retirer
3. Cliquer sur Ajouter ou Supprimer
4. Une ligne est ajoutée automatiquement à l'historique

**Validations** :
- ⚠️ Code ne peut pas être vide
- ⚠️ Quantité doit être > 0
- ⚠️ Le produit doit exister
- ⚠️ Stock ne peut pas devenir négatif

---

#### 4️⃣ "Liste des ajouts"
Historique de tous les ajouts de stock effectués.

**Affichage** :
- **Code** : Code du produit (rose)
- **Mouvement** : "ajout" (vert)
- **Quantité** : Nombre ajouté
- **Date/Heure** : Moment de l'opération (format JJ/MM/AAAA HH:MM:SS)

**Fonctionnalités** :
- 📜 Affiche les ajouts triés par date décroissante
- 🔄 Rafraîchissement au passage sur la page
- 🎨 Code couleur : vert pour "ajout"

---

#### 5️⃣ "Liste des suppressions"
Historique de tous les retrait de stock effectués.

**Affichage** :
- **Code** : Code du produit (rose)
- **Mouvement** : "suppression" (rouge)
- **Quantité** : Nombre retiré
- **Date/Heure** : Moment de l'opération

**Fonctionnalités** :
- 📜 Affiche les suppressions triés par date décroissante
- 🔄 Rafraîchissement au passage sur la page
- 🎨 Code couleur : rouge pour "suppression"

---

## 🎨 Compréhension du design

### Couleurs utilisées

| Couleur | Code | Utilisation |
|---------|------|-------------|
| 🔵 Bleu nuit | #1a1a2e | Fond principal |
| 🔵 Bleu profond | #16213e | Champs de saisie |
| 🔵 Bleu moyen | #0f3460 | Boutons neutres |
| 🔴 Rose/Rouge | #e94560 | Codes produits, accents GSB |
| 🟢 Vert | #4caf50 | Succès, ajouts |
| 🔴 Rouge | #f44336 | Danger, suppressions |
| ⚪ Blanc | #ffffff | Textes principaux |
| ⚫ Gris | #a8b2d8 | Textes secondaires |

### Layout

- **Boutons** : Arrondis (coins à 8px)
- **Champs** : Fond bleu foncé avec bordure subtile
- **Listes** : Fond bleu très foncé avec items en cartes
- **Texte** : Blanc sur fond sombre (contraste fort)

---

## 🧪 Données de démonstration

Au premier lancement, 5 produits sont automatiquement créés :

| Code | Libellé | Stock | Stock mini | Description |
|------|---------|-------|-----------|-------------|
| A0001 | Acai Bio | 10 | 3 | Riche en antioxydants |
| A0002 | Aloe Vera | 5 | 2 | Apaisant et hydratant |
| B0001 | Baobab Poudre | 7 | 2 | Source de vitamine C |
| B0002 | Bacopa Monnieri | 6 | 2 | Améliore la mémoire |
| M0001 | Moringa Bio | 12 | 4 | Multivitaminé naturel |

**Vous pouvez les modifier ou les supprimer librement.**

---

## 📋 Cas d'utilisation courants

### Cas 1️⃣ : Ajouter un nouveau produit

1. Cliquer sur **"Ajouter un nouvel échantillon"**
2. Saisir Code : `P0001`
3. Saisir Libellé : `Probiotiques`
4. Saisir Stock : `8`
5. Cliquer **"Ajouter"**
6. ✅ Message : `✓ Échantillon ajouté : P0001, Probiotiques, 8`

---

### Cas 2️⃣ : Voir tous les produits en stock

1. Cliquer sur **"Liste des échantillons"**
2. 📜 Affichage scrollable de tous les produits
3. Observer : Code, Libellé, Stock actuel, Stock minimum
4. Cliquer **"Quitter"** pour revenir

---

### Cas 3️⃣ : Le commercial reçoit 3 unités de "Acai Bio" (A0001)

1. Cliquer sur **"Maj d'un échantillon"**
2. Saisir Code : `A0001`
3. Saisir Quantité : `3`
4. Cliquer **"Ajouter"** (vert)
5. ✅ Message : `✓ Quantité ajoutée pour A0001 : +3`
6. Stock passe de 10 à 13
7. Une ligne est créée dans l'historique

---

### Cas 4️⃣ : Le commercial distribue 2 unités de "Vitamine D" (V0001)

1. Cliquer sur **"Maj d'un échantillon"**
2. Saisir Code : `V0001`
3. Saisir Quantité : `2`
4. Cliquer **"Supprimer"** (rouge)
5. ✅ Message : `✓ Quantité supprimée pour V0001 : -2`
6. Stock passe de X à X-2
7. Une ligne est créée dans l'historique

---

### Cas 5️⃣ : Consulter l'historique des ajouts

1. Cliquer sur **"Liste des ajouts"**
2. 📜 Affichage scrollable de tous les ajouts
3. Code, "ajout" (vert), Quantité, Date/Heure
4. Triés par date la plus récente en premier

---

### Cas 6️⃣ : Consulter l'historique des distributions

1. Cliquer sur **"Liste des suppressions"**
2. 📜 Affichage scrollable de toutes les suppressions
3. Code, "suppression" (rouge), Quantité, Date/Heure
4. Triés par date la plus récente en premier

---

## ⚠️ Messages d'erreur

### ❌ Lors de l'ajout d'un produit

| Message | Cause | Solution |
|---------|-------|----------|
| "Erreur : Le code produit ne peut pas être vide" | Champ Code vide | Saisir un code |
| "Erreur : Le libellé produit ne peut pas être vide" | Champ Libellé vide | Saisir un libellé |
| "Erreur : Le stock doit être un nombre positif" | Stock invalide | Entrer un nombre ≥ 0 |
| "Erreur : Ce code produit existe déjà" | Code en doublon | Changer le code |

### ❌ Lors de la MAJ de stock

| Message | Cause | Solution |
|---------|-------|----------|
| "Erreur : Le code produit ne peut pas être vide" | Code vide | Saisir un code |
| "Erreur : La quantité doit être un nombre positif" | Quantité < 0 ou 0 | Entrer une quantité > 0 |
| "Erreur : Échantillon... non trouvé" | Code inexistant | Vérifier le code |
| "Erreur : Stock insuffisant..." | Suppression > stock | Réduire la quantité |

---

## 🔄 Navigation

**Depuis n'importe quelle page** :
- ❌ Bouton **"Quitter"** = Retour à l'accueil
- 🏠 Vous êtes toujours à 1 clic de revenir

**Flow de navigation** :
```
MainPage (Accueil)
    ├─→ AjoutEchantillonPage → Quitter → MainPage
    ├─→ ListeEchantillonsPage → Quitter → MainPage
    ├─→ MajEchantillonPage → Quitter → MainPage
    └─→ ListeMouvementsPage → Quitter → MainPage
```

---

## 💾 Sauvegarde des données

**Les données sont sauvegardées automatiquement** :
- ✅ Chaque ajout de produit
- ✅ Chaque modification de stock
- ✅ Chaque historique créé

**Stockage** :
- 📁 Base de données SQLite locale
- 📂 Chemin : `/data/data/com.companyname.androidgsb/files/gsb_stock.db3`
- 🔒 Données persistantes même après fermeture de l'app

---

## 🎯 Conseils d'utilisation

1. **Vérifié les produits existants** avant d'en ajouter un nouveau
2. **Consultez l'historique** régulièrement pour valider vos opérations
3. **Stock minimum** : C'est un indicateur (ne bloque pas les opérations)
4. **Erreurs de saisie** : Relisez le message d'erreur et corrigez
5. **Quitter proprement** : Utilisez toujours le bouton "Quitter"

---

## 📞 Support

**En cas de problème** :
- Vérifier que le code produit est exact (majuscules/minuscules)
- Vérifier que la saisie des nombres est correcte
- Fermer et rouvrir l'application si blocage
- Les données sont persistantes, aucun risque de perte

---

**Version** : 1.0  
**Plateforme** : Android 5.0+  
**Base de données** : SQLite  
**Backup** : Manuel via export de la BD

