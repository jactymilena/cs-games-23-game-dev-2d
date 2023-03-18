**Équipe: Hydro-Sherbrooke**

Membres:
- Benjamin Courchesne
- Martin Dufour
- Jacty Saenz

### Mini-jeu 1: 2D
#### Niveau 1:
Cela a été réussi avec un état de Patrouille présent sur les ennemis. Des points ont été disposés de manière alléatoire sur la map. Les ennemis se déplacent sur les points de manière alléatoire.

#### Niveau 2:
Lorsque le joueur entre dans la ligne de vue d'un ennemie, ce dernier entre en état de chasse et va suivre le joueur tant et aussi longtemps que le joueur sera dans la ligne de vue de l'ennemi. La ligne de vue étant représenté par un cône.

#### Niveau 3:
Lorsque le joueur quitte la ligne de vue d'un ennemi, ce dernier va se diriger vers la dernière position connue du joueur et va bouger dans des directions aléatoires dans un certain rayon de la position durant quelques secondes.


#### Niveau 4:
Un état a été ajouté pour gouverner tout les ennemies. Lorsque l'un des ennemis
a dans sa ligne de vue le joueur, tout les ennemis vont suivre ce joueur tant qu'au
moins un des ennemis aura en ligne de vu le joueur. Lorsque plus aucun des ennemis a
le joueur dans sa ligne de vue, tout les ennemis tombent en mode recherche.


#### Niveau 5:
Notre "pathfinding" est très rudimentaire mais fonctionnel. Un ennemi fait un rayon entre sa position actuelle et la destination possible. S'il n'y a pas d'obstacle entre lui et la destination il bouge, sinon il essai un autre chemin. 
