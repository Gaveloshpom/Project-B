using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_A
{
    public class Company: IPrintable
    {
        private DateTime founded;
        private string name;
        private List<Brigade> brigades = new List<Brigade> { };

        public DateTime Founded { get; set; }
        public string Name 
        {
            get {return name;}
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід назви компанії"); };
            }
        }

        public List<Brigade> Brigades { get; }

        public Company(DateTime founded, string name) 
        {
            Founded = founded;
            Name = name;
        }

        public void AddBrigade(Brigade brigade) 
        {
            brigades.Add(brigade);
        }

        public void DeleteBrigade(Brigade brigade)
        {
            brigades.Remove(brigade);
        }

        public int GetTotalWorkers() 
        {
            int result = 0;
            foreach (var brig in Brigades) 
            {
                result += brig.GetWorkerCount();
            };
            return result;
        }

        public void PrintToDisplay() 
        {
            Console.WriteLine($"\nКомпанія: {Name}, Дата Заснування: {Founded}, Кіл-ть робітників: {GetTotalWorkers}");
        }
    }
}
