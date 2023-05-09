# Programmation_Parallele
TP multi-thread => Redimensionner un lot de 200 images de 20 Mo

Le programme simule le traitement de 200 images de 20 Mo en les redimensionnant à l'aide de la méthode ResizeImage. Le traitement est effectué en deux parties : d'abord en mode séquentiel, puis en mode multi-thread.

Dans la partie séquentielle, chaque image est traitée une à une à l'aide d'une boucle foreach.

Dans la partie multi-thread, le traitement est réparti sur plusieurs threads. La liste des images est partagée entre les threads. Chaque thread récupère une image de la liste et la traite jusqu'à ce que toutes les images aient été traitées.

Chaque thread récupère une image de la liste et la supprime de la liste pour éviter que deux threads traitent la même image. Le traitement des images est effectué de manière asynchrone sur plusieurs threads, ce qui permet de réduire le temps de traitement par rapport à la méthode séquentielle.

Enfin, le programme mesure le temps de traitement pour chaque méthode et affiche le temps nécessaire pour le traitement séquentiel et le traitement multi-thread
