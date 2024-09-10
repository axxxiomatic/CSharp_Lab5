using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab05_CSharp
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    internal class TestCollections<TKey, TValue>
    {
        private List<TKey> listKeys;
        private List<string> stringList;
        private Dictionary<TKey, TValue> dictionaryKeys;
        private Dictionary<string, TValue> dictionaryStr;
        private GenerateElement<TKey, TValue> generateElement;

        public TestCollections(int count, GenerateElement<TKey, TValue> j)
        {
            listKeys = new List<TKey>();
            stringList = new List<string>();
            dictionaryKeys = new Dictionary<TKey, TValue>();
            dictionaryStr = new Dictionary<string, TValue>();
            generateElement = j;
            for (int i = 0; i < count; i++)
            {
                var elem = generateElement(i);
                dictionaryKeys.Add(elem.Key, elem.Value);
                dictionaryStr.Add(elem.Key.ToString(), elem.Value);
                listKeys.Add(elem.Key);
                stringList.Add(elem.Key.ToString());
            }
        }


        public static KeyValuePair<Edition, Magazine> GenerateElement(int j)
        {
            Edition key = new Edition("Edition" + j, new DateTime(2000 + j % 30, 1 + j % 12, 1 + j % 30), j);
            Magazine value = new Magazine("Magazine" + j, (Frequency)(j % 3), new DateTime(2000 + j % 21, 1 + j % 12, 1 + j % 30), j);
            return new KeyValuePair<Edition, Magazine>(key, value);
        }


        public void search_LIST_KEYS()
        {
            Console.WriteLine("\nIn the keys list: \nTime of search: \n");

            TKey first = listKeys[0];
            TKey middle = listKeys[listKeys.Count / 2];
            TKey last = listKeys[listKeys.Count - 1];
            TKey none = generateElement(listKeys.Count + 1).Key;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            listKeys.Contains(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            listKeys.Contains(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            listKeys.Contains(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            listKeys.Contains(none);
            sw.Stop();
            Console.WriteLine("For the element which is not in array: " + sw.Elapsed);
        }

        public void search_LIST_STR()
        {
            Console.WriteLine("\nIn the strings list: \nTime of search: \n");

            var first = stringList[0];
            var middle = stringList[stringList.Count / 2];
            var last = stringList[stringList.Count - 1];
            var none = generateElement(stringList.Count + 1).Key.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            stringList.Contains(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            stringList.Contains(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            stringList.Contains(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            stringList.Contains(none);
            sw.Stop();
            Console.WriteLine("For the element which is not in list: " + sw.Elapsed);
        }

        public void search_DICTIONARY_KEYS_ByKey()
        {
            Console.WriteLine("\nSearch in a key dictionary by key: \nTime of search: \n");

            TKey first = dictionaryKeys.ElementAt(0).Key;
            TKey middle = dictionaryKeys.ElementAt(dictionaryKeys.Count / 2).Key;
            TKey last = dictionaryKeys.ElementAt(dictionaryKeys.Count - 1).Key;
            TKey none = generateElement(dictionaryKeys.Count + 1).Key;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dictionaryKeys.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            dictionaryKeys.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            dictionaryKeys.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset();
            sw.Start();
            dictionaryKeys.ContainsKey(none);
            sw.Stop();
            Console.WriteLine("For the element which is not in list: " + sw.Elapsed);
        }

        public void search_DICTIONARY_STR_ByKey()
        {
            Console.WriteLine("\nSearch in a string dictionary by key: \nTime of search: \n");

            var first = dictionaryStr.ElementAt(0).Key;
            var middle = dictionaryStr.ElementAt(dictionaryStr.Count / 2).Key;
            var last = dictionaryStr.ElementAt(dictionaryStr.Count - 1).Key;
            var none = generateElement(dictionaryStr.Count + 1).Key.ToString();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dictionaryStr.ContainsKey(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryStr.ContainsKey(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryStr.ContainsKey(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryStr.ContainsKey(none);
            sw.Stop();
            Console.WriteLine("For the element which is not in list: " + sw.Elapsed);
        }

        public void search_DICTIONARY_KEYS_ByValue()
        {
            Console.WriteLine("\nSearch in a key dictionary by value: \nTime of search: \n");

            var first = dictionaryKeys.ElementAt(0).Value;
            var middle = dictionaryKeys.ElementAt(dictionaryKeys.Count / 2).Value;
            var last = dictionaryKeys.ElementAt(dictionaryKeys.Count - 1).Value;
            var none = generateElement(dictionaryKeys.Count + 1).Value;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dictionaryKeys.ContainsValue(first);
            sw.Stop();
            Console.WriteLine("For the first element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryKeys.ContainsValue(middle);
            sw.Stop();
            Console.WriteLine("For the middle element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryKeys.ContainsValue(last);
            sw.Stop();
            Console.WriteLine("For the last element: " + sw.Elapsed);

            sw.Reset(); sw.Start();
            dictionaryKeys.ContainsValue(none);
            sw.Stop();
            Console.WriteLine("For the element which is not in list: " + sw.Elapsed);
        }
    }
}
