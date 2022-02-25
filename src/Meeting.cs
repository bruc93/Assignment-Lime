using System;
using System.Collections.Generic;
using System.Globalization;

namespace Application
{
    internal class Meeting
    {
        // Data
        private DateTime startDateTime;
        private DateTime endDateTime;
        // Constructor
        public Meeting(string _Date, string _startTime, string _endTime)
        {
            startDateTime = GenerateDateTime(_Date + " " + _startTime);
            endDateTime = GenerateDateTime(_Date + " " + _endTime);
        }
        public Meeting(DateTime _startTime, DateTime _endTime)
        {
            startDateTime = _startTime;
            endDateTime = _endTime;
        }
        // Generate a DateTime based on the string
        public static DateTime GenerateDateTime(string _time)
        {
            // Deconstruct string into separate pieces
            string[] startInfo = _time.Split(' ');
            string[] dateInfo = startInfo[0].Split('/');
            string[] timeStamp = startInfo[1].Split(':');
            // Parse string data into int
            int month = Int32.Parse(dateInfo[0]);
            int day = Int32.Parse(dateInfo[1]);
            int year = Int32.Parse(dateInfo[2]);
            int hour = Int32.Parse(timeStamp[0]);
            int mins = Int32.Parse(timeStamp[1]);
            // Construct DateTime class
            DateTime dateTime = new DateTime(year, month, day, new GregorianCalendar());
            // Uses the default calendar of the InvariantCulture.
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            // Change to digital time for easy conversion
            if (startInfo[2] == "PM")
                hour += 12;
            // Add hours and mins to the date time
            dateTime = myCal.AddHours(dateTime, hour);
            dateTime = myCal.AddMinutes(dateTime, mins);

            return dateTime;
        }


        // Check if meetings overlaps
        public bool IsOverlapping(Meeting _otherMeeting)
        {
            //Check if dates aligned 
            if (this.startDateTime.ToShortDateString() == _otherMeeting.GetStartTime().ToShortDateString())
            {
                float thisStartTime = this.startDateTime.Hour + this.startDateTime.Minute * 0.01f;
                float thisEndTime = this.endDateTime.Hour + this.endDateTime.Minute * 0.01f;

                float otherStartTime = _otherMeeting.GetStartTime().Hour + _otherMeeting.GetStartTime().Minute * 0.01f;
                float otherEndTime = _otherMeeting.GetEndTime().Hour + _otherMeeting.GetEndTime().Minute * 0.01f;

                if (thisStartTime >= otherStartTime && thisStartTime <= otherEndTime)
                   return true;

                if (thisEndTime >= otherStartTime && thisEndTime <= otherEndTime)
                   return true;
            }

            return false;
        }
        // Returns starting time for this meeting
        public DateTime GetStartTime()
        {
            return this.startDateTime;
        }
        // Returns end time for this meeting
        public DateTime GetEndTime()
        {
            return this.endDateTime;
        }

    }
}
