code review *cf38e0ba199a8e667ea3100d6662f4b7a792dee7* - thanks *philgung* 

# Plateau

 - [ ] La définition des Actions au début du controlleur Plateau, je ne suis pas fan. Cela masque ce que le constructeur est censé faire. Soit à mettre à la fin, soit à sortir en bas dans la classe. Tu y gagneras en lisibilité
 - [X] Dans ce constructeur, la logique pour gérer la conf est assez complexe. Passé par un while pour gérer le flux du fichier de config est un peu overkill. Tu dois sans pouvoir lire le flux de la conf plus simplement. 
 - [X] Je ne suis pas fan que tu es de la logique métier dans ton constructeur, comme ajouter des déplacements à ton rover
 - [...] ValidatePosition ne devrait pas forcément être de la responsabilité du Plateau. Pourquoi ne pas encapsuler la notion de positionX, positionY et de direction dans un objet,
 - [ ] par exemple Location qui contiendrai également la logique pour valider la position ? Bref, un souci sur les responsabilités selon moi.

# Rover

 - [X] Pareil, parseInt, parseDirection pourrait être sorti. 
 - [ ] De manière générale, faire des Actions ou des func avec une dépendance avec un variable extérieur/globale n'est pas une très bonne idée, difficile de gérer les effets de bords. A rapprocher des fonctions pures du paradigme fonctionnelle
 - [ ] Utiliser un tuple en signature de méthode publique va amener de la complexité en lisibilité. Pour moi utiliser un tuple ok, mais l'utiliser si cela reste dans le périmètre d'une méthode ou d'une classe. En revanche l'exposé, est pour moi plus un code smells. A mon sens, il faudrait plutot l'encapsuler dans un objet.
 - [X] Turn(clockwise) ?? Je ne me souviens pas qu'on ait cette notion dans l'énoncé. Pourquoi ne pas faire simple et dire tournerAGauche et tournerADroite ?
 - [X] Idem, au lieu de MoveX, MoveY, j'aurai préféré Avancer() ou Reculer(), et en fonction de la direction et de la position, tu adaptes. Sinon le nommage est trop technique selon moi, a t on vraiment besoin de savoir que tu as des coordonnées X et Y lorsqu'on déplace le rover ?

# Générale

 - [ ] Je préfère éviter de gérer le flux de mon code avec des exceptions, je préférerai passer par des objets encapsulants.
 - [X] Sur les tests, tu pourrais créer plusieurs fichiers de tests en fonction du périmètre fonctionnelle, cela faciliterait la lecture. 
 - [-] L'input dans le constructeur qui est une chaine de caractère, amène beaucoup de complexité. Pourquoi ne pas être passer par un objet avec la position, la direction et les mouvements ? ANSWER: c'est dans les spec!
	> Input:
	> The first line of input is the upper-right coordinates of the plateau, the lower-left coordinates are assumed to be 0,0.
	> The rest of the input is information pertaining to the rovers that have been deployed. Each rover has two lines of input. The first line gives the rover's position, and the second line is a series of instructions telling the rover how to explore the plateau.
	> The position is made up of two integers and a letter separated by spaces, corresponding to the x and y co-ordinates and the rover's orientation.

 - [X] Le nom de certain test est vague, par ex :
 - [X] check_Plateau_Fleet_invalid_position3


# found variants for later

 - [ ] Implement wrapping from one edge of the grid to another. (planets are spheres after all)
 - [ ] Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point and reports the obstacle.
 
 https://kata-log.rocks/mars-rover-kata
 
 Your Task
You’re part of the team that explores Mars by sending remotely controlled vehicles to the surface of the planet. Develop an API that translates the commands sent from earth to instructions that are understood by the rover.

Requirements
You are given the initial starting point (x,y) of a rover and the direction (N,S,E,W) it is facing.
The rover receives a character array of commands.
Implement commands that move the rover forward/backward (f,b).
Implement commands that turn the rover left/right (l,r).
Implement wrapping at edges. But be careful, planets are spheres.
Implement obstacle detection before each move to a new square. If a given sequence of commands encounters an obstacle, the rover moves up to the last possible point, aborts the sequence and reports the obstacle.
Rules


