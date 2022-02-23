using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Application
{
    internal class Scheduler
    {
       private Dictionary<string, Person> m_persons = new Dictionary<string, Person>();

       public bool LoadFile(string filePath)
       {
           List<string> lines = File.ReadAllLines(filePath).ToList();

           if (lines.Count <= 0)
               return false;

           foreach (var line in lines)
           {
               string[] packageInfo = line.Split(';');
               AddPackage(packageInfo);
           }

           if (m_persons.ElementAt(0).Value.m_meetings.ElementAt(0).Value.ElementAt(0)
                .IsOverlapping(new Meeting("3/12/2015", "7:00:00 AM", "1:30:00 PM")))
            {
                Console.WriteLine("COLLIDED");
            }

            return true;
       }

       public bool ScheduleMeeting(List<string> ids, int duration, string startDateTime, string endDateTime, int startHour, int endHour)
       {
           if (ids.Count < 1 || duration < 30 || (duration/60) > endHour-startHour)
               return false;


           DateTime startDate = Meeting.GenerateDateTime(startDateTime);
           DateTime endDate = Meeting.GenerateDateTime(endDateTime);

           // Uses the default calendar of the InvariantCulture.
           Calendar myCal = CultureInfo.InvariantCulture.Calendar;

            while (startDate < endDate)
            {

                //Generate new meeting
                Meeting currentMeeting = new Meeting(startDate, myCal.AddMinutes(startDate, duration));
                bool isColliding = false;

                foreach (var id in ids)
                {
                    if (m_persons.ContainsKey(id))
                    {
                        if (m_persons[id].OverlappingMeetings(currentMeeting))
                        {
                            isColliding = true;
                            break;
                        }

                    }
                }

                if (!isColliding)
                {
                    Console.WriteLine("Meeting can start at: " + currentMeeting.GetStartTime().ToShortDateString() + " " + currentMeeting.GetStartTime().ToShortTimeString());
                    Console.WriteLine("Meeting will end at: " + currentMeeting.GetEndTime().ToShortDateString() + " " + currentMeeting.GetEndTime().ToShortTimeString());
                    return true;
                }
                    

                //Step 30 min each iteration
                startDate = myCal.AddMinutes(startDate, 30);

                if (startDate.Hour >= endHour || currentMeeting.GetEndTime().Hour > endHour)
                {
                    startDate = myCal.AddDays(startDate, 1);
                    //Reset hours back to start hour
                    startDate = myCal.AddHours(startDate, -(startDate.Hour-startHour));
                }
            }

            return false;
       }


       //adds the information in packageInfo to m_persons dictionary
       private void AddPackage(string[] packageInfo)
       {
           if (packageInfo.Count() == 2 && packageInfo[0].Length > 0 && packageInfo[1].Length > 0)
           {
               if (!m_persons.ContainsKey(packageInfo[0]))
               {
                   m_persons.Add(packageInfo[0], new Person(packageInfo[1], packageInfo[0]));
               }
               else if (m_persons.ContainsKey(packageInfo[0]) && m_persons[packageInfo[0]].name == "NULL")
               {
                   m_persons[packageInfo[0]].name = packageInfo[1];
               }
           }
           else if (packageInfo.Count() == 4)
           {
               string[] startTimeInfo = packageInfo[1].Split(' ');
               string[] endTimeInfo = packageInfo[2].Split(' ');

               if (startTimeInfo.Count() < 3 || endTimeInfo.Count() < 3)
                   return;

               Meeting meeting = new Meeting(startTimeInfo[0], startTimeInfo[1] + " " + startTimeInfo[2],
                   endTimeInfo[1] + " " + endTimeInfo[2]);

               if (!m_persons.ContainsKey(packageInfo[0]))
                   m_persons.Add(packageInfo[0], new Person("NULL", packageInfo[0]));

               m_persons[packageInfo[0]].AddMeeting(meeting);
           }
       }
    }
}
