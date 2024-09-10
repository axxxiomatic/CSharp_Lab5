using System;
using System.Collections.Generic;

namespace Lab05_CSharp
{
    [Serializable]
    internal class Article : IRateAndCopy, IComparable, IComparer<Article>
    {
        public Person person;
        public string articleName;
        public double articleRating;

        // Определение конструкторов
        public Article(Person person, string articleName, double articleRating)
        {
            this.person = person;
            this.articleName = articleName;
            this.articleRating = articleRating;
        }

        public Article() : this(new Person(), "Unnamed", 0) { }

        // Определение get и set методов
        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                person = value;
            }
        }

        public string ArticleName
        {
            get
            {
                return articleName;
            }
            set
            {
                articleName = value;
            }
        }

        public double Rating
        {
            get
            {
                return articleRating;
            }
            set
            {
                articleRating = value;
            }
        }

        // Определения перегруженных методов и операторов
        public override string ToString()
        {
            return person.Name + " " + person.Surname + " " + person.Birthday.ToShortDateString() + " " + articleName + " " + articleRating;
        }

        public override bool Equals(Object obj1)
        {
            if (obj1 == null)
            {
                return false;
            }
            Article obj = obj1 as Article;
            if (obj1 as Article == null)
            {
                return false;
            }
            else return person.Equals(obj.person) && articleName.Equals(obj.articleName) && articleRating.Equals(obj.articleRating);
        }

        public static bool operator ==(Article obj1, Article obj2)
        {
            if (obj1 is null || obj2 is null)
            {
                return false;
            }
            if (obj1.person == obj2.person)
            {
                if (obj1.articleName == obj2.articleName)
                {
                    if (obj1.articleRating == obj2.articleRating)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        public static bool operator !=(Article obj1, Article obj2)
        {
            if (obj1 is null || obj2 is null)
            {
                return false;
            }
            if (obj1.person != obj2.person)
            {
                return true;
            }
            else if (obj1.articleName != obj2.articleName)
            {
                return true;
            }
            else if (obj1.articleRating != obj2.articleRating)
            {
                return true;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return person.GetHashCode() ^ articleName.GetHashCode() ^ articleRating.GetHashCode();
        }

        public object DeepCopy()
        {
            Article obj = new Article();
            obj.person = person.DeepCopy() as Person;
            obj.articleName = articleName;
            obj.articleRating = articleRating;
            return obj;
        }

        // Реализация интерфейсов
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var obj1 = obj as Article;
            if (obj1 != null)
            {
                return this.articleName.CompareTo(obj1.articleName);
            }
            else
            {
                if (obj is null)
                {
                    return 1;
                }
                else
                {
                    //throw new ArgumentException("Объект не является статьёй.");
                    return 0;
                }
            }
        }

        public int Compare(Article x, Article y)
        {
            return x.person.Surname.CompareTo(y.person.Surname);
        }

        public int CompareName(Article x, Article y)
        {
            return x.person.Name.CompareTo(y.person.Name);
        }
    }
}