Facilitation notes (warning, spoilers)
There are multiple solutions for the requirement of “wrapping around the edges”. Depending on the background of your participants, they might have different understandings of the coordinate system. Previous experience doing this kata shows that misunderstandings, e.g. participants arriving with different mental models, can easily derail the kata and deflect from testing and designing. For facilitators, we recommend you work towards the participants aligning on which model to go by.

It is most helpful to work with visualizations and spend some structured time exploring the problem and the options the participants present and highlight how they can all be valid solutions.

Torus/Donut: Retaining euclidian geometry
Similar to games (think snake, pacman), where the player vanishes on the top and reappears on the bottom (vis versa for left & right), this might be a solution someone with a mental model based in maths/topology or games arrives at.

The implementation does not produce any new edge cases, but the mental model might be hard to align on.

Example:
In a 4x4 grid (x ∈ { 1, 2, 3, 4 }, y ∈ { 1, 2, 3, 4 }) the following table shows the resulting position for a movement on the grid:

Initial Position \ Operation	x + 1	x - 1	y + 1	y - 1
(1, 1)	(2, 1)	(4, 1)	(1, 2)	(1, 4)
(2, 1)	(3, 1)	(1, 1)	(2, 2)	(2, 4)
(2, 2)	(3, 2)	(1, 2)	(2, 3)	(2, 1)
(3, 1)	(4, 1)	(2, 1)	(3, 2)	(3, 4)
Credit to @drpicox for providing an explanation for this model.

Polar coordinate system: Thinking in maps and planets
This interpretation of the grid system lends itself to the concept of latitude and longitude. The sphere is sliced into an even number of latitudes (equidistant lines) and longitudes (evenly spaced lines from North to South pole)

A visualization of polar coordinates for a 8X8 grid

In this model, X and Y become abstract representations of longitudes and latitudes.

While the model might be the first one people arrive at if they’re coming from the mental models of planets and maps, it produces some significant edge cases that make this solution rather challenging:

Some potential challenges:

The distance between two points of the grid is no longer the actual distance. (two points close the poles are closer to each other than two points on the equator)
If the Poles can be represented through the coordinates, the behaviour at that point is undefined. E.g. at the North pole, the Mars Rover always faces South (by definition), but the direction it would move forward to is informed by the longitude it is facing. (e.g. (x, 1) for all x ∈ {1, 2, 3, 4} describes the same point, but a different point the mars rover will end on if it moves forward.)
The geometry at the poles is no longer euclidian, e.g. the path you take at the pole to arrive back at the same position has three corners, where everywhere else it has four. The same visualization above, with two paths that end up at the same point highlighted. At the pole, the path has 3 corners, everywhere else it has 4 corners
This interpretation is vastly more complex, but can be tamed by constraining the solution to the following aspects:

The number of latitudes and longitudes is the same and a multiple of 2
x and y are positive integers. Positions not on the grid can not exist.
The poles are not “on the grid”. The rover moves “over” the pole but never rests on them.
Example
In a 4x4 grid (x ∈ { 1, 2, 3, 4 }, y ∈ { 1, 2, 3, 4 }) the following table shows the resulting position for a movement on the grid:

Initial Position \ Operation	x + 1	x - 1	y + 1	y - 1
(1, 1)	(2, 1)	(4, 1)	(1, 2)	(3, 1)
(2, 1)	(3, 1)	(1, 1)	(2, 2)	(4, 1)
(2, 2)	(3, 2)	(2, 1)	(2, 3)	(2, 1)
(3, 1)	(4, 1)	(2, 1)	(3, 2)	(1, 1)
Starting Points
C++, C#, Clojure, D, Elixir, F#, Go, Haskell, Java, JavaScript, Kotlin, PHP, Python, ReScript, Ruby, Rust, Scala, TypeScript

Clojure, CoffeeScript, C++, C#, Erlang, Groovy, Intercal, Java, JavaScript, Lisp, PHP, Ruby, Scala
Hardcore TDD. No Excuses!
Change roles (driver, navigator) after each TDD cycle.
No red phases while refactoring.
Be careful about edge cases and exceptions. We can not afford to lose a mars rover, just because the developers overlooked a null pointer.