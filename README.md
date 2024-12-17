# Simulation API

## Description

API de simulation développée dans le cadre d'un test technique.

## Architecture

L'application suit une architecture en "Vertical Slice", où chaque fonctionnalité est organisée de manière autonome, du contrôleur jusqu'à la logique métier. Cette approche :

- Favorise la cohésion du code par fonctionnalité
- Réduit le couplage entre les différentes parties de l'application
- Facilite la maintenance et l'évolution du code

La structure des dossiers suit cette logique avec un dossier `Features` contenant toutes les fonctionnalités, chacune regroupant ses propres endpoints, validations et logique métier.

## Démarrage rapide

Pour démarrer l'application, exécutez la commande suivante :

```bash
docker compose up --build
```

L'API sera accessible à l'adresse : `http://localhost:8080`

Le endpoint de simulation est exposé sur : `http://localhost:8080/api/simulations`

## Technologies utilisées

- **.NET 9.0** - Framework de développement
- **Docker** - Conteneurisation
- **FluentValidation** - Validation des données
- **MediatR** - Implémentation du pattern Mediator
- **Redis** - Cache distribué
- **Newtonsoft.Json** - Sérialisation JSON

## Exécution des tests

Les tests unitaires se trouvent dans le dossier `tests/Api.Tests.Unit`. Pour les exécuter, utilisez la commande suivante :

```bash
dotnet test
```

Vous devez vous trouver à la racine du projet (au même endroit que Simu.sln)

## Fonctionnalités testées

L'application teste les fonctionnalités suivantes :

1. **Stratégie de crédit fixe** (`FixedCreditStrategyTests`)

   - Validation des contraintes d'entrée (capital, durée)
   - Calcul des mensualités
   - Calcul des intérêts
   - Gestion des cas d'erreur

2. **Gestion des simulations** (`StartSimulationHandlerTests`)

   - Traitement des demandes de simulation
   - Mise en cache des résultats
   - Validation des requêtes

3. **Validation des requêtes** (`StartSimulationRequestValidatorTests`)
   - Validation des données d'entrée
   - Gestion des cas invalides
