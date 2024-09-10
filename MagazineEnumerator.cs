using System;
using System.Collections.Generic;

namespace Lab05_CSharp
{
    internal class MagazineEnumerator : System.Collections.IEnumerator
    {
        public List<Article> articles;
        public List<Person> persons;
        int position = -1;

        // Определение конструкторов
        public MagazineEnumerator()
        {
            articles = new List<Article>();
            persons = new List<Person>();

        }

        public MagazineEnumerator(List<Article> articles, List<Person> persons)
        {
            articles = new List<Article>(articles);
            persons = new List<Person>(persons);
        }

        public object Current
        {
            get
            {
                if (position == -1 || position >= articles.Count)
                    throw new IndexOutOfRangeException();
                return articles[position];
            }
        }

        public bool MoveNext()
        {
            if (position < articles.Count - 1)
            {
                position++;
                while (persons.Contains(((Article)articles[position]).person) && position < articles.Count - 1)
                {
                    position++;
                }
                if (!persons.Contains(((Article)articles[position]).person))
                    return true;
                return false;
            }
            return false;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
