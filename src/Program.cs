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
            // Filepath to the text data that needs to be loaded
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

            // Print result of simple tests that were performed
            Console.WriteLine(RunTest1(scheduler) ? "[x] Test1 OK" : "[-] Test1 FAILED");
            Console.WriteLine(RunTest2(scheduler) ? "[x] Test2 OK" : "[-] Test2 FAILED");

            Console.ReadLine();
        }

        private static bool RunTest1(Scheduler scheduler)
        {
            List<string> ids = new List<string>();

            // Six random ids picked from the text file freebusy.txt
            ids.Add("276908764613820584354290536660008166629");
            ids.Add("103222943108469712161093620402295866178");
            ids.Add("48639959687376052586683994275030460621");
            ids.Add("76611992019510366901071996520955952903");
            ids.Add("69405402307022621117331807458848852688");
            ids.Add("307735198406951055033400449967417104845");

            // Get a scheduleMeeting based on the time strings that fits everyone
            Meeting scheduledMeeting =
                scheduler.ScheduleMeeting(ids, 240, "1/22/2015 8:00:00 AM", "1/30/2015 9:00:00 AM", 12, 16);

            if (scheduledMeeting == null)
                return false;

            // Print result of the meeting
            Console.WriteLine("Meeting can start at: " + scheduledMeeting.GetStartTime().ToShortDateString() + " " +
                              scheduledMeeting.GetStartTime().ToShortTimeString());
            Console.WriteLine("Meeting will end at: " + scheduledMeeting.GetEndTime().ToShortDateString() + " " +
                              scheduledMeeting.GetEndTime().ToShortTimeString());
            return true;
        }

        private static bool RunTest2(Scheduler scheduler)
        {
            List<string> ids = new List<string>();
            // Two random ids from the text file
            string id1 = "276908764613820584354290536660008166629";
            string id2 = "103222943108469712161093620402295866178";

            // Add to package for parameter
            ids.Add(id1);
            ids.Add(id2);

            // Get a meeting on a limited time frame on date that is known to not have any meetings.
            Meeting scheduledMeeting =
                scheduler.ScheduleMeeting(ids, 120, "6/22/2015 8:00:00 AM", "6/22/2015 11:00:00 AM", 8, 10);

            // Add this meeting to both persons
            scheduler.GetPersons()[id1].AddMeeting(scheduledMeeting);
            scheduler.GetPersons()[id2].AddMeeting(scheduledMeeting);

            // Should not be possible to create a new meeting on the same time!
            Meeting scheduledMeeting2 =
                scheduler.ScheduleMeeting(ids, 120, "6/22/2015 8:00:00 AM", "6/22/2015 11:00:00 AM", 8, 10);

            //Should come back as null ( not possible to schedule here)
            if (scheduledMeeting2 == null)
                return true;

            return false;
        }
    }
}
