namespace Lab05_CSharp
{
    class ListEntry
    {
        public string collectionName;
        public Update why;
        public string propertyName;
        public string textElementKey;

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
            set
            {
                why = value;
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

        public string TextElementKey
        {
            get
            {
                return textElementKey;
            }
            set
            {
                textElementKey = value;
            }
        }

        public ListEntry(string my_collectionName, Update my_why, string my_PropertyName, string my_textElementKey)
        {
            collectionName = my_collectionName;
            why = my_why;
            propertyName = my_PropertyName;
            textElementKey = my_textElementKey;
        }

        public override string ToString()
        {
            return "Change in collection: " + collectionName + "\nChange type: " + why + "\nChange in property: " + propertyName + "\nElement key: " + textElementKey;
        }
    }
}
