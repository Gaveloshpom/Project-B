using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class Worker: IPerson
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Specialization Specialization { get; set; }

        public string GetFullName()
        {
            throw new NotImplementedException();
        }

        Worker(int id, string firstName, string lastName, int age, Specialization specialization)
        {
            throw new NotImplementedException();
        }
    }
}
