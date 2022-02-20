using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Person
    {
        public string name { get; set; }
        public string id { get; set; }

        public Person(string _name, string _id)
        {
            this.name = _name;
            this.id = _id;
        }

    }
}
