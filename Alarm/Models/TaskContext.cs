using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using Control;

namespace Alarm.Models
{
    public class TaskContext
    {
        public static TaskContext Instance = new TaskContext();

        private TaskContext()
        {
        }

        public HashSet<TaskModel> TaskModels { get; set; } = new HashSet<TaskModel>();

        public void ReloadTask()
        {

        }

        public void Edit(string id, TaskModel model)
        {
            Add(model);
        }

        public bool Del(string id)
        {
            return true;
        }

        public TaskModel Get(string id)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("@id", id);
            var tb = DbAccess.GetSqlResult("getrow", dict).Select();
            return TaskModel.Create(tb?.FirstOrDefault());
        }

        public bool Add(TaskModel model)
        {
            var rst = Get(model.id);
            if (rst == null)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("@id", model.id);
                dict.Add("@state", model.state);
                dict.Add("@title", model.title);
                dict.Add("@content", model.content);
                dict.Add("@type", model.type);
                dict.Add("@time", model.time);
                dict.Add("@date", model.date);
                dict.Add("@weekdays", model.weekdays);
                dict.Add("@audio", model.audio);
                dict.Add("@volume", model.volume);
                dict.Add("@fontsize", model.fontsize);
                var tb = DbAccess.InsertOrUpdate("addrow", dict);
                return true;
            }
            return false;
        }
    }

    public class TaskModel
    {
        public static TaskModel Create(DataRow row)
        {
            if (row == null)
                return null;
            var rst = new TaskModel();

            rst.id = row["id"] + "";
            rst.state = row["state"] + "";
            rst.title = row["title"] + "";
            rst.content = row["content"] + "";
            rst.type = row["type"] + "";
            rst.time = row["time"] + "";
            rst.date = row["date"] + "";
            rst.weekdays = row["weekdays"] + "";
            rst.audio = row["audio"] + "";
            rst.volume = row["volume"] + "";
            rst.fontsize = row["fontsize"] + "";
            return rst;
        }
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
    }
}