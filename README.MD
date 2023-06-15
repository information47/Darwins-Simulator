# Darwin's Simulator

Darwin’s Simulator est un projet Unity 3D. Comme son nom l’indique, il a pour but de simuler l’évolution darwinienne. Il contient une sélection naturelle simple en fonction de l’énergie, la faim, la vitesse de déplacement et la taille de chaque individu. La partie la partie la plus intéressante est la suivante : Chaque individu dispose d’un cerveau composé d’un réseau de neurones utilisant l’algorithme NEAT (Neuroevolution of augmenting topologies). 

**Valeurs d’entrée du réseau de neurones :**
-	Angle et Distance entre l’individu et la nourriture la plus proche 				   (dans son champ de vision)
-	Trois rayons qui déterminent la distance et l’orientation entre l’individu et limites du monde ou les obstacles.
-	Nombre d’individus à proximité
-	Faim
-	Santé
	
**Valeurs de sortie du réseau de neurones :**
-	Vitesse de déplacement
-	Vitesse de rotation
-	Reproduction
-	Récupération de santé

Ainsi, grâce à ces entrées et sortie, le réseau permet à l’individu d'évoluer dans son monde en fonction de ses valeurs d’entrée.

une version 2 orientée comportements sociaux plutôt que sciences est à venir.x plutôt que sciences est à venir.
![apercu en simulation](./apercu.gif)