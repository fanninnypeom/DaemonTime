using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace DaemonTime
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RecordPage : Page
    {
        private ObservableCollection<viewModel> Items { get; set; }
        private load_day myDayHelper { get; set; }

        public RecordPage()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<viewModel>();
            myDayHelper = new load_day();
            myDayHelper.init_load_day(Items);
        }

        private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer.ChangeView(null, 30, null);
        }
       private  void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var sv = sender as ScrollViewer;

            if (!e.IsIntermediate)
            {
                if (sv.VerticalOffset == 0.0)
                {

                    myDayHelper.load_last_day(Items);
                    sv.ChangeView(null, 30, null);
                }
               
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Content = "点击";

        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Content = "点击";

        }

    }


}



