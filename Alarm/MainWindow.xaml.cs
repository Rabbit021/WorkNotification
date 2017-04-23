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
using Alarm.Models;
using Alarm.CommonLib;
using System.Linq;

namespace Alarm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.BtnAdd.Click += BtnAdd_Click;
            this.Loaded += MainWindow_Loaded;

            Refresh();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var task = new TaskModel()
            {
                id = Guid.NewGuid().ToString(),
                date = DateTime.Now.ToShortDateString()
            };
            Db.Instance.repository.Insert<TaskModel>(task);

            Refresh();

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // TODO Edit
            Refresh();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var task = (sender as FrameworkElement).DataContext as TaskModel;
            if (task == null)
                return;
            Db.Instance.repository.Delete<TaskModel>(task.id);
            this.Refresh();
        }

        public void Refresh()
        {
            var tasks = Db.Instance.repository.Query<TaskModel>().ToList();
            this.listView.ItemsSource = tasks;

            // TODO Refresh Task 

        }
    }
}
