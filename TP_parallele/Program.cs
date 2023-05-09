using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MultiThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuration du traitement
            int nbThreads = 4;
            int nbImages = 200;
            int imageSize = 20; // Mo

            // Création de la liste d'images fictives à traiter
            List<string> images = new List<string>();
            for (int i = 1; i <= nbImages; i++)
            {
                images.Add($"image{i}.jpg");
            }

            // Lancement du traitement séquentiel
            Stopwatch sequentialWatch = new Stopwatch();
            sequentialWatch.Start();
            foreach (string image in images)
            {
                ResizeImage(image, imageSize);
            }
            sequentialWatch.Stop();
            Console.WriteLine($"Traitement séquentiel terminé en {sequentialWatch.ElapsedMilliseconds} ms");

            // Lancement du traitement multi-thread
            Stopwatch parallelWatch = new Stopwatch();
            parallelWatch.Start();

            // création des threads pour le traitement en parallèle
            Thread[] threads = new Thread[nbThreads];
            for (int i = 0; i < nbThreads; i++)
            {
                // création d'un thread qui traite les images jusqu'à ce qu'il n'y en ait plus
                threads[i] = new Thread(() => {
                    while (images.Count > 0)
                    {
                        string image = null;
                        // Verrouiller la collection d'images pr qu'il y est un  thread à la fois qui peut accéder à la collection pour y ajouter ou supprimer des éléments
                        lock (images)
                        {
                            if (images.Count > 0)
                            {
                                // Recupere la première image dans la collection et la supprimer
                                image = images[0];
                                images.RemoveAt(0);
                            }
                        }
                        if (image != null)
                        {
                            // Si une image est récupérée on la redimensionne
                            ResizeImage(image, imageSize);
                        }
                    }
                });
                // Démarre le thread
                threads[i].Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            // Arrête le chronomètre de traitement en parallèle
            parallelWatch.Stop();
            Console.WriteLine($"Traitement multi-thread terminé en {parallelWatch.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

        // Méthode de redimensionnement d'une image fictive
        static void ResizeImage(string image, int size)
        {
            Console.WriteLine($"Traitement de l'image {image} sur le thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(100); // Simule le traitement de l'image
        }
    }
}
