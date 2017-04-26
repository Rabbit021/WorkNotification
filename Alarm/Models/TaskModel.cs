using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Alarm.Annotations;
using Alarm.CommonLib;
using LiteDB;

namespace Alarm.Models
{
    public class TaskModel : INotifyPropertyChanged, IJobEntity, ITriggerEntity
    {
        private string _id;
        private string _state;
        private string _title;
        private string _content;
        private string _type;
        private string _time;
        private string _date;
        private string _weekdays;
        private string _audio;
        private string _expression;
        private string _display;

        public TaskModel()
        {
            id = Guid.NewGuid().ToString();
        }

        [BsonId]
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(id));
            }
        }

        public string state
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPropertyChanged(nameof(state));
            }
        }

        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(title));
            }
        }

        public string content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged(nameof(content));
            }
        }

        public string type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(type));
            }
        }

        public string time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(time));
            }
        }

        public string date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(date));
            }
        }

        public string weekdays
        {
            get { return _weekdays; }
            set
            {
                _weekdays = value;
                OnPropertyChanged(nameof(weekdays));
            }
        }

        public string audio
        {
            get { return _audio; }
            set
            {
                _audio = value;
                OnPropertyChanged(nameof(audio));
            }
        }

        public string expression
        {
            get { return _expression; }
            set
            {
                _expression = value;

                OnPropertyChanged(nameof(expression));
            }
        }

        public string Group { get; set; } = "Default";

        public string display
        {
            get { return _display; }

            set
            {
                _display = value;
                OnPropertyChanged(nameof(display));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}