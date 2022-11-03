code review *cf38e0ba199a8e667ea3100d6662f4b7a792dee7* - thanks *philgung* 

# Plateau

 - [ ] La définition des Actions au début du controlleur Plateau, je ne suis pas fan. Cela masque ce que le constructeur est censé faire. Soit à mettre à la fin, soit à sortir en bas dans la classe. Tu y gagneras en lisibilité
 - [ ] Dans ce constructeur, la logique pour gérer la conf est assez complexe. Passé par un while pour gérer le flux du fichier de config est un peu overkill. Tu dois sans pouvoir lire le flux de la conf plus simplement. 
 - [ ] Je ne suis pas fan que tu es de la logique métier dans ton constructeur, comme ajouter des déplacements à ton rover
 - [ ] ValidatePosition ne devrait pas forcément être de la responsabilité du Plateau. Pourquoi ne pas encapsuler la notion de positionX, positionY et de direction dans un objet, par exemple Location qui contiendrai également la logique pour valider la position ? Bref, un souci sur les responsabilités selon moi.

# Rover

 - [ ] Pareil, parseInt, parseDirection pourrait être sorti. 
 - [ ] De manière générale, faire des Actions ou des func avec une dépendance avec un variable extérieur/globale n'est pas une très bonne idée, difficile de gérer les effets de bords. A rapprocher des fonctions pures du paradigme fonctionnelle
 - [ ] Utiliser un tuple en signature de méthode publique va amener de la complexité en lisibilité. Pour moi utiliser un tuple ok, mais l'utiliser si cela reste dans le périmètre d'une méthode ou d'une classe. En revanche l'exposé, est pour moi plus un code smells. A mon sens, il faudrait plutot l'encapsuler dans un objet.
 - [ ] Turn(clockwise) ?? Je ne me souviens pas qu'on ait cette notion dans l'énoncé. Pourquoi ne pas faire simple et dire tournerAGauche et tournerADroite ?
 - [ ] Idem, au lieu de MoveX, MoveY, j'aurai préféré Avancer() ou Reculer(), et en fonction de la direction et de la position, tu adaptes. Sinon le nommage est trop technique selon moi, a t on vraiment besoin de savoir que tu as des coordonnées X et Y lorsqu'on déplace le rover ?

# Générale

 - [ ] Je préfère éviter de gérer le flux de mon code avec des exceptions, je préférerai passer par des objets encapsulants.
 - [ ] Sur les tests, tu pourrais créer plusieurs fichiers de tests en fonction du périmètre fonctionnelle, cela faciliterait la lecture. 
 - [ ] L'input dans le constructeur qui est une chaine de caractère, amène beaucoup de complexité. Pourquoi ne pas être passer par un objet avec la position, la direction et les mouvements ?
 - [ ] Le nom de certain test est vague, par ex :

 - [ ] check_Plateau_Fleet_invalid_position3


# found variants for later

 - [ ] Implement wrapping from one edge of the grid to another. (planets are spheres after all)
 - [ ] Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point and reports the obstacle.