using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Alarm.CommonLib;
using Alarm.Control;
using Alarm.Models;

namespace Alarm.Control
{
    [TemplateVisualState(Name = "ListTask", GroupName = "VisualStateGroup")]
    [TemplateVisualState(Name = "EditTask", GroupName = "VisualStateGroup")]
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();

            this.List.EditEvent += List_EditEvent;
            ;
            this.List.DeleteEvent += List_DeleteEvent;
            this.List.AddEvent += List_AddEvent;
            this.Edit.BtnCancle.Click += BtnCancle_Click;
            this.Edit.BtnSave.Click += BtnSave_Click;

            VisualStateManager.GoToState(this, "ListTask", false);
            Refresh();
        }

        private void List_AddEvent(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel();
            task.expression = CronBuilder.CreateHourlyTrigger(39).ToString();
            this.Edit.Current = task;
            VisualStateManager.GoToState(this, "EditTask", false);
        }

        private void List_DeleteEvent(object sender, RoutedEventArgs e)
        {
            var task = (sender as FrameworkElement)?.DataContext as TaskModel;
            if (task == null)
                return;
            Db.repository?.Delete<TaskModel>(task.id);
            this.Refresh();
        }

        private void List_EditEvent(object sender, RoutedEventArgs e)
        {
            var task = (sender as FrameworkElement)?.DataContext as TaskModel;
            if (task == null)
                return;
            this.Edit.Current = task;
            VisualStateManager.GoToState(this, "EditTask", false);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var task = this.Edit.Current;
            AddOrUpdate(task);
            VisualStateManager.GoToState(this, "ListTask", false);
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "ListTask", false);
        }

        public void Refresh()
        {
            var tasks = Db.repository?.Query<TaskModel>().ToList();
            this.List.listView.ItemsSource = tasks;
            ScheduledManager.Instance.Refresh<ShowAlarmJob>(tasks);
        }

        public void AddOrUpdate(TaskModel task)
        {
            if (task == null) return;
            Db.repository?.Upsert<TaskModel>(task);
            Refresh();
        }
    }
}