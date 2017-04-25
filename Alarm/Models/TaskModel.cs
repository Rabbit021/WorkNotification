using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Alarm.Annotations;
using LiteDB;

namespace Alarm.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        [BsonIgnore] private string _id;
        [BsonIgnore] private string _state;
        [BsonIgnore] private string _title;
        [BsonIgnore] private string _content;
        [BsonIgnore] private string _type;
        [BsonIgnore] private string _time;
        [BsonIgnore] private string _date;
        [BsonIgnore] private string _weekdays;
        [BsonIgnore] private string _audio;
        [BsonIgnore] private string _volume;
        [BsonIgnore] private string _fontsize;
        [BsonIgnore] private string _expression;

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

        public string volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(volume));
            }
        }

        public string fontsize
        {
            get { return _fontsize; }
            set
            {
                _fontsize = value;
                OnPropertyChanged(nameof(fontsize));
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}