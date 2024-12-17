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
        private List<Brigade> brigades;

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
            brigades = new List<Brigade>();
        }

        public static Company Parse(string s)
        {
            string[] input = s.Split(" ");
            int count = input.Length;

            if (input.Length != 2)
            {
                throw new ArgumentException("Некоректна кіл-ть даних");
            }

            return new(toDate(input[1]), input[0]);
        }

        public static bool TryParse(string s, out Company obj)
        {
            try
            {
                obj = Parse(s);
                return true;
            }
            catch
            {
                obj = null;
                return false;
            }
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
            Console.WriteLine($"Компанія: {Name} | Дата заснування: {Founded} | Кіл-ть бригад: {brigades.Count} | Кіл-ть робітників: {GetTotalWorkers()}");
        }

        public static DateOnly toDate(string input)
        {
            DateOnly date;

            if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }
            else
            {
                throw new Exception("Некоректний ввід дати");
            }
        }
    }
}
