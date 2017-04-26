using System;
using System.Collections.Generic;
using System.Text;

namespace Alarm.CommonLib
{
    public class Expression
    {
        public string Second { get; set; } = "0";
        public string Minutes { get; set; } = "0";
        public string Hours { get; set; } = "0";
        public string Days { get; set; } = "1";
        public string Months { get; set; } = "0";
        public string DaysOfWeek { get; set; } = "?";
        public string Years { get; set; } = "?";

        public override string ToString()
        {
            return $"{Minutes} {Hours} {Days} {Months} {DaysOfWeek}";
        }
    }

    public static class CronBuilder
    {
        #region Minutely Triggers

        public static Expression CreateMinutelyTrigger()
        {
            var cronExpression = new Expression
            {
                Minutes = "*",
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        #endregion

        #region Hourly Triggers

        public static Expression CreateHourlyTrigger()
        {
            return CreateHourlyTrigger(0);
        }

        public static Expression CreateHourlyTrigger(int triggerMinute)
        {
            var cronExpression = new Expression
            {
                Minutes = triggerMinute.ToString(),
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateHourlyTrigger(int[] triggerMinutes)
        {
            Expression cronExpression = new Expression
            {
                Minutes = triggerMinutes.ConvertArrayToString(),
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateHourlyTrigger(int firstMinuteToTrigger, int lastMinuteToTrigger)
        {
            return CreateHourlyTrigger(firstMinuteToTrigger, lastMinuteToTrigger, 1);
        }

        public static Expression CreateHourlyTrigger(int firstMinuteToTrigger, int lastMinuteToTrigger, int interval)
        {
            string value = firstMinuteToTrigger + "-" + lastMinuteToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            Expression cronExpression = new Expression
            {
                Minutes = value,
                Hours = "*",
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        #endregion

        #region Daily Triggers

        public static Expression CreateDailyTrigger()
        {
            return CreateDailyTrigger(0);
        }

        public static Expression CreateDailyTrigger(int triggerHour)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = triggerHour.ToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateDailyTrigger(int[] triggerHours)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = triggerHours.ConvertArrayToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, 1);
        }

        public static Expression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            string value = firstHourToTrigger + "-" + lastHourToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = value,
                Days = "*",
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateDailyTrigger(DayOfWeek[] daysOfWeekFilter)
        {
            return CreateDailyTrigger(0, daysOfWeekFilter);
        }

        public static Expression CreateDailyTrigger(int triggerHour, DayOfWeek[] daysOfWeekFilter)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = triggerHour.ToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return cronExpression;
        }

        public static Expression CreateDailyTrigger(int[] triggerHours, DayOfWeek[] daysOfWeekFilter)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = triggerHours.ConvertArrayToString(),
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return cronExpression;
        }

        public static Expression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, DayOfWeek[] daysOfWeekFilter)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, 1, daysOfWeekFilter);
        }

        public static Expression CreateDailyTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval, DayOfWeek[] daysOfWeekFilter)
        {
            string value = firstHourToTrigger + "-" + lastHourToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = value,
                Days = "*",
                Months = "*",
                DaysOfWeek = daysOfWeekFilter.ConvertArrayToString()
            };
            return cronExpression;
        }

        public static Expression CreateDailyOnlyWeekDayTrigger()
        {
            return CreateDailyOnlyWeekDayTrigger(0);
        }

        public static Expression CreateDailyOnlyWeekDayTrigger(int triggerHour)
        {
            return CreateDailyTrigger(triggerHour, GetWeekDays());
        }

        public static Expression CreateDailyOnlyWeekDayTrigger(int[] triggerHours)
        {
            return CreateDailyTrigger(triggerHours, GetWeekDays());
        }

