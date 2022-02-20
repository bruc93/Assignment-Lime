using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory() + @"\freebusy.txt";


            Console.WriteLine(filePath);

            List<string> lines = File.ReadAllLines(filePath).ToList();
            Dictionary<string, Person> persons = new Dictionary<string, Person>();

            foreach (var line in lines)
            {
                string[] packageInfo = line.Split(';');
                if (packageInfo.Count() == 2 && packageInfo[0].Length > 0 && packageInfo[1].Length > 0)
                {
                    if(!persons.ContainsKey(packageInfo[0]))
                        persons.Add(packageInfo[0] ,new Person(packageInfo[0], packageInfo[1]));
                }
                else if (packageInfo.Count() == 4)
                {

                }
            }

            foreach (var person in persons)
            {
                Console.WriteLine(person.Value.id + ": " + person.Value.name);
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
