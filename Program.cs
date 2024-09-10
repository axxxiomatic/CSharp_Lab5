using System;
using System.Collections.Generic;
using System.IO;
using Lab05_Csharp;

namespace Lab05_CSharp
{
    enum Frequency { Weekly, Monthly, Yearly };

    class Program
    {
        static void Main(string[] args)
        {
            // Упражнение 1
            Console.WriteLine("Exercise 1 \n");
            // Создадим объект класса Magazine, заполним списки редакторов и статей

            List<Person> EditorsArray1 = new List<Person>(2);
            EditorsArray1.Add(new Person("Vlad", "Gorban", new DateTime(2002, 7, 9)));
            EditorsArray1.Add(new Person("Alexandra", "Morozova", new DateTime(2000, 6, 4)));
            List<Article> ArticlesArray1 = new List<Article>(2);
            ArticlesArray1.Add(new Article(new Person("Vlad", "Gorban", new DateTime(2002, 7, 9)), "XXX", 5));
            ArticlesArray1.Add(new Article(new Person("Alexandra", "Morozova", new DateTime(2000, 6, 4)), "YYY", 6));
            Magazine Mag1 = new Magazine("Popular Philosophy", Frequency.Weekly, new DateTime(2021, 10, 14), 1);
            Mag1.ArticlesList.AddRange(ArticlesArray1);
            Mag1.EditorsList.AddRange(EditorsArray1);

            // Сделаем глубокую копию с использованием сериализации
            Magazine MagCopy = Mag1.DeepCopySerialization(Mag1);
            Console.WriteLine("Original object:");
            Mag1.ToString();
            Console.WriteLine("\nSerialized copy:");
            MagCopy.ToString();

            // Упражнение 2
            Console.WriteLine("\nExercise 2 \n");
            Console.WriteLine("Enter your file name: ");
            string fileName = Console.ReadLine();
            Magazine Mag2 = new Magazine();
            if (fileName != null)
            {
                Mag2.Load(fileName);
            }
            else
            {
                Console.WriteLine("Cannot open your file! Creating a new one.");
                using (FileStream fs = File.Create(fileName))
                {
                    fs.Close();
                }
            }

            // Упражнение 3
            Console.WriteLine("\nExercise 3\n");
            Mag2.ToString();

            // Упражнение 4
            Console.WriteLine("\nExercise 4\n");
            Mag2.AddArticleFromConsole();
            Console.WriteLine(" ");
            Mag2.Save(fileName);
            Mag2.ToString();

            // Упражнение 5
            Console.WriteLine("\nExercise 5\n");
            Magazine.Load(fileName, ref Mag2);
            Mag2.AddArticleFromConsole();
            Console.WriteLine(" ");
            Magazine.Save(fileName, ref Mag2);

            // Упражнение 6
            Console.WriteLine("\nExercise 6\n");
            Mag2.ToString();

            Console.WriteLine("That's all.");
            Console.ReadKey();
        }
    }
}