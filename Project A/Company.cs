using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Company: IPrintable
    {
        public DateTime Founded;
        public string Name;
        public List<Brigade> Brigades;

        public void PrintToDisplay() 
        {
            throw new NotImplementedException();
        }
    }
}
