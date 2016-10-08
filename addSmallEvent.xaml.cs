using DaemonTime.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace DaemonTime
{
    public partial class addSmallEvent:Page
    {
        private string higherElement;
        public addSmallEvent() {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            higherElement = e.Parameter as string;
        }
        private void addSmallItem(object sender, RoutedEventArgs e)
        {
            string Name = higherElement;
            string name = textBox.Text.ToString();
            using (var conn = AppDatabase.GetDbConnection())
            {
                // 需要添加的 Person 对象。
                var eve =new Events() { Name = name, fatherName = Name };

                // 受影响行数。
                var count = conn.Insert(eve);

                //          string msg = $"新增的 Person 对象的 Id 为 {addPerson.Id}，Name 为 {addPerson.Name}";
                //          await new MessageDialog(msg).ShowAsync();
            }
            this.Frame.Navigate(typeof(MainPage));
        }
    }
    
}
