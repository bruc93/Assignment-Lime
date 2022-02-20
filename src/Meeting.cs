using System;
using System.Collections.Generic;

namespace Application
{
    internal class Meeting
    {
        //Data
        private List<string> ids;
        private string date;
        private float startTime;
        private float endTime;
        private int duration;

        //Constructor
        Meeting(List<string> _ids, string _Date, float _startTime, int _duration)
        {
            this.ids = _ids;
            this.date = _Date;
            this.startTime = _startTime;
            this.endTime = startTime + _duration;
            this.duration = _duration;
        }

        //Check if meetings overlaps
        public bool IsOverlapping(Meeting _otherMeeting)
        {
            if (_otherMeeting == null) throw new ArgumentNullException(nameof(_otherMeeting));

            
            if (this.date == _otherMeeting.date)
            {

                if(this.startTime <= _otherMeeting.startTime && this.endTime > _otherMeeting.startTime)
                    return true;

                if (this.startTime > _otherMeeting.startTime && this.startTime < _otherMeeting.endTime)
                    return true;

            }

            return false;
        }
    }
}
