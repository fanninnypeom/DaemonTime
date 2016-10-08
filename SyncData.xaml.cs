using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using DaemonTime.models;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;

namespace DaemonTime
{
    public sealed partial class SyncData : Page
    {
        public SyncData()
        {
            this.InitializeComponent();
        }
        public async void Register(object sender, RoutedEventArgs e)
        {
            await popRegister.ShowAsync();
            return ;
        }
        public void Register_(object sender, RoutedEventArgs e)
        {
            remoteDB.Registers(name.Text, id.Text,passwordBox1.Password);
            popRegister.Hide();
            return;
        }
        public async void Up(object sender, RoutedEventArgs e)
        {

            int re=remoteDB.Push(textBox.Text, passwordBox.Password);
            if (re == 1)
            {
                var dialog = new MessageDialog("账号或密码错误", "消息提示");

                dialog.Commands.Add(new UICommand("确定", cmd => { }, commandId: 0));
                dialog.Commands.Add(new UICommand("取消", cmd => { }, commandId: 1));

                //设置默认按钮，不设置的话默认的确认按钮是第一个按钮
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                //获取返回值
                var result = await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("上传数据成功！", "消息提示");

                dialog.Commands.Add(new UICommand("确定", cmd => { }, commandId: 0));
                dialog.Commands.Add(new UICommand("取消", cmd => { }, commandId: 1));

                //设置默认按钮，不设置的话默认的确认按钮是第一个按钮
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                //获取返回值
                var result = await dialog.ShowAsync();
            }
            return;
        }
        public async void Down(object sender, RoutedEventArgs e)
        {

            int re=remoteDB.Pull(textBox.Text, passwordBox.Password);
            if(re==1)
            {
                var dialog = new MessageDialog("账号或密码错误!", "消息提示");

                dialog.Commands.Add(new UICommand("确定", cmd => { }, commandId: 0));
                dialog.Commands.Add(new UICommand("取消", cmd => { }, commandId: 1));

                //设置默认按钮，不设置的话默认的确认按钮是第一个按钮
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                //获取返回值
                var result = await dialog.ShowAsync();
            }
            else
            {
                var dialog = new MessageDialog("同步数据成功!", "消息提示");

                dialog.Commands.Add(new UICommand("确定", cmd => { }, commandId: 0));
                dialog.Commands.Add(new UICommand("取消", cmd => { }, commandId: 1));

                //设置默认按钮，不设置的话默认的确认按钮是第一个按钮
                dialog.DefaultCommandIndex = 0;
                dialog.CancelCommandIndex = 1;

                //获取返回值
                var result = await dialog.ShowAsync();
            }
            return;
        }
        public void Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
            return;
        }
    }
}
