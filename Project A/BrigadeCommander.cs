using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class BrigadeCommander : PersonBase
    {
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public override string GetFullInfo()
        {
            return $"Brigade Commander: {FirstName} {LastName} {Age}";
        }

        public BrigadeCommander(string firstName, string lastName, int age) : base(firstName, lastName, age) {}

    }
}
