using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Company: IPrintable
    {
        private DateTime founded;
        private string name;
        private List<Brigade> brigades;

        public DateTime Founded;
        public string Name;
        public List<Brigade> Brigades;

        void AddBrigade(Brigade brigade) 
        {
            throw new NotImplementedException();
        }

        void DeleteBrigade(Brigade brigade)
        {
            throw new NotImplementedException();
        }

        public void PrintToDisplay() 
        {
            throw new NotImplementedException();
        }
    }
}
