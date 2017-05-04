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
using System.IO;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Reflection;

namespace Alarm
{
    public partial class MainWindow : Window
    {
        private const string DisplayText = "显示";
        private const string HiddenText = "隐藏";
        public Assembly curAssembly { get; } = Assembly.GetExecutingAssembly();
        private bool _autonRun;
        public bool AutoRun
        {
            get
            {
                _autonRun = GetAutoRunState();
                return _autonRun;
            }
            set
            {
                if (_autonRun != value)
                {
                    _autonRun = value;
                    this.auroRun.IsChecked = _autonRun;
                    SetAutoRun(_autonRun);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            StateChanged += MainWindow_StateChanged;
            Initlize();
        }

        private void Initlize()
        {
            SetVisible(Visibility.Hidden);
            AutoRun = GetAutoRunState();
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
            catch
            {
                Environment.Exit(0);
            }
        }

        #region 托盘相关
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

        private void auroRun_Click(object sender, RoutedEventArgs e)
        {
            AutoRun = !AutoRun;
        }
        #endregion

        private void SetVisible(Visibility newVisible)
        {
            Visibility = newVisible;
            visible.Header = Visibility == Visibility.Visible ? HiddenText : DisplayText;
            WindowState = WindowState.Normal;
            ShowInTaskbar = newVisible == Visibility.Visible;
        }

        [RegistryPermission(SecurityAction.Demand)]
        private void SetAutoRun(bool autorun)
        {
            try
            {
                var registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (autorun)
                    registry.SetValue(curAssembly.GetName().Name, curAssembly.Location);
                else
                    registry.DeleteValue(curAssembly.GetName().Name, false);
            }
            catch
            {
            }
        }

        [RegistryPermission(SecurityAction.Demand)]
        bool GetAutoRunState()
        {
            try
            {
                var registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                var str = registry.GetValue(curAssembly.GetName().Name) + "";
                if (string.IsNullOrEmpty(str))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}