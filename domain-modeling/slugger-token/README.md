# Slugger : le token de fin de slug

## Le contexte : Slugger

Slugger génère des *slugs* : des identifiants lisibles par un humain, composés de mots pris dans un thème, à la manière des noms que Docker donne à ses conteneurs.

Les mots viennent d'un vocabulaire lu dans des fichiers, un par thème. Un slug peut en plus se terminer par quelques caractères tirés au hasard : c'est le **token**.

Par exemple, avec un thème cyberpunk, un slug pourrait ressembler à ceci :

```
neon-samurai          sans token
neon-samurai-4821     avec un token décimal de 4 caractères
neon-samurai-3fa9     avec un token hexadécimal de 4 caractères
```

## À quoi sert le token

Docker ajoute un suffixe numérique au nom d'un conteneur quand ce nom est déjà pris. Slugger reproduit cet aspect visuel **sans détecter de collision** : le token est simplement tiré au hasard, et il n'apparaît qu'une partie du temps, selon une probabilité choisie par l'utilisateur.

Un token se définit par :

- **son alphabet** : les dix chiffres décimaux, ou les seize caractères hexadécimaux en minuscules ;
- **sa longueur** : le nombre de caractères tirés, au moins un ;
- **sa probabilité d'apparition** : de 0 (jamais) à 100 (toujours).

Sa longueur compte dans le budget de longueur du slug, comme celle des mots.

## Le hasard est injecté

Le tirage passe par une source de hasard (`IRandomSource`) fournie par l'appelant. Les tests peuvent ainsi lui donner une suite de valeurs écrite à l'avance et vérifier le slug obtenu. Le nombre de valeurs consommées par chaque tirage fait donc partie du contrat : s'il change, toutes les valeurs écrites dans les tests après ce tirage sont décalées.

## Le contenu de ce dossier

| Fichier | Rôle |
| --- | --- |
| `Token.cs` | Le token lui-même, un value object. |
| `TokenAlphabet.cs` | Les alphabets dans lesquels un token peut être tiré. |
| `TokenFactory.cs` | Le tirage d'un token. |

L'extrait est volontairement isolé du reste de Slugger : `IRandomSource` et `ValueType<T>` n'y figurent pas, et il ne se compile pas tel quel.

## L'exercice

Lisez ce code comme si vous faisiez la revue d'une pull request. Qu'est-ce qui vous gêne, et que proposeriez-vous ?
