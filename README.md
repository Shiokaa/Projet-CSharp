# Projet CSharp - Ynov B2

Ce dépôt contient deux applications développées dans le cadre du cours de C# (Ynov B2). Les deux projets sont des applications de bureau basées sur **Windows Forms (WinForms)**.

## Structure du Projet

- **`partie-1`** : `BibliothequeApp` - Une application de gestion de bibliothèque (Livres, Adhérents, Emprunts) avec une base de données MySQL.
- **`partie-2`** : `MeteoApp` - Une application météo permettant de consulter le temps actuel et de gérer des villes favorites.

---

## Partie 1 : Application de Gestion de Bibliothèque (`BibliothequeApp`)

Ce projet est une application WinForms (.NET 10) connectée à une base de données MySQL. Elle permet de gérer les livres, les adhérents, et les emprunts de la bibliothèque.

### Fonctionnalités

- Gestion des **Livres** (Ajout, modification, suppression).
- Gestion des **Adhérents** (Ajout, modification, suppression).
- Gestion des **Emprunts** (Attribution d'un livre à un adhérent, retour).
- Base de données locale provisionnée via Docker.

### Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker & Docker Compose](https://www.docker.com/) (pour la base de données MySQL)

### Installation et exécution

1. **Démarrer la base de données** :
   Dans le dossier `partie-1`, montez les conteneurs ajoutez un `.env` pour docker :

   ```env
   MYSQL_ROOT_PASSWORD=root_secret
   MYSQL_DATABASE=bibliotheque
   MYSQL_USER=biblio_user
   MYSQL_PASSWORD=biblio_secret
   ```

   ```bash
   cd partie-1
   docker-compose up -d
   ```

   _La base de données sera disponible sur le port 3306 et PhpMyAdmin sera accessible sur `http://localhost:8080`._

2. **Configuration de la base de données (BibliothequeApp)** :
   Dans le dossier `partie-1/BibliothequeApp`, créez un fichier `.env` contenant les identifiants de votre base de données :

   ```env
   DB_SERVER=localhost
   DB_PORT=3306
   DB_DATABASE=bibliotheque
   DB_USER=biblio_user
   DB_PASSWORD=biblio_secret
   ```

3. **Lancer l'application** :
   ```bash
   cd BibliothequeApp
   dotnet run
   ```

---

## Partie 2 : Application Météo (`MeteoApp`)

Ce projet est une application WinForms (.NET 8) qui interroge une API météo externe pour récupérer les prévisions et la température en temps réel d'une ville.

### Fonctionnalités

- Consultation de la météo actuelle pour une ville donnée.
- Gestion des **Villes Favorites** (Ajout et suppression des favoris via un stockage local).
- Récupération des données via une API tierce.

### Prérequis

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

### Configuration de l'API

Assurez-vous que la clé d'API est configurée dans le fichier `appsettings.json` :

```json
{
  "WeatherApiKey": "VOTRE_CLE_API_ICI"
}
```

### Installation et exécution

```bash
cd partie-2/MeteoApp
dotnet run
```

---

## Exécution sous Linux (via Wine)

Puisque les deux applications utilisent **Windows Forms** (`net10.0-windows` et `net8.0-windows`), elles ne peuvent pas être exécutées nativement via un simple `dotnet run` sous Linux. Vous devez utiliser **Wine** et compiler les applications de manière autonome (Self-Contained) pour inclure le runtime Windows.

### Prérequis Linux

- `wine` installé sur votre distribution.
- Le SDK .NET correspondant installé sous Linux (pour pouvoir compiler).

### Lancer la Partie 1 (BibliothequeApp) sous Linux

1. **Démarrez la base de données** (comme indiqué dans la section Partie 1) :
   ```bash
   cd partie-1
   docker-compose up -d
   ```
2. **Compilez l'application en version autonome pour Windows** :
   ```bash
   cd BibliothequeApp
   dotnet publish -r win-x64 --self-contained true -p:PublishSingleFile=true
   ```
3. **Lancez l'exécutable avec Wine** :
   ```bash
   wine bin/Release/net10.0-windows/win-x64/publish/BibliothequeApp.exe
   ```

### Lancer la Partie 2 (MeteoApp) sous Linux

1. **Compilez l'application en version autonome pour Windows** :
   ```bash
   cd partie-2/MeteoApp
   dotnet publish -r win-x64 --self-contained true -p:PublishSingleFile=true
   ```
2. **Lancez l'exécutable avec Wine** :
   ```bash
   wine bin/Release/net8.0-windows/win-x64/publish/MeteoApp.exe
   ```
