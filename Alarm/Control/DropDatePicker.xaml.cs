using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Alarm.Control

{
    /// <summary>
    /// iSagySingleDropDatePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DropDatePicker : UserControl
    {
        private bool isInitData = false;
        public DropDatePicker()
        {
            InitializeComponent();
            this.Loaded += ISagySingleDropDatePicker_Loaded;
        }

        #region PickerTime
        public static readonly DependencyProperty PickerTimeProperty = DependencyProperty.Register(
            "PickerTime", typeof(DateTime), typeof(DropDatePicker), new PropertyMetadata((sender, e) =>
            {
                var control = sender as DropDatePicker;
                if (control == null) return;
                control.UnRegistEvent();
                var time = DateTime.Today;
                if (!DateTime.TryParse(e.NewValue + "", out time)) return;
                if (DateTime.MinValue == time) return;
                control.InitData();
                try
                {
                    var year = time.Year;
                    //待设定年是否在包含的选项年里
                    if (year <= DateTime.Today.Year + 5 && year >= DateTime.Today.Year - 5)
                    {
                        control.cobYear.SelectedIndex = year - (DateTime.Today.Year - 5);
                    }
                    control.cobMonth.SelectedIndex = time.Month - 1;
                    control.RefreshDayFromMonth(time.Month);
                    control.cobDay.SelectedIndex = time.Day - 1;
                    control.cobHour.SelectedIndex = time.Hour;
                    control.cobMinute.SelectedIndex = time.Minute;
                    control.cobWeek.SelectedIndex = time.Day - 1;

                }
                catch
                {

                }
                finally
                {
                    control.UnRegistEvent();
                    control.RegistEvent();
                }
            }));
        public DateTime PickerTime
        {
            get { return (DateTime)GetValue(PickerTimeProperty); }
            set { SetValue(PickerTimeProperty, value); }
        }
        #endregion

        #region DPSelectMode
        public static readonly DependencyProperty DPSelectModeProperty = DependencyProperty.Register(
            "DPSelectMode", typeof(SelectMode), typeof(DropDatePicker), new PropertyMetadata((sender, e) =>
          {
              var control = sender as DropDatePicker;
              if (control == null) return;
              var vis = Visibility.Visible;
              control.cobDay.Visibility = vis;
              control.cobHour.Visibility = vis;
              control.cobMonth.Visibility = vis;
              control.cobYear.Visibility = vis;
              control.cobMinute.Visibility = vis;
              control.cobWeek.Visibility = Visibility.Collapsed;
              control.StackPanelDate.Visibility = vis;
              control.StackPanelHourMinute.Visibility = vis;
              var mode = (SelectMode)Enum.Parse(typeof(SelectMode), e.NewValue + "");
              switch (mode)
              {
                  case SelectMode.OnlyDate:
                      control.StackPanelHourMinute.Visibility = Visibility.Collapsed;
                      break;
                  case SelectMode.OnlyTime:
                      control.StackPanelDate.Visibility = Visibility.Collapsed;
                      break;
                  case SelectMode.OnlyWeek:
                      {
                          control.StackPanelDate.Visibility = Visibility.Collapsed;
                          control.StackPanelHourMinute.Visibility = Visibility.Collapsed;
                          control.cobWeek.Visibility = vis;
                      }
                      break;
                  case SelectMode.OnlyHour:
                      {
                          control.StackPanelDate.Visibility = Visibility.Collapsed;
                          control.cobMinute.Visibility = Visibility.Collapsed;
                      }
                      break;
                  case SelectMode.OnlyDay:
                      {
                          control.StackPanelDate.Visibility = Visibility.Visible;
                          control.StackPanelHourMinute.Visibility = Visibility.Collapsed;
                          control.cobYear.Visibility = Visibility.Collapsed;
                          control.cobMonth.Visibility = Visibility.Collapsed;
                      }
                      break;
                  case SelectMode.OnlyMonthDay:
                      {
                          control.StackPanelDate.Visibility = Visibility.Visible;
                          control.StackPanelHourMinute.Visibility = Visibility.Collapsed;
                          control.cobYear.Visibility = Visibility.Collapsed;
                      }
                      break;
                  case SelectMode.OnlyHourMinute:
                      {
                          control.StackPanelDate.Visibility = Visibility.Collapsed;
                      }
                      break;
              }
          }));
        public SelectMode DPSelectMode
        {
            get { return (SelectMode)GetValue(DPSelectModeProperty); }
            set { SetValue(DPSelectModeProperty, value); }
        }
        #endregion

        private void ISagySingleDropDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            UnRegistEvent();
            RegistEvent();
            InitData();
        }

        private void RegistEvent()
        {
            cobYear.SelectionChanged += CobYear_OnSelectionChanged;
            cobMonth.SelectionChanged += CobMonth_OnSelectionChanged;
            cobDay.SelectionChanged += CobDay_OnSelectionChanged;
            cobHour.SelectionChanged += CobHour_OnSelectionChanged;
            cobMinute.SelectionChanged += CobMinute_OnSelectionChanged;
            cobWeek.SelectionChanged += CobWeek_OnSelectionChanged;
        }

        private void UnRegistEvent()
        {
            cobYear.SelectionChanged -= CobYear_OnSelectionChanged;
            cobMonth.SelectionChanged -= CobMonth_OnSelectionChanged;
            cobDay.SelectionChanged -= CobDay_OnSelectionChanged;
            cobHour.SelectionChanged -= CobHour_OnSelectionChanged;
            cobMinute.SelectionChanged -= CobMinute_OnSelectionChanged;
            cobWeek.SelectionChanged -= CobWeek_OnSelectionChanged;
        }

        private void InitData()
        {
            if (isInitData) return;
            UnRegistEvent();
            cobYear.Items.Clear();
            for (int i = DateTime.Today.Year - 5; i <= DateTime.Today.Year + 5; i++)
            {
                cobYear.Items.Add(i.ToString());
            }
            cobYear.SelectedIndex = 5;
            cobMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cobMonth.Items.Add(i.ToString("00"));
            }
            cobMonth.SelectedIndex = DateTime.Today.Month - 1;
            cobDay.SelectedIndex = DateTime.Today.Day - 1;

            cobHour.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                cobHour.Items.Add(i.ToString("00"));
            }
            cobHour.SelectedIndex = 0;
            cobMinute.Items.Clear();
            for (int i = 0; i < 60; i++)
            {
                cobMinute.Items.Add(i.ToString("00"));
            }
            cobMinute.SelectedIndex = 0;
            cobWeek.Items.Clear();
            string[] weeks = { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };
            for (int i = 0; i < weeks.Length; i++)
            {
                cobWeek.Items.Add(weeks[i]);
            }
            cobWeek.SelectedIndex = 0;
            isInitData = true;
            PickerTime = DateTime.Today;
            RegistEvent();

        }

        private void CobYear_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobYear.SelectedItem == null) return;
            var year = Convert.ToInt32(cobYear.SelectedItem);
            var selectedDay = PickerTime.Day;
            cobDay.Items.Clear();
            var days = DateTime.DaysInMonth(year, PickerTime.Month);
            for (int i = 1; i <= days; i++)
            {
                cobDay.Items.Add(i.ToString("00"));
            }
            if (selectedDay > days)
                cobDay.SelectedIndex = days - 1;
            else cobDay.SelectedIndex = selectedDay - 1;
            RefreshTime(year, PickerTime.Month, cobDay.SelectedIndex + 1);
        }

        private void CobMonth_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobMonth.SelectedItem == null) return;
            var month = Convert.ToInt32(cobMonth.SelectedItem);
            RefreshDayFromMonth(month);
            //var days = DateTime.DaysInMonth(PickerTime.Year, month);
            //var selectedDay = PickerTime.Day;
            //cobDay.Items.Clear();
            //for (int i = 1; i <= days; i++)
            //{
            //    cobDay.Items.Add(i.ToString("00"));
            //}
            //if (selectedDay > days)
            //    cobDay.SelectedIndex = days - 1;
            //else cobDay.SelectedIndex = selectedDay - 1;
            RefreshTime(PickerTime.Year, month, cobDay.SelectedIndex + 1);
        }

        private void RefreshDayFromMonth(int month)
        {
            var days = DateTime.DaysInMonth(PickerTime.Year, month);
            cobDay.Items.Clear();
            var selectedDay = PickerTime.Day;
            for (int i = 1; i <= days; i++)
            {
                cobDay.Items.Add(i.ToString("00"));
            }
            if (selectedDay > days)
                cobDay.SelectedIndex = days - 1;
            else cobDay.SelectedIndex = selectedDay - 1;
        }

        private void CobDay_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobDay.SelectedItem == null) return;
            var day = Convert.ToInt32(cobDay.SelectedItem);
            RefreshTime(PickerTime.Year, PickerTime.Month, day);
        }

        private void RefreshTime(int year, int month, int day)
        {
            this.PickerTime = Convert.ToDateTime($"{year}-{month}-{day}");
        }

        private void CobHour_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobHour.SelectedItem == null) return;
            var hour = Convert.ToInt32(cobHour.SelectedItem);

            this.PickerTime = Convert.ToDateTime($"{PickerTime.Year}-{PickerTime.Month}-{PickerTime.Day} {hour}:{PickerTime.Minute}:00");
        }

        private void CobMinute_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobMinute.SelectedItem == null) return;
            var minute = Convert.ToInt32(cobMinute.SelectedItem);

            this.PickerTime = Convert.ToDateTime($"{PickerTime.Year}-{PickerTime.Month}-{PickerTime.Day} {PickerTime.Hour}:{minute}:00");
        }

        public enum SelectMode
        {
            //年月日
            OnlyDate,
            //小时分钟
            OnlyTime,
            OnlyWeek,
            OnlyHour,
            OnlyDay,
            OnlyMonthDay,
            OnlyHourMinute,
            All
        }

        private void CobWeek_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobWeek.SelectedItem == null) return;
            var week = cobWeek.SelectedIndex + 1;
            this.PickerTime = Convert.ToDateTime($"{PickerTime.Year}-{PickerTime.Month}-{week}");
        }
    }

    public class SingleDropDatePickerConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DropDatePicker.SelectMode mode;
                Enum.TryParse(value + "", out mode);
                return mode;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public class Curve
        {
            public DateTime Time { get; set; }
            public string Value { get; set; }
        }
    }
}
