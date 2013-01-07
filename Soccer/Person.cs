using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soccer
{
    public class Person
    {
        public string FirstName;
        public string LastName;
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
