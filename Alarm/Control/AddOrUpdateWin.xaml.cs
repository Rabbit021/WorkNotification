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
using System.Windows.Shapes;
using Alarm.Models;

namespace Alarm.Control
{
    /// <summary>
    /// AddOrUpdateWin.xaml 的交互逻辑
    /// </summary>
    public partial class AddOrUpdateWin : Window
    {
        static AddOrUpdateWin current;
        public static AddOrUpdateWin Current
        {
            get { return current; }
            set
            {
                if (AddOrUpdateWin.current != null)
                    AddOrUpdateWin.current.Close();
                current = value;
            }
        }
        public AddOrUpdateWin()
        {
            InitializeComponent();
            var win = Application.Current.MainWindow;
            if (win != null)
                this.Owner = win;
            this.BtnSave.Click += BtnSave_Click;
            this.BtnCancle.Click += BtnCancle_Click;
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            Current?.Close();
        }

        public TaskModel CurrentTask { get; set; }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // TODO 
            this.DialogResult = true;
        }

        public static bool? Refresh(TaskModel task)
        {
            AddOrUpdateWin.Current = new AddOrUpdateWin();
            Current.CurrentTask = task;
            return Current.ShowDialog();
        }
    }
}
