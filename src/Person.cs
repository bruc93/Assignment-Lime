using System.Collections.Generic;

namespace Application
{
    internal class Person
    {
        public string name { get; set; }
        public string id { get; set; }

        public Dictionary<string,Meeting> m_meetings { get; set; }
        public Person(string _name, string _id)
        {
            this.name = _name;
            this.id = _id;
        }

    }
}
