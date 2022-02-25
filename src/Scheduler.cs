using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Application
{
    internal class Scheduler
    {
       // Key = Date string, Value = Person
       private Dictionary<string, Person> m_persons = new Dictionary<string, Person>();
       
       // Loads text file into the system
       public bool LoadFile(string filePath)
       {
           // Store all lines from file in list
           List<string> lines = File.ReadAllLines(filePath).ToList();
           // If no lines leave
           if (lines.Count <= 0)
               return false;
           // Iterate through all lines and generate packages to load into system
           foreach (var line in lines)
           {
               string[] packageInfo = line.Split(';');
               AddPackage(packageInfo);
           }

           return true;
       }

       // Try to schedule a new meeting for all involved based on the given parameters
       public Meeting ScheduleMeeting(List<string> ids, int duration, string startDateTime, string endDateTime, int startHour, int endHour)
       {
           // Early control check
           if (ids.Count < 1 || duration < 30 || (duration/60) > endHour-startHour || startHour < 0 || startHour > 23 || endHour < 0 || endHour > 23)
               return null;
           
           // Generate DateTimes
           DateTime startDate = Meeting.GenerateDateTime(startDateTime);
           DateTime endDate = Meeting.GenerateDateTime(endDateTime);

           // Uses the default calendar of the InvariantCulture.
           Calendar myCal = CultureInfo.InvariantCulture.Calendar;

           // Iterate through all days until reaching end date
           while (startDate < endDate)
           {
               // Generate new meeting based on startDate
               Meeting currentMeeting = new Meeting(startDate, myCal.AddMinutes(startDate, duration));
               // If new meeting goes over office hours skip to next date
               if (currentMeeting.GetEndTime().Hour > endHour)
               {
                   startDate = ResetStartDate(startHour, startDate, myCal);
                   continue;
               }
               // If a meeting collides for person with another meeting break and end this loop
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
               // No collision detected can return a fit
               if (!isColliding)
               {
                   return currentMeeting;
               }
               // Step 30 min each iteration
               startDate = myCal.AddMinutes(startDate, 30);
               // Reset hours back to start hour and add a day
               if (startDate.Hour >= endHour || currentMeeting.GetEndTime().Hour > endHour)
               {
                   startDate = ResetStartDate(startHour, startDate, myCal);
               }
           }

           return null;
       }
       // Reset the start date back to the beginning of the office hour on the next day!
       private static DateTime ResetStartDate(int startHour, DateTime startDate, Calendar myCal)
       {
           startDate = myCal.AddDays(startDate, 1);
           startDate = myCal.AddHours(startDate, -(startDate.Hour - startHour));
           return startDate;
       }
       // Adds information generated into the m_persons container
       private void AddPackage(string[] packageInfo)
       {
           // Type 1 package with Employee ID and Name
           if (packageInfo.Count() == 2 && packageInfo[0].Length > 0 && packageInfo[1].Length > 0)
           {
               // If employee does not exist in the system create a new
               if (!m_persons.ContainsKey(packageInfo[0]))
               {
                   m_persons.Add(packageInfo[0], new Person(packageInfo[1], packageInfo[0]));
               }
               // If employee does exist in the system but has not been assigned a name, give it!
               else if (m_persons.ContainsKey(packageInfo[0]) && m_persons[packageInfo[0]].name == "NULL")
               {
                   m_persons[packageInfo[0]].name = packageInfo[1];
               }
           }
           // Type 2 package with meeting information for a employee
           else if (packageInfo.Count() == 4)
           {
               // split the string into pieces for easy withdrawal
               string[] startTimeInfo = packageInfo[1].Split(' ');
               string[] endTimeInfo = packageInfo[2].Split(' ');
               // safety check
               if (startTimeInfo.Count() < 3 || endTimeInfo.Count() < 3)
                   return;
               // Generate meeting based on incoming package information
               Meeting meeting = new Meeting(startTimeInfo[0], startTimeInfo[1] + " " + startTimeInfo[2],
                   endTimeInfo[1] + " " + endTimeInfo[2]);
               // If employee does not exist in the system add for the future
               if (!m_persons.ContainsKey(packageInfo[0])) 
                   m_persons.Add(packageInfo[0], new Person("NULL", packageInfo[0]));
               // Add the meeting for the affected person in the system 
               m_persons[packageInfo[0]].AddMeeting(meeting);
           }
       }
       // Return all the persons in the system as a dictionary
       public Dictionary<string, Person> GetPersons()
       {
           return this.m_persons;
       }
    }
}
