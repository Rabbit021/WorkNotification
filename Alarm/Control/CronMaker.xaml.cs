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

namespace Alarm.Control
{
    public partial class CronMaker : UserControl
    {
        public CronMaker()
        {
            InitializeComponent();

            var Types = new List<string>();
            Types.Add("分钟");
            Types.Add("小时");
            Types.Add("天");
            Types.Add("周");
            Types.Add("月");
            Types.Add("年");

            this.comboBox.ItemsSource = Types;
            this.comboBox.SelectedItem = Types.FirstOrDefault();
            this.comboBox.SelectionChanged += ComboBox_SelectionChanged;
            Refresh(Types.FirstOrDefault());

            var weeks = new List<string>();
            weeks.Add("星期一");
            weeks.Add("星期二");
            weeks.Add("星期三");
            weeks.Add("星期四");
            weeks.Add("星期五");
            weeks.Add("星期六");
            weeks.Add("星期日");
            this.weekDay.ItemsSource = weeks;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var type = this.comboBox.SelectedItem + "";
            Refresh(type);
            if (CurrentCronType != null)
                CurrentCronType.Type = type;
        }

        #region CurrentCronType

        public CronType CurrentCronType
        {
            get { return (CronType)GetValue(CurrentCronTypeProperty); }
            set { SetValue(CurrentCronTypeProperty, value); }
        }

        public static readonly DependencyProperty CurrentCronTypeProperty =
            DependencyProperty.Register("CurrentCronType", typeof(CronType), typeof(CronMaker), new PropertyMetadata((sender, e) =>
            {
                var vm = sender as CronMaker;
                if (vm == null) return;
                var type = e.NewValue as CronType;
                vm.Refresh(type);
                vm.timeregion.DataContext = type;
            }));

        #endregion

        private void Refresh(CronType type)
        {
            Refresh(type?.Type);

            this.weekDay.Text = type?.weeks;
        }

        private void Refresh(string type)
        {
            var cols = Visibility.Collapsed;
            var vis = Visibility.Visible;

            Minute.Visibility = cols;
            Hour.Visibility = cols;
            Daily.Visibility = cols;
            Week.Visibility = cols;
            Month.Visibility = cols;
            Year.Visibility = cols;

            if (string.IsNullOrEmpty(type)) return;

            switch (type)
            {
                case "分钟":
                    Minute.Visibility = vis;
                    break;
                case "小时":
                    Hour.Visibility = vis;
                    break;
                case "天":
                    Daily.Visibility = vis;
                    break;
                case "周":
                    Week.Visibility = vis;
                    break;
                case "月":
                    Month.Visibility = vis;
                    break;
                case "年":
                    Year.Visibility = vis;
                    break;
            }
        }
    }
}