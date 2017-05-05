using System;
using System.Collections.Generic;
using System.ComponentModel;
using Alarm.Annotations;

namespace Alarm.Models
{
    public class CronType : INotifyPropertyChanged
    {
        public CronType()
        {
            Interval = 1;
            DateTime = DateTime.Now;
            Type = "∑÷÷”";
        }

        private string _type;
        private int _interval = 1;
        private DateTime _dateTime;
        private string _weeks;

        public string weeks
        {
            get { return _weeks; }
            set
            {
                _weeks = value;
                OnPropertyChanged(nameof(weeks));
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public int Interval
        {
            get { return _interval; }
            set
            {
                _interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged(nameof(DateTime));
            }
        }

        public string Expression { get; set; }
        public string Display { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}