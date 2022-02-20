// See https://aka.ms/new-console-template for more information

using Project;
using System;
using System.IO;

Console.WriteLine("Hello, World!");

string filePath = Directory.GetCurrentDirectory() + @"\freebusy-1.0.0.txt";


Console.WriteLine(filePath);

List<string> lines = File.ReadAllLines(filePath).ToList();


foreach (var line in lines)
{
    Console.WriteLine(line);
}

//Console.ReadLine();






if (args.Length > 0)
{

}
else
{
    Console.WriteLine("No arguments");
}

