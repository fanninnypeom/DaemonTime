using DaemonTime.models;
using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DaemonTime
{
    public sealed partial class Chart : Page
    {
        public ObservableCollection<dayNum> WeekNum { get; set; }
        public ObservableCollection<weekNum> MonthNum { get; set; }
        public ObservableCollection<nameNum> Test { get; set; }
        public Chart()
        {
            this.InitializeComponent();
            this.Test = new ObservableCollection<nameNum> { };
            drawWeekStatic();
            drawMonthStatic();
            drawBarStatic();
            drawPieStatic();
        }
        private void naviRecord(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecordPage));
        }
        private void naviChart(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Chart));
        }
        private void naviMain(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        private void drawBar(object sender, RoutedEventArgs e)
        {
            ColumnChart.Series.Clear();
            DateTime start = (DateTime)datePicker.Value;
            DateTime[] list = new DateTime[7];
            list[0] = start;
            for (int i = 1; i < 7; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(7).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                        {
                            eventnamel.Add(Event.ev1);
                        }
                        if (!eventnamel.Contains(Event.ev2))
                        {
                            eventnamel.Add(Event.ev2);
                        }
                    }
                }
                foreach (String eventName in eventnamel)
                {
                    flag = false;
                    ColumnSeries series = new ColumnSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    double timesum = 0;
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time+=0.5;
                                timesum+=0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                                timesum += 0.5;
                            }
                        }
                        p.Add(new weekNum
                        {
                            week = datetime.ToString("dd"),
                            hours = time
                        });
                    }
                    p.Add(new weekNum
                    {
                        week = "All",
                        hours = timesum
                    });
                    series.ItemsSource = p;
                    ColumnChart.Series.Add(series);
                }
                if (flag)
                {
                    ColumnSeries series = new ColumnSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new weekNum
                        {
                            week = datetime.ToString("dd"),
                            hours = 0
                        });
                    }
                    p.Add(new weekNum
                    {
                        week = "All",
                        hours = 0
                    });
                    series.ItemsSource = p;
                    ColumnChart.Series.Add(series);
                }
            }
        }
        private void drawPie(object sender, RoutedEventArgs e)
        {
            Test.Clear();
            DateTime start = (DateTime)datePickerPie1.Value;
            DateTime end = (DateTime)datePickerPie2.Value;
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                var dbEvents = conn.Table<Event>();
                List<Event> eventlist = new List<Event>();
                List<string> eventnamel = new List<string>();
                foreach (var event1 in dbEvents)
                {
                    if (event1.dateTime >= start.ToFileTime() && event1.dateTime < end.AddDays(1).ToFileTime())
                    {
                        eventlist.Add(event1);
                        if (!eventnamel.Contains(event1.ev1))
                        {
                            eventnamel.Add(event1.ev1);
                        }
                        if (!eventnamel.Contains(event1.ev2))
                        {
                            eventnamel.Add(event1.ev2);
                        }
                    }
                }
                Boolean flag = true;
                foreach (string eventname in eventnamel)
                {
                    flag = false;
                    double time = 0;
                    foreach (var event1 in eventlist)
                    {
                        if (event1.ev1 == eventname)
                        {
                            time += 0.5;
                        }
                        if (event1.ev2 == eventname)
                        {
                            time += 0.5;
                        }
                    }
                    Test.Add(new nameNum
                    {
                        name = eventname,
                        hours = time
                    });
                }
                if (flag)
                {
                    Test.Add(new nameNum
                    {
                        name = "empty",
                        hours = 1
                    });
                }
                this.DataContext = this;
            }
        }
        private void drawPieStatic()
        {
            datePickerPie1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            datePickerPie2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime start = (DateTime)datePickerPie1.Value;
            DateTime end = (DateTime)datePickerPie2.Value;
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                var dbEvents = conn.Table<Event>();
                List<Event> eventlist = new List<Event>();
                List<string> eventnamel = new List<string>();
                foreach (var event1 in dbEvents)
                {
                    if (event1.dateTime >= start.ToFileTime() && event1.dateTime < end.AddDays(1).ToFileTime())
                    {
                        eventlist.Add(event1);
                        if (!eventnamel.Contains(event1.ev1))
                        {
                            eventnamel.Add(event1.ev1);
                        }
                        if (!eventnamel.Contains(event1.ev2))
                        {
                            eventnamel.Add(event1.ev2);
                        }
                    }
                }
                Boolean flag = true;
                foreach (string eventname in eventnamel)
                {
                    flag = false;
                    double time = 0;
                    foreach (var event1 in eventlist)
                    {
                        if (event1.ev1 == eventname)
                        {
                            time += 0.5;
                        }
                        if (event1.ev2 == eventname)
                        {
                            time += 0.5;
                        }
                    }
                    Test.Add(new nameNum
                    {
                        name = eventname,
                        hours = time
                    });
                }
                if (flag)
                {
                    Test.Add(new nameNum
                    {
                        name = "empty",
                        hours = 1
                    });
                }
                this.DataContext = this;
            }
        }
        private void drawBarStatic()
        {
            datePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-7);
            DateTime start = (DateTime)datePicker.Value;
            DateTime[] list = new DateTime[7];
            list[0] = start;
            for (int i = 1; i < 7; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(7).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                        {
                            eventnamel.Add(Event.ev1);
                        }
                        if (!eventnamel.Contains(Event.ev2))
                        {
                            eventnamel.Add(Event.ev2);
                        }
                    }
                }
                foreach (String eventName in eventnamel)
                {
                    flag = false;
                    ColumnSeries series = new ColumnSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    double timesum = 0;
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                                timesum += 0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                                timesum += 0.5;
                            }
                        }
                        p.Add(new weekNum
                        {
                            week = datetime.ToString("dd"),
                            hours = time
                        });
                    }
                    p.Add(new weekNum
                    {
                        week = "All",
                        hours = timesum
                    });
                    series.ItemsSource = p;
                    ColumnChart.Series.Add(series);
                }
                if (flag)
                {
                    ColumnSeries series = new ColumnSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new weekNum
                        {
                            week = datetime.ToString("dd"),
                            hours = 0
                        });
                    }
                    p.Add(new weekNum
                    {
                        week = "All",
                        hours = 0
                    });
                    series.ItemsSource = p;
                    ColumnChart.Series.Add(series);
                }
            }
        }
        private void drawWeekStatic()
        {
            datePickerw.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-7);
            DateTime start = (DateTime)datePickerw.Value;
            DateTime[] list = new DateTime[7];
            list[0] = start;
            for (int i = 1; i < 7; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(7).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                        {
                            eventnamel.Add(Event.ev1);
                        }
                        if (!eventnamel.Contains(Event.ev2))
                        {
                            eventnamel.Add(Event.ev2);
                        }
                    }
                }
                foreach (String eventName in eventnamel)
                {
                    flag = false;
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                        }
                        p.Add(new weekNum
                        {
                            week = datetime.DayOfWeek.ToString(),
                            hours = time
                        });
                    }
                    series.ItemsSource = p;
                    weekChart.Series.Add(series);
                }
                if (flag)
                {
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new weekNum
                        {
                            week = datetime.DayOfWeek.ToString(),
                            hours = 0
                        });
                    }
                    series.ItemsSource = p;
                    weekChart.Series.Add(series);
                }
            }
        }
        private void drawMonthStatic()
        {
            datePickerM.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-30);
            DateTime start = (DateTime)datePickerM.Value;
            DateTime[] list = new DateTime[30];
            list[0] = start;
            for (int i = 1; i < 30; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(30).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                        {
                            eventnamel.Add(Event.ev1);
                        }
                        if (!eventnamel.Contains(Event.ev2))
                        {
                            eventnamel.Add(Event.ev2);
                        }
                    }
                }
                foreach (string eventName in eventnamel)
                {
                    flag = false;
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "day";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    var p = new ObservableCollection<dayNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                        }
                        p.Add(new dayNum
                        {
                            day = datetime.ToString("dd"),
                            hours = time
                        });
                    }
                    series.ItemsSource = p;
                    monthChart.Series.Add(series);
                }
                if (flag)
                {
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "day";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<dayNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new dayNum
                        {
                            day = datetime.ToString("dd"),
                            hours = 0
                        });
                    }
                    series.ItemsSource = p;
                    monthChart.Series.Add(series);
                }
            }
        }

        private void drawWeek(object sender, RoutedEventArgs e)
        {
            weekChart.Series.Clear();
            DateTime start = (DateTime)datePickerw.Value;
            DateTime[] list = new DateTime[7];
            list[0] = start;
            for (int i = 1; i < 7; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(7).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                            eventnamel.Add(Event.ev1);
                        if (!eventnamel.Contains(Event.ev2))
                            eventnamel.Add(Event.ev2);
                    }
                }
                foreach (string eventName in eventnamel)
                {
                    flag = false;
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                        }
                        p.Add(new weekNum
                        {
                            week = datetime.DayOfWeek.ToString(),
                            hours = time
                        });
                    }
                    series.ItemsSource = p;
                    weekChart.Series.Add(series);
                }
                if (flag)
                {
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "week";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<weekNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new weekNum
                        {
                            week = datetime.DayOfWeek.ToString(),
                            hours = 0
                        });
                    }
                    series.ItemsSource = p;
                    weekChart.Series.Add(series);
                }
            }
        }

        private void drawMonth(object sender, RoutedEventArgs e)
        {
            monthChart.Series.Clear();
            DateTime start = (DateTime)datePickerM.Value;
            DateTime[] list = new DateTime[30];
            list[0] = start;
            for (int i = 1; i < 30; i++)
            {
                list[i] = start.AddDays(i);
            }
            using (var conn = new SQLiteDBHelper().Dbconnection())
            {
                Boolean flag = true;
                var dbEvents = conn.Table<Event>();
                List<string> eventnamel = new List<string>();
                List<Event> eventlist = new List<Event>();
                foreach (var Event in dbEvents)
                {
                    if (Event.dateTime < start.AddDays(30).ToFileTime() && Event.dateTime >= start.ToFileTime())
                    {
                        eventlist.Add(Event);
                        if (!eventnamel.Contains(Event.ev1))
                        {
                            eventnamel.Add(Event.ev1);
                        }
                        if (!eventnamel.Contains(Event.ev2))
                        {
                            eventnamel.Add(Event.ev2);
                        }
                    }
                }
                foreach (string eventName in eventnamel)
                {
                    flag = false;
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "day";
                    series.YBindingPath = "hours";
                    series.Label = eventName;
                    var p = new ObservableCollection<dayNum> { };
                    foreach (var datetime in list)
                    {
                        double time = 0;
                        foreach (var Event in eventlist)
                        {
                            if (Event.ev1 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                            if (Event.ev2 == eventName && Event.dateTime >= datetime.ToFileTime() && Event.dateTime < datetime.AddDays(1).ToFileTime())
                            {
                                time += 0.5;
                            }
                        }
                        p.Add(new dayNum
                        {
                            day = datetime.ToString("dd"),
                            hours = time
                        });
                    }
                    series.ItemsSource = p;
                    monthChart.Series.Add(series);
                }
                if (flag)
                {
                    LineSeries series = new LineSeries();
                    series.XBindingPath = "day";
                    series.YBindingPath = "hours";
                    var p = new ObservableCollection<dayNum> { };
                    foreach (var datetime in list)
                    {
                        p.Add(new dayNum
                        {
                            day = datetime.ToString("dd"),
                            hours = 0
                        });
                    }
                    series.ItemsSource = p;
                    monthChart.Series.Add(series);
                }
            }
        }
    }
}
