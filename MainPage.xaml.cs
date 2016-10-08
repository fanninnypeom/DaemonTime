using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using DaemonTime.models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace DaemonTime
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            using (var conn = AppDatabase.GetDbConnection())
            {
                var dbEvents = conn.Table<Events>();
                foreach (var Event in dbEvents)
                {

                    TextBlock r = new TextBlock();
                    r.Text = Event.Name;
                    
                    GridView g = new GridView();
                    g.Margin = new Thickness(0, 0, 0, 0);
                    Button b = new Button();
                    Button b1 = new Button();
                    SymbolIcon s = new SymbolIcon();
                    s.Symbol = Symbol.Clock;
                    
                    b.Content = "\ue601";
                    b.Name = "icon";
                    b.Background= new SolidColorBrush(Colors.White);
                    b.FontFamily=new FontFamily("/Resources/iconfont.ttf#iconfont");
                    b.Click += addSmallItem;
                    b.Tag = Event.Name;

                    b1.Content = "\ue600";
                    b1.Name = "icon";
                    b1.Background = new SolidColorBrush(Colors.White);
                    b1.FontFamily = new FontFamily("/Resources/iconfont.ttf#iconfont");
                    b1.Click += changeItem;
                    b1.Tag = Event.Name;

                    g.Items.Add(s);
                    g.Items.Add(r);
                    g.Items.Add(b);
                    g.Items.Add(b1);
                    
                    if (Event.fatherName == "")
                        Content.Children.Add(g);

                    foreach (var sEvent in dbEvents)
                    {
                        if (sEvent.fatherName == Event.Name)
                        {
                            GridView gg = new GridView();
                            Button b3 = new Button();
                            b3.Content = "\ue600";
                            b3.Name = "icon";
                            b3.Background = new SolidColorBrush(Colors.White);
                            b3.FontFamily = new FontFamily("/Resources/iconfont2.ttf#iconfont");
                            b3.Click +=deleteSmallItem;
                            b3.Tag = sEvent.Name;
                            TextBlock tt1 = new TextBlock();
                            tt1.Text = "  ";
                            TextBlock tt = new TextBlock();
                            tt.Text = sEvent.Name;
                            tt.FontSize = 14;
                            tt.Margin = new Thickness(0, 0, 0, 0);
                            gg.Items.Add(tt1);
                            gg.Items.Add(tt);
                            gg.Items.Add(b3);
                            Content.Children.Add(gg);
                        }
                    }

                }

            }
        }
        private void addItem(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(addEvent));
        }
        private void addSmallItem(object sender, RoutedEventArgs e)
        {
            string t = (string)((Button)sender).Tag;
            this.Frame.Navigate(typeof(addSmallEvent), t
                );
        }
        private void Clean(object sender, RoutedEventArgs e)
        {
            AppDatabase.deleteEvents();
            this.Frame.Navigate(typeof(MainPage));//刷新
        }
        private async void changeItem(object sender, RoutedEventArgs e)
        {
            originName.Text= (string)((Button)sender).Tag;
            await popChange.ShowAsync();
        }
        private void changeEvent(object sender, RoutedEventArgs e)
        {
            string origin = originName.Text;
            string past = (string)changeName.Text;
            AppDatabase.update(origin,past);
            popChange.Hide();
            this.Frame.Navigate(typeof(MainPage));//刷新
        }
        private void deleteSmallItem(object sender, RoutedEventArgs e)
        {
            string del = (string)((Button)sender).Tag;
            AppDatabase.deleteItem(del);
            this.Frame.Navigate(typeof(MainPage));
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
        private void naviSync(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SyncData));
        }

    }
}