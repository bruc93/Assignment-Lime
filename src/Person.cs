using System;
using System.Collections.Generic;

namespace Application
{
    internal class Person
    {
        public string name { get; set; }
        public string id { get; set; }
        //Key holds the date of the meetings
        public Dictionary<string,List<Meeting>> m_meetings { get; set; }
        // Constructor
        public Person(string _name, string _id)
        {
            this.name = _name ?? throw new ArgumentNullException(nameof(_name));
            this.id = _id ?? throw new ArgumentNullException(nameof(_id));
            m_meetings = new Dictionary<string, List<Meeting>>();
        }
        // Add _meeting to this persons meetings
        public void AddMeeting(Meeting _meeting)
        {
            // Safety check
            if (_meeting == null) throw new ArgumentNullException(nameof(_meeting));
            // Check if key date exists, if not add this new date and add the meeting 
            if (!m_meetings.ContainsKey(_meeting.GetStartTime().ToShortDateString()))
            {
               m_meetings.Add(_meeting.GetStartTime().ToShortDateString(), new List<Meeting>());
               m_meetings[_meeting.GetStartTime().ToShortDateString()].Add(_meeting);
            }
            else
            {
                m_meetings[_meeting.GetStartTime().ToShortDateString()].Add(_meeting);
            }
        }

        //Check if meeting overlaps any of the current meeting on that day for this person
        public bool OverlappingMeetings(Meeting scheduledMeeting)
        {
            // Check if key exists in container
            if (!m_meetings.ContainsKey(scheduledMeeting.GetStartTime().ToShortDateString()))
            {
                return false;
            }
            // Iterate through all the meetings this person has
            foreach (var meeting in m_meetings[scheduledMeeting.GetStartTime().ToShortDateString()])
            {
                if (meeting.IsOverlapping(scheduledMeeting))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
