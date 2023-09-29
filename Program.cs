using System;
using System.Collections.Generic;
using System.Linq;

class Pilote
{
    public string Nom { get; set; }
    public int Numero { get; set; }
    public List<double> TempsAuTour { get; set; } = new List<double>();
    public int ClassementFinal { get; set; }
}

class Ecurie
{
    public string Nom { get; set; }
    public List<Pilote> Pilotes { get; set; } = new List<Pilote>();
}

class Course
{
    public int NombreDeTours { get; set; }
    public List<Ecurie> Ecuries { get; set; } = new List<Ecurie>();

    public void CalculerClassement()
    {
        foreach (var ecurie in Ecuries)
        {
            foreach (var pilote in ecurie.Pilotes)
            {
                pilote.ClassementFinal = (int)pilote.TempsAuTour.Sum();
            }
            ecurie.Pilotes.Sort((p1, p2) => p1.ClassementFinal.CompareTo(p2.ClassementFinal));
        }
    }

    public Ecurie DeterminerEcurieGagnante()
    {
        Ecurie ecurieGagnante = null;
        double meilleurTempsTotal = double.MaxValue;

        foreach (var ecurie in Ecuries)
        {
            double tempsTotal = ecurie.Pilotes.Sum(p => p.TempsAuTour.Sum());
            if (tempsTotal < meilleurTempsTotal)
            {
                meilleurTempsTotal = tempsTotal;
                ecurieGagnante = ecurie;
            }
        }

        return ecurieGagnante;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Gestion de Course Automobile");

        Course course = new Course();

        Console.Write("Nombre de tours : ");
        course.NombreDeTours = int.Parse(Console.ReadLine());

        Console.Write("Nombre d'écuries : ");
        int nombreEcuries = int.Parse(Console.ReadLine());

        for (int i = 0; i < nombreEcuries; i++)
        {
            Ecurie ecurie = new Ecurie();
            Console.Write($"Nom de l'écurie {i + 1}: ");
            ecurie.Nom = Console.ReadLine();

            for (int j = 0; j < course.NombreDeTours; j++)
            {
                Pilote pilote = new Pilote();
                Console.Write($"Nom du pilote {j + 1}: ");
                pilote.Nom = Console.ReadLine();
                Console.Write($"Numéro du pilote {j + 1}: ");
                pilote.Numero = int.Parse(Console.ReadLine());

                for (int k = 0; k < course.NombreDeTours; k++)
                {
                    Console.Write($"Temps au tour {k + 1} pour {pilote.Nom}: ");
                    double temps = double.Parse(Console.ReadLine());
                    pilote.TempsAuTour.Add(temps);
                }

                ecurie.Pilotes.Add(pilote);
            }

            course.Ecuries.Add(ecurie);
        }

        course.CalculerClassement();
        Ecurie ecurieGagnante = course.DeterminerEcurieGagnante();

        Console.WriteLine("\nRésultats de la course :");
        foreach (var ecurie in course.Ecuries)
        {
            Console.WriteLine($"Écurie : {ecurie.Nom}");
            foreach (var pilote in ecurie.Pilotes)
            {
                Console.WriteLine($"Pilote : {pilote.Nom}, Classement : {pilote.ClassementFinal}");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"L'écurie gagnante est : {ecurieGagnante.Nom}");
    }
}


