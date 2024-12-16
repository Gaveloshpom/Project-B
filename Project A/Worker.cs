using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Worker : PersonBase, ICloneable
    {
        private int id;
        private Specialization specialization;


        public int Id 
        { 
            get { return  id; }
            set
            {
                string input = value.ToString();
                int val;
                if (!int.TryParse(input, out val))
                {
                    throw new ArgumentException("Ввід має бути числом");
                };
                id = value;
            }
        }

        public Specialization Specialization 
        { 
            get { return specialization; }
            set 
            {
                string input = value.ToString();
                if(!Enum.TryParse<Specialization>(input, out specialization)) { throw new ArgumentException("Некоректний ввід спеціалізації"); };
            } 
        }


        public Worker(int id, string firstName, string lastName, int age, Specialization specialization) : base(firstName, lastName, age)
        {
            Id = id;
            Specialization = specialization;
        }

        public void Promote(Specialization newSpecialization) 
        {
            if (Specialization == newSpecialization) { throw new ArgumentException("Помилка! Професія вже присвоєна"); }
            Specialization = newSpecialization;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public override string GetFullInfo() 
        {
            return $"Worker: {FirstName} {LastName} {Age} - {Specialization}";
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
