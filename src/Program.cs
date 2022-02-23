using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {

            string filePath = Directory.GetCurrentDirectory() + @"\freebusy.txt";
            Scheduler scheduler = new Scheduler();
            try
            {
                if(!scheduler.LoadFile(filePath))
                    Console.WriteLine("[-] ERROR: Failed to load file...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }




            List<string> ids = new List<string>();

            ids.Add("276908764613820584354290536660008166629");
            ids.Add("103222943108469712161093620402295866178");
            ids.Add("48639959687376052586683994275030460621");
            ids.Add("76611992019510366901071996520955952903");
            ids.Add("69405402307022621117331807458848852688");
            ids.Add("307735198406951055033400449967417104845");



            scheduler.ScheduleMeeting(ids, 240*2, "1/22/2015 8:00:00 AM", "1/30/2015 9:00:00 AM", 8, 16);

            //foreach (var person in persons)
            //{
            //    Console.WriteLine(person.Value.id + ": " + person.Value.name);
            //}

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
