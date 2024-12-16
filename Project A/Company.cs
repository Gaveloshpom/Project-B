using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_A
{
    public class Company: IPrintable
    {
        private DateOnly founded;
        private string name;
        private List<Brigade> brigades = new List<Brigade> { };

        public DateOnly Founded 
        {
            get { return founded; }
            set 
            {
                if (value > DateOnly.FromDateTime(DateTime.Now)) { throw new ArgumentException("Некоректний ввід дати(Дата з майбутнього)"); }
                
                founded = value;
            } 
        }
        public string Name 
        {
            get {return name;}
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁіІїЇєЄґҐ0-9\s\-\']{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід назви компанії"); };
            }
        }

        public List<Brigade> Brigades { get { return brigades; } }

        public Company(DateOnly founded, string name) 
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
            Console.WriteLine($"\nКомпанія: {Name} | Дата заснування: {Founded} | Кіл-ть бригад: {brigades.Count} | Кіл-ть робітників: {GetTotalWorkers()}");
        }
    }
}
