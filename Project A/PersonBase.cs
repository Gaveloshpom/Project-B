using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_A
{
    public abstract class PersonBase : IPerson
    {

        private string firstName;
        private string lastName;
        private int age;


        public string FirstName
        {
            get { return firstName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.firstName = value; }
                else { throw new ArgumentException("Некоректний ввід імені"); }
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.lastName = value; }
                else { throw new ArgumentException("Некоректний ввід прізвища"); }
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 18 || value > 70)
                    throw new ArgumentException("Вік має бути в межах від 18 до 70");
                age = value;
            }
        }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        protected PersonBase(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public abstract string GetFullInfo();
    }
}
