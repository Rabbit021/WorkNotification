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
    /// EditTaskControl.xaml 的交互逻辑
    /// </summary>
    public partial class EditTaskControl : UserControl
    {
        public EditTaskControl()
        {
            InitializeComponent();
        }

        #region Current
        public TaskModel Current
        {
            get { return (TaskModel)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(TaskModel), typeof(EditTaskControl), new PropertyMetadata((sender, e) =>
            {
                var vm = sender as EditTaskControl;
                if (vm == null) return;

                var val = e.NewValue as TaskModel;
                if (val == null) return;
                vm.Edit.DataContext = val;
            }));
        #endregion
    }
}