        public static Expression CreateDailyOnlyWeekDayTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, GetWeekDays());
        }

        public static Expression CreateDailyOnlyWeekDayTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, interval, GetWeekDays());
        }

        public static Expression CreateDailyOnlyWeekEndTrigger()
        {
            return CreateDailyOnlyWeekEndTrigger(0);
        }

        public static Expression CreateDailyOnlyWeekEndTrigger(int triggerHour)
        {
            return CreateDailyTrigger(triggerHour, GetWeekEndDays());
        }

        public static Expression CreateDailyOnlyWeekEndTrigger(int[] triggerHours)
        {
            return CreateDailyTrigger(triggerHours, GetWeekEndDays());
        }

        public static Expression CreateDailyOnlyWeekEndTrigger(int firstHourToTrigger, int lastHourToTrigger)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, GetWeekEndDays());
        }

        public static Expression CreateDailyOnlyWeekEndTrigger(int firstHourToTrigger, int lastHourToTrigger, int interval)
        {
            return CreateDailyTrigger(firstHourToTrigger, lastHourToTrigger, interval, GetWeekEndDays());
        }

        #endregion

        #region Monthly Triggers

        public static Expression CreateMonthlyTrigger()
        {
            return CreateMonthlyTrigger(0);
        }

        public static Expression CreateMonthlyTrigger(int triggerDay)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = triggerDay.ToString(),
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateMonthlyTrigger(int[] triggerDays)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = triggerDays.ConvertArrayToString(),
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateMonthlyTrigger(int firstDayToTrigger, int lastDayToTrigger)
        {
            return CreateMonthlyTrigger(firstDayToTrigger, lastDayToTrigger, 1);
        }

        public static Expression CreateMonthlyTrigger(int firstDayToTrigger, int lastDayToTrigger, int interval)
        {
            string value = firstDayToTrigger + "-" + lastDayToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = value,
                Months = "*",
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        #endregion

        #region Yearly Triggers

        public static Expression CreateYearlyTrigger()
        {
            return CreateYearlyTrigger(0);
        }

        public static Expression CreateYearlyTrigger(int triggerMonth)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = triggerMonth.ToString(),
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateYearlyTrigger(int[] triggerMonths)
        {
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = triggerMonths.ConvertArrayToString(),
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        public static Expression CreateYearlyTrigger(int firstMonthToTrigger, int lastMonthToTrigger)
        {
            return CreateYearlyTrigger(firstMonthToTrigger, lastMonthToTrigger, 1);
        }

        public static Expression CreateYearlyTrigger(int firstMonthToTrigger, int lastMonthToTrigger, int interval)
        {
            string value = firstMonthToTrigger + "-" + lastMonthToTrigger;
            if (interval != 1)
            {
                value += "/" + interval;
            }
            Expression cronExpression = new Expression
            {
                Minutes = "0",
                Hours = "0",
                Days = "0",
                Months = value,
                DaysOfWeek = "*"
            };
            return cronExpression;
        }

        #endregion

        private static string ConvertArrayToString(this IEnumerable<int> list)
        {
            StringBuilder result = new StringBuilder();
            List<int> values = new List<int>(list);
            values.Sort();
            for (int i = 0; i < values.Count; i++)
            {
                result.Append(values[i].ToString());
                if (i != values.Count - 1)
                {
                    result.Append(",");
                }
            }
            return result.ToString();
        }

        private static string ConvertArrayToString(this DayOfWeek[] list)
        {
            StringBuilder result = new StringBuilder();
            List<int> values = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                values.Add((int)list[i]);
            }
            values.Sort();
            for (int i = 0; i < values.Count; i++)
            {
                result.Append(values[i].ToString());
                if (i != values.Count - 1)
                {
                    result.Append(",");
                }
            }
            return result.ToString();
        }

        private static DayOfWeek[] GetWeekDays()
        {
            return new[]
            {
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday
                };
        }

        private static DayOfWeek[] GetWeekEndDays()
        {
            return new[]
            {
                    DayOfWeek.Sunday,
                    DayOfWeek.Saturday
                };
        }
    }
}