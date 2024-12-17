using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_A
{
    public class BrigadeCommander : PersonBase, ICloneable
    {
        public override string GetFullInfo()
        {
            return $"Бригадний командир: {FirstName} {LastName} {Age}";
        }

        public BrigadeCommander(string firstName, string lastName, int age) : base(firstName, lastName, age) {}

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
