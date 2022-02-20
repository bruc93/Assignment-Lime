// See https://aka.ms/new-console-template for more information

using Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;




namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Directory.GetCurrentDirectory() + @"\freebusy.txt";


            Console.WriteLine(filePath);

            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<Person> persons = new List<Person>();

            foreach (var line in lines)
            {
                string[] personInfo = line.Split(';');
                if (personInfo.Count() == 2)
                {
                    persons.Add(new Person(personInfo[0], personInfo[1]));
                }

            }

            foreach (var person in persons)
            {
                Console.WriteLine(person.id + ": " + person.name);
            }

            Console.WriteLine("Hello World!");
        }
    }
}