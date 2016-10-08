using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
namespace DaemonTime
{
     class load_day
    {
        SQLiteDBHelper mySQL { set; get; }
        public DateTime lastdate { set; get; }
        public string year { set; get; }

        public string month { set; get; }

        public string day { set; get; }

        public 

        load_day() {
            year = DateTime.Now.ToString("yyyy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            lastdate = DateTime.Now;
        }

        //添加一天
        public  void init_load_day(ObservableCollection<viewModel> Items) {
            SQLiteDBHelper mySQL = new SQLiteDBHelper();
            if (true)
            {
                mySQL.CreateDatabase();
                ////////////test
                Event evt = new Event();
                evt.dateTime = year + month + day + 22;
                evt.year = year;
                evt.month = month;
                evt.day = day;
                evt.ev1 = "啪啪啪";
                evt.ev2 = "吃饭";
                evt.time = "15";
                mySQL.Add(evt);

            }

            List<Event> eList = new List<Event>(); ;
            try
            {
                eList = mySQL.Querybyday(year, month, day);
            }
            catch (Exception e)
            {
                if (eList == null)
                {
                    eList = new List<Event>();
                }
            }

            for (int i = 23; i >= 0; i--) {
                viewModel temp = new viewModel();
                temp.time = i + ":00";
                temp.month_day = "";
                if (i == 0)
                {
                    temp.month_day = month + "月";
                }
                if (i == 1)
                {

                    temp.month_day = day + "日";
                }
                bool tag = false;
                foreach(var p in eList) {
                    if (p.time == i + "")
                    {
                        tag = true;
                        temp.ev1 = p.ev1;
                        temp.ev2 = p.ev2;
                    }

                }

                if (tag == false) {
                    temp.ev1 = "";
                    temp.ev2 = "";

                }
                temp.btAllDate = year + "-" + month + "-" + day;

                Items.Insert(0, temp);

            }

            this.lastdate.AddDays(-1);

        }

        public void load_last_day(ObservableCollection<viewModel> Items) {
            string lastYear = lastdate.ToString("yyyy");
            string lastMonth = lastdate.ToString("MM");
            string lastDay = lastdate.ToString("dd");
            List<Event> eList = new List<Event>(); ;
            try
            {
                 eList = mySQL.Querybyday(lastYear, lastMonth, lastDay);
            }
            catch (Exception e) {
                if (eList == null) {
                    eList = new List<Event>();
                }
            }
            for (int i = 23; i >= 0; i--)
            {
                viewModel temp = new viewModel();
                temp.time = i + ":00";
                temp.month_day = "";
                if (i == 0)
                {
                    temp.month_day = lastMonth + "月";
                }
                if (i == 1)
                {

                    temp.month_day = lastDay + "日";
                }
                bool tag = false;
                foreach (var p in eList)
                {
                    if (p.time == i + "")
                    {
                        tag = true;
                        temp.ev1 = p.ev1;
                        temp.ev2 = p.ev2;
                    }

                }

                if (tag == false)
                {
                    temp.ev1 = "";
                    temp.ev2 = "";

                }

                temp.btAllDate = lastYear + "-" + lastMonth + "-" + lastDay;
                Items.Insert(0, temp);

            }
            this.lastdate = lastdate.AddDays(-1);

        }

    }
}
