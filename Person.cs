using System;

namespace Lab05_CSharp
{
    [Serializable]
    internal class Person
    {
        private string name;
        private string surname;
        DateTime birthday;

        // Определение конструкторов
        public Person(string name, string surname, DateTime birthday)
        {
            this.name = name;
            this.surname = surname;
            this.birthday = birthday;
        }
        public Person() : this("Name", "Surname", new DateTime(2001, 9, 11)) { }

        // Определение get и set методов
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }
        public int Year
        {
            get
            {
                return Birthday.Year;
            }
            set
            {
                Birthday = new DateTime(value, Birthday.Month, Birthday.Day);
            }
        }

        public override string ToString()
        {
            return Name + " " + Surname + " " + Birthday.ToShortDateString();
        }
        public virtual string ToShortString()
        {
            return Name + " " + Surname;
        }

        public override bool Equals(object obj1)
        {
            if (obj1 == null)
            {
                return false;
            }
            Person obj = obj1 as Person;
            return name.Equals(obj.name) && surname.Equals(obj.surname) && birthday.Equals(obj.birthday);
        }

        public static bool operator ==(Person obj1, Person obj2)
        {
            return obj1.name == obj2.name && obj1.surname == obj2.surname && obj1.birthday == obj2.birthday;
        }

        public static bool operator !=(Person obj1, Person obj2)
        {
            return obj1.name != obj2.name || obj1.surname != obj2.surname || obj1.birthday != obj2.birthday;

        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ surname.GetHashCode() ^ birthday.GetHashCode();
        }

        public object DeepCopy()
        {
            Person obj = new Person();
            obj.name = name;
            obj.surname = surname;
            obj.birthday = birthday;
            return obj;
        }
    }
}
