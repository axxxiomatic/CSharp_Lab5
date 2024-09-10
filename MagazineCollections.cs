using System;
using Lab05_CSharp;
using System.Collections.Generic;
using System.Linq;

namespace Lab05_Csharp
{
    delegate TKey KeySelector<TKey>(Magazine value);
    delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

    internal class MagazineCollections<TKey>
    {
        private Dictionary<TKey, Magazine> magazineCollection;
        private KeySelector<TKey> keySelector;
        public string collectionName;

        public string CollectionName
        {
            get
            {
                return collectionName;
            }
            set
            {
                collectionName = value;
            }
        }

        public bool Replace(Magazine magold, Magazine magnew)
        {
            if (magazineCollection.ContainsValue(magold))
            {
                foreach (KeyValuePair<TKey, Magazine> magazine_pair in magazineCollection)
                {
                    if (magazine_pair.Value == magold)
                    {
                        magazineCollection[magazine_pair.Key] = magnew;
                        MagazinePropertyChanged(Update.Replace, "None", magazine_pair.Key);
                        magold.PropertyChanged -= PropertyChangeded;
                        magnew.PropertyChanged += PropertyChangeded;
                        break;
                    }
                }
                return true;
            }
            else return false;
        }

        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        private void MagazinePropertyChanged(Update update, string name, TKey key)
        {
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(collectionName, update, name, key));
        }

        private void PropertyChangeded(object sourse, EventArgs args)
        {
            MagazinePropertyChanged(Update.Property, (args as MagazinesChangedEventArgs<string>).propertyName, keySelector((Magazine)sourse));
        }

        public MagazineCollections(KeySelector<TKey> keySelectorValue)
        {
            keySelector = keySelectorValue;
            magazineCollection = new Dictionary<TKey, Magazine>();
        }

        public static string Define_Key(Magazine val)
        {
            return val.EditionCirculation.ToString();
        }

        public void AddDefaults()
        {
            Magazine m = new Magazine();
            magazineCollection.Add(keySelector(m), m);
        }

        public void AddMagazines(params Magazine[] value)
        {
            foreach (Magazine magazine in value)
            {
                magazineCollection.Add(keySelector(magazine), magazine);
                foreach (KeyValuePair<TKey, Magazine> magazine_pair in magazineCollection)
                {
                        MagazinePropertyChanged(Update.Add, "AddMagazines", magazine_pair.Key);
                        magazine.PropertyChanged += PropertyChangeded;
                        break;
                }
                
            }
        }

        public override string ToString()
        {
            foreach (KeyValuePair<TKey, Magazine> magazine in magazineCollection)
            {
                Console.WriteLine($"TKey = {magazine.Key} => Value:");
                Console.WriteLine(magazine.Value.ToString());
            }
            return "\n";
        }

        public virtual string ToShortString()
        {
            foreach (KeyValuePair<TKey, Magazine> magazine in magazineCollection)
            {
                Console.WriteLine($"TKey = {magazine.Key} => Value:");
                Console.WriteLine(magazine.Value.ToShortString());
            }
            return "\n";
        }

        public double MaxRating
        {
            get
            {
                if (magazineCollection.Count > 0) return magazineCollection.Values.Max(m => m.Rating);
                return 0;
            }
        }
        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return magazineCollection.Where(item => item.Value.How_often == value);
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> GroupCollection
        {
            get
            {
                return magazineCollection.GroupBy(item => item.Value.How_often);
            }
        }
    }
}
