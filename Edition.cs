using System;
using System.ComponentModel;

namespace Lab05_CSharp
{
    [Serializable]
    internal class Edition
    {
        protected string editionTitle;
        protected DateTime editionRelease;
        protected int editionCirculation;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Edition_number_Changed(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Edition_DateOfPublish_Changed(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Edition_Circulation_Changed(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Определение конструкторов
        public Edition(string editionTitle, DateTime editionRelease, int editionCirculation)
        {
            this.editionTitle = editionTitle;
            this.editionRelease = editionRelease;
            this.editionCirculation = editionCirculation;
        }

        public Edition() : this("Header", new DateTime(2001, 9, 11), 0) { }

        // Определение get и set методов
        public string EditionTitle
        {
            get { return editionTitle; }
            set { editionTitle = value; }
        }
        public DateTime EditionRelease
        {
            get { return editionRelease; }
            set { editionRelease = value; }
        }
        public int EditionCirculation
        {
            get { return editionCirculation; }
            set 
            {
                if (value < 0) throw new Exception("Incorrect input.\n");
                editionCirculation = value;
            }
        }

        public virtual object DeepCopy()
        {
            Edition obj = new Edition();
            obj.editionTitle = editionTitle;
            obj.editionRelease = editionRelease;
            obj.editionCirculation = editionCirculation;
            return obj;
        }

        public override bool Equals(object obj1)
        {
            if (obj1 == null)
            {
                return false;
            }
            Edition obj = obj1 as Edition;
            return editionTitle.Equals(obj.editionTitle) && editionRelease.Equals(obj.editionRelease) && editionCirculation.Equals(obj.editionCirculation);
        }

        public static bool operator ==(Edition obj1, Edition obj2)
        {
            return obj1.editionTitle == obj2.editionTitle && obj1.editionRelease == obj2.editionRelease && obj1.editionCirculation == obj2.editionCirculation;
        }

        public static bool operator !=(Edition obj1, Edition obj2)
        {
            return obj1.editionTitle != obj2.editionTitle || obj1.editionRelease != obj2.editionRelease || obj1.editionCirculation != obj2.editionCirculation;
        }

        public override int GetHashCode()
        {
            return editionTitle.GetHashCode() ^ editionCirculation.GetHashCode() ^ editionRelease.GetHashCode();
        }

        public override string ToString()
        {
            return editionTitle + " " + editionRelease.ToShortDateString() + " " + editionCirculation;
        }

    }
}
