using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;

namespace Alarm.Models
{
    public class TaskModel
    {
        [BsonId]
        public string id { get; set; }

        public string state { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string weekdays { get; set; }
        public string audio { get; set; }
        public string volume { get; set; }
        public string fontsize { get; set; }
        public string expression { get; set; }
    }
}