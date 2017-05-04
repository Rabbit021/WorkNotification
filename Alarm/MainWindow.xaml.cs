using System;
using System.Collections.Generic;
using System.Drawing;
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
using Alarm.Control;
using Alarm.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Alarm
{
    public partial class MainWindow : Window
    {
        private const string DisplayText = "显示";
        private const string HiddenText = "隐藏";

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            StateChanged += MainWindow_StateChanged;
            SetVisible(Visibility.Hidden);
        }

        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                SetVisible(Visibility.Hidden);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                var win = this.Visibility == Visibility.Visible ? this : null;
                if (!CloseWindow.ShowConfirm(win))
                {
                    e.Cancel = true;
                    return;
                }
                ScheduledManager.Instance.Close();
            }
            catch (Exception exp)
            {
                Environment.Exit(0);
            }
        }

        // 窗口最小化
        private void visible_Click(object sender, RoutedEventArgs e)
        {
            var header = (sender as MenuItem)?.Header + "";
            switch (header)
            {
                case DisplayText:
                    SetVisible(Visibility.Visible);
                    break;
                case HiddenText:
                    SetVisible(Visibility.Hidden);
                    break;
                default:
                    break;
            }
        }

        // 退出
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetVisible(Visibility newVisible)
        {
            Visibility = newVisible;
            visible.Header = Visibility == Visibility.Visible ? HiddenText : DisplayText;
            WindowState = WindowState.Normal;
            ShowInTaskbar = newVisible == Visibility.Visible;
        }
    }
}