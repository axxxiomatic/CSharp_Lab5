using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab05_CSharp
{
    [Serializable]
    internal class Magazine : Edition, IRateAndCopy, IEnumerable
    {
        private Frequency frequency;
        private List<Person> editorsList;
        private List<Article> articlesList;

        // Определения перегруженных операторов и методов
        public override bool Equals(Object obj_value)
        {
            if (obj_value == null)
            {
                return false;
            }
            Magazine obj = obj_value as Magazine;
            if (obj_value as Magazine is null)
            {
                return false;
            }
            else return editionTitle.Equals(obj.editionTitle) && frequency == obj.frequency && editionRelease.Equals(obj.editionRelease) && editionCirculation.Equals(obj.editionCirculation) && articlesList.Equals(obj.articlesList);
        }

        public static bool operator ==(Magazine obj1, Magazine obj2)
        {
            return obj1.editionTitle.Equals(obj2.editionTitle) && obj1.frequency == obj2.frequency && obj1.editionRelease.Equals(obj2.editionRelease) && obj1.editionCirculation.Equals(obj2.editionCirculation) && obj1.articlesList.Equals(obj2.articlesList);
        }

        public static bool operator !=(Magazine obj1, Magazine obj2)
        {
            return !obj1.editionTitle.Equals(obj2.editionTitle) || obj1.frequency != obj2.frequency || !obj1.editionRelease.Equals(obj2.editionRelease) || !obj1.editionCirculation.Equals(obj2.editionCirculation) || !obj1.articlesList.Equals(obj2.articlesList);
        }

        public override int GetHashCode()
        {
            return frequency.GetHashCode() ^ editorsList.GetHashCode() ^ articlesList.GetHashCode() ^ editionCirculation.GetHashCode() ^ editionTitle.GetHashCode() ^ editionRelease.GetHashCode();
        }

        public override object DeepCopy()
        {
            Magazine obj = new Magazine();
            obj.editionTitle = editionTitle;
            obj.editionRelease = editionRelease;
            obj.editionCirculation = editionCirculation;
            obj.frequency = frequency;
            obj.EditorsList = new List<Person>(obj.EditorsList.Count + editorsList.Count);
            obj.editorsList.AddRange(editorsList);
            obj.ArticlesList = new List<Article>(obj.ArticlesList.Count + articlesList.Count);
            obj.articlesList.AddRange(articlesList);
            return obj;
        }

        // Определение конструкторов
        public Magazine(string edition_nameValue, Frequency how_oftenValue, DateTime edition_DayOfPublishValue, int edition_numberValue)
        {
            editionTitle = edition_nameValue;
            frequency = how_oftenValue;
            editionRelease = edition_DayOfPublishValue;
            editionCirculation = edition_numberValue;
            editorsList = new List<Person>();
            articlesList = new List<Article>();
        }
        public Magazine()
        {
            editionTitle = null;
            frequency = 0;
            editionRelease = new DateTime();
            editionCirculation = 0;
            editorsList = new List<Person>();
            articlesList = new List<Article>();
        }

        // Определение get и set метода
        public Edition Edition
        {
            get
            {
                return new Edition(editionTitle, editionRelease, editionCirculation);
            }
            set
            {
                editionTitle = value.EditionTitle;
                editionRelease = value.EditionRelease;
                editionCirculation = value.EditionCirculation;
            }
        }

        public Frequency How_often
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
            }
        }

        public double Rating
        {
            get
            {
                double rate = 0;
                if (articlesList.Count == 0)
                {
                    return rate = 0;
                }
                foreach (Article element in articlesList)
                {
                    rate += element.articleRating;
                }
                return rate / articlesList.Count;
            }
        }

        public List<Article> ArticlesList
        {
            get
            {
                return articlesList;
            }
            set
            {
                articlesList.AddRange(value);
            }
        }

        public List<Person> EditorsList
        {
            get
            {
                return editorsList;
            }
            set
            {
                editorsList.AddRange(value);
            }
        }

        public void AddArticles(params Article[] mas)
        {
            articlesList.AddRange(mas);
        }

        public void AddEditors(params Person[] mas)
        {
            editorsList.AddRange(mas);
        }
        public bool this[Frequency index]
        {
            get
            {
                return frequency == index;
            }
        }

        public override string ToString()
        {
            Console.WriteLine(editionTitle + " " + editionRelease.ToShortDateString() + " " + editionCirculation + " " + frequency);
            Console.WriteLine("Articles:");
            foreach (Article element in articlesList)
            {
                Console.WriteLine(element.ToString());
            }
            Console.WriteLine("Editors:");
            foreach (Person element in editorsList)
            {
                Console.WriteLine(element.ToString());
            }
            return "\n";

        }
        public virtual string ToShortString()
        {
            return editionTitle + " " + editionRelease.ToShortDateString() + " " + editionCirculation + " " + frequency + " " + Rating + "\n";
        }

        public IEnumerator GetEnumerator()
        {
            return new MagazineEnumerator(articlesList, editorsList);
        }

        public IEnumerable GetAutorsWhoAreEditors()
        {
            foreach (Article element in articlesList)
            {
                if (editorsList.Contains(element.person))
                    yield return element;
            }
        }

        public IEnumerable GetEditorsWithNoArticles()
        {
            foreach (Article element in articlesList)
            {
                if (!editorsList.Contains(element.person))
                    yield return element.person;
            }
        }

        // Определение методов сортировки
        public void SortByRating()
        {
            articlesList.Sort(new CompareArticleByRating());
        }

        public void SortByName()
        {
            articlesList.Sort(new Article());
        }

        public void SortBySurname()
        {
            articlesList.Sort(new Article());
        }

        // Определим методы, необходимые для сериализации
        // Экземплярные методы:

        // Здесь DeepCopy не будет сильно отличаться, так как BinaryFormatter не будет сериализовывать объекты, содержащие ссылки
        public Magazine DeepCopySerialization(Magazine my_object)
        {
            // Сериализуем в поток памяти MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, my_object);
                ms.Position = 0;
                return (Magazine)formatter.Deserialize(ms);
            }
        }

        public bool Save(string fname)
        {
            try
            {
                using (var fs = new FileStream(fname, FileMode.Create))
                {
                    // Через BinaryFormatter сериализуем вызывающий объект и передаём по потоку в файл
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, this);
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Error saving to file!");
                return false;
            }
        }

        public bool Load(string fname)
        {
            // Создадим копию, и в случае ошибки десериализации восстановим из неё исходные данные
            // Приведение типа нужно, так как тип данных метода определён как object
            Magazine tempCopy = (Magazine)DeepCopy();
            try
            {
                using (var fs = new FileStream(fname, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Magazine loadedObject = (Magazine)formatter.Deserialize(fs);

                    // Десериализуем объект из файлового потока и копируем оттуда данные в this
                    editionTitle = loadedObject.editionTitle;
                    editionRelease = loadedObject.editionRelease;
                    editionCirculation = loadedObject.editionCirculation;
                    frequency = loadedObject.frequency;
                    editorsList = new List<Person>(loadedObject.editorsList);
                    articlesList = new List<Article>(loadedObject.articlesList);

                    return true;
                }
            }

            catch
            {
                Console.WriteLine("Error loading from file!");
                editionTitle = tempCopy.editionTitle;
                editionRelease = tempCopy.editionRelease;
                editionCirculation = tempCopy.editionCirculation;
                frequency = tempCopy.frequency;
                editorsList = new List<Person>(tempCopy.editorsList);
                articlesList = new List<Article>(tempCopy.articlesList);
                return false;
            }
        }

        public bool AddArticleFromConsole()
        {
            string[] values = new string[5];
            Console.WriteLine("Entering new article. Enter author's surname, name, birth date (year, month, day), article's name and rating, divided by ';'.");
            string input = Console.ReadLine();
            values = input.Split(';');

            DateTime dateTime;
            if (DateTime.TryParse(values[2], out dateTime))
            {
                Console.WriteLine("Parsed date and time: " + dateTime);
            }
            else
            {
                Console.WriteLine("Invalid input");
                return false;
            }
            double drating;
            if (double.TryParse(values[4], out drating))
            {
                Console.WriteLine("Parsed rating: " + drating);
            }
            else
            {
                Console.WriteLine("Invalid input");
                return false;
            }

            articlesList.Add(new Article(new Person(values[0], values[1], dateTime), values[3], drating));
            return true;
        }

        // Статические методы:
        public static bool Save(string fname, ref Magazine my_object)
        {
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.Create))
                {
                    // Через BinaryFormatter сериализуем вызывающий объект и передаём по потоку в файл
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, my_object);
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Error saving to file!");
                return false;
            }
        }

        public static bool Load(string fname, ref Magazine my_object)
        {
            // Во временной копии сохраним данные объекта на случай ошибки
            Magazine tempCopy = (Magazine)my_object.DeepCopy();
            try
            {
                using (FileStream fs = new FileStream(fname, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Magazine loadedObject = (Magazine)formatter.Deserialize(fs);

                    // Десериализуем объект из файлового потока и копируем оттуда данные в this
                    my_object.editionTitle = loadedObject.editionTitle;
                    my_object.editionRelease = loadedObject.editionRelease;
                    my_object.editionCirculation = loadedObject.editionCirculation;
                    my_object.frequency = loadedObject.frequency;
                    my_object.editorsList = new List<Person>(loadedObject.editorsList);
                    my_object.articlesList = new List<Article>(loadedObject.articlesList);

                    return true;
                }
            }

            catch
            {
                // При ошибке возвращаем исходные данные
                Console.WriteLine("Error loading from file!");
                my_object.editionTitle = tempCopy.editionTitle;
                my_object.editionRelease = tempCopy.editionRelease;
                my_object.editionCirculation = tempCopy.editionCirculation;
                my_object.frequency = tempCopy.frequency;
                my_object.editorsList = new List<Person>(tempCopy.editorsList);
                my_object.articlesList = new List<Article>(tempCopy.articlesList);
                return false;
            }
        }
    }
}
