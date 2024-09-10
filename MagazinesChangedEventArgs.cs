using System;

namespace Lab05_CSharp
{
    internal class MagazinesChangedEventArgs<TKey> : EventArgs
    {
        public string collectionName;
        public Update why;
        public string propertyName;
        public TKey elementKey;

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

        public Update Why
        {
            get
            {
                return why;
            }
        }

        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                propertyName = value;
            }
        }

        public TKey ElementKey
        {
            get
            {
                return elementKey;
            }
            set
            {
                elementKey = value;
            }
        }

        public MagazinesChangedEventArgs(string collection_name, Update my_why, string property_Name, TKey element_Key)
        {
            collectionName = collection_name;
            why = my_why;
            propertyName = property_Name;
            elementKey = element_Key;
        }

        public override string ToString()
        {
            return "Change in collection: " + collectionName + "\nChange type: " + why + "\nChange in property: " + propertyName + "\nElement key: " + elementKey;
        }
    }
}
