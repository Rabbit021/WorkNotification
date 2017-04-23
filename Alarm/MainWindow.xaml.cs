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
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var val = new TaskModel()
            {
                id = Guid.NewGuid().ToString(),
            };
            TaskContext.Instance.Add(val);
        }
    }
}
