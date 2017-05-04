using System.Configuration;
using System.Windows;

namespace Alarm.Windows
{
    /// <summary>
    /// CloseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CloseWindow : Window
    {
        private string passwd = string.Empty;
        public CloseWindow()
        {
            InitializeComponent();
            BtnCancle.Click += BtnCancle_Click;
            BtnExit.Click += BtnExit_Click;
            passwd = ConfigurationManager.AppSettings[nameof(passwd)] + "";
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            if (string.Equals(passwd, this.pwd.Password))
            {
                DialogResult = true;
            }
            else
            {
                msg.Text = "退出密码错误！";
            }
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public static bool ShowConfirm(Window owner = null)
        {
            var win = new CloseWindow();
            if (owner == null)
            {
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                win.Owner = owner;
                win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            var dialog = win.ShowDialog();
            return dialog != null && (bool)dialog;
        }
    }
}