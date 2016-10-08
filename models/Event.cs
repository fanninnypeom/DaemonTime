using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using Windows.UI.Xaml;
namespace DaemonTime
{
    class Event
    {
        [PrimaryKey]
        public string dateTime { get; set; }  //主键， 通过datetime.tofilename()方法得到，换成long型也可

        public string year { get; set; } //年  "2011"

        public string month { get; set; }  //  月 "06"
        public string day { get; set; }  // 日  "31"
        public string time { get; set; } // 时间（小时） "00" - "23"
 
        public string ev1 { get; set; } // 前半小时事件

        public string ev2{ get; set; }  // 后半小时事件


    }
}
