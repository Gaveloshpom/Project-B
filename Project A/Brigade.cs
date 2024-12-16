using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_A
{
    public class Brigade
    {
        private string name;
        private BrigadeCommander brigadeCommander;
        private List<Worker> workers = new List<Worker> { };
        private string location;

        public string Name
        {
            get { return name; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід назви бригади"); };
            }
        }
        public BrigadeCommander BrigadeCommander { get; set; }
        public List<Worker> Workers { get;}
        public string Location
        {
            get { return location; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Zа-яА-ЯёЁїЇіІєЄґҐ]{3,20}$");
                if (regex.IsMatch(value)) { this.name = value; }
                else { throw new ArgumentException("Некоректний ввід локації"); };
            }
        }

        public Brigade(string name, BrigadeCommander brigadeCommander, string location) 
        {
            Name = name;
            BrigadeCommander = brigadeCommander;
            Location = location;
        }

        public void AddWorker(Worker worker) 
        {
            workers.Add(worker);
        }

        public bool RemoveWorker(int workerId) 
        {
            try
            {
                int index = workers.FindIndex(worker => worker.Id == workerId);
                workers.RemoveAt(index);
                return true;
            }
            catch { return false; }
        }

        public int GetWorkerCount() 
        {
            return workers.Count + 1;
        }
    }
}
