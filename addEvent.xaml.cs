using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DaemonTime.models;
using DaemonTime;

namespace DaemonTime
{
    public sealed partial class addEvent : Page
    {
        public addEvent()
        {
            this.InitializeComponent();
        }
        private void addItem(object sender, RoutedEventArgs e)
        {
            string name = textBox.Text;
            using (var conn = AppDatabase.GetDbConnection())
            {
                // 需要添加的 Person 对象。
                var addPerson = new Events() { Name = name,  fatherName = "" };

                // 受影响行数。
                var count = conn.Insert(addPerson);

                //          string msg = $"新增的 Person 对象的 Id 为 {addPerson.Id}，Name 为 {addPerson.Name}";
                //          await new MessageDialog(msg).ShowAsync();
            }
            this.Frame.Navigate(typeof(MainPage));
        }

    }

}