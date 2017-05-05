using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Alarm.Windows
{
    /// <summary>
    /// ShowInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowInfoWindow : Window
    {
        private static ShowInfoWindow _current;

        public static ShowInfoWindow Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new ShowInfoWindow();
                    _current.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
                return _current;
            }
            set { _current = value; }
        }

        private static readonly List<TaskModel> AlarmInfos = new List<TaskModel>();

        public ShowInfoWindow()
        {
            InitializeComponent();
            this.btnClose.Click += BtnClose_Click;
            this.Loaded += ShowInfoWindow_Loaded;
            this.Closing += ShowInfoWindow_Closing;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置全屏
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            this.Topmost = bool.Parse(ConfigurationManager.AppSettings["Topmost"]);
            this.Activate();
            Left = 0.0;
            Top = 0.0;
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
        }

        private void ShowInfoWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 关闭提示,清空
            Current = null;
            AlarmInfos.Clear();
        }

        // 显示提醒
        public static bool ShowAlarm(TaskModel task)
        {
            AlarmInfos.Insert(0, task);
            Current.lsbInfo.ItemsSource = AlarmInfos;
            var dialog = Current.ShowDialog();
            return dialog != null && (bool)dialog;
        }
    }
}