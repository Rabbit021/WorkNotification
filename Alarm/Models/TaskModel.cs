using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Alarm.Annotations;
using Alarm.CommonLib;
using Alarm.Control;
using LiteDB;

namespace Alarm.Models
{
    public class TaskModel : INotifyPropertyChanged, IJobEntity, ITriggerEntity
    {
        private string _id;
        private string _title;
        private string _content;
        private CronType _type;

        public TaskModel()
        {
            id = Guid.NewGuid().ToString();
            type = new CronType();
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

        public CronType type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(type));
            }
        }

        public string Group { get; set; } = "Default";

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string expression
        {
            get { return type.Expression; }
            set { type.Expression = value; }
        }
        public string display
        {
            get { return type.Display; }
            set { type.Display = value; }
        }
    }
}