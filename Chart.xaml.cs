
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
        public Chart()
        {
            this.InitializeComponent();
            drawWeekStatic();

        }
        private void naviRecord(object sender, RoutedEventArgs e)
        {
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
            DateTime tt = (DateTime)datePicker.Value;
            DateTime ttt = (DateTime)datePicker1.Value;
            long t1 = tt.ToFileTime();
            long t2 = ttt.ToFileTime();
            long t3 = t1 + t2;
        }
        private void drawPie(object sender, RoutedEventArgs e)
        {
        }
        private void drawWeekStatic()
        {
            var p = new ObservableCollection<weekNum> {
                 new weekNum()
                 {
                     week="mon",hours=1.0
                 },
        new weekNum()
        {
            week = "tue",hours = 2.0
        },
        new weekNum()
        {
            week = "wes",hours = 3.0
        },
        new weekNum()
        {
            week = "thu",hours = 4.0
        },
        new weekNum()
        {
            week = "fri",hours = 5.0
        },
        new weekNum()
        {
            week = "sat",hours = 6.0
        },
        new weekNum()
        {
            week = "sun",hours = 7.0
        }
    };
            var p2 = new ObservableCollection<weekNum> {
                 new weekNum()
                 {
                     week="mon",hours=7.0
                 },
        new weekNum()
        {
            week = "tue",hours = 6.0
        },
        new weekNum()
        {
            week = "wes",hours = 5.0
        },
        new weekNum()
        {
            week = "thu",hours = 4.0
        },
        new weekNum()
        {
            week = "fri",hours = 3.0
        },
        new weekNum()
        {
            week = "sat",hours = 2.0
        },
        new weekNum()
        {
            week = "sun",hours = 1.0
        }
    };
            LineSeries series1 = new LineSeries();
            series1.ItemsSource = p;
            series1.XBindingPath = "week";

            series1.YBindingPath = "hours";
            series1.Label = "游戏";

            LineSeries series2 = new LineSeries();
            series2.ItemsSource = p2;
            series2.XBindingPath = "week";

            series2.YBindingPath = "hours";
            series2.Label = "学习";

            weekChart.Series.Add(series1);
            weekChart.Series.Add(series2);

        }
    }
}
