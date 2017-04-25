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

namespace Alarm
{
    /// <summary>
    /// ListTaskControl.xaml 的交互逻辑
    /// </summary>
    public partial class ListTaskControl : UserControl
    {
        public ListTaskControl()
        {
            InitializeComponent();
            this.BtnAdd.Click += BtnAdd_Click;
        }


        public event RoutedEventHandler EditEvent = (sender, e) => { };
        public event RoutedEventHandler DeleteEvent = (sender, e) => { };
        public event RoutedEventHandler AddEvent = (sender, e) => { };

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEvent?.Invoke(sender, e);
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            EditEvent.Invoke(sender, e);
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            DeleteEvent.Invoke(sender, e);
        }
    }
}