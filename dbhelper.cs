using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Windows.Storage;
namespace DaemonTime
{

    class SQLiteDBHelper
    {
        private  string dbpath = string.Empty;
        public SQLiteDBHelper()
        {
            if (string.IsNullOrEmpty(dbpath))
            {
                dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Sqlite.db");
            }

        }
        public SQLiteConnection Dbconnection()
        {

            return new SQLiteConnection(new SQLitePlatformWinRT(), dbpath);
        }

        public void CreateDatabase()
        {
            var db = Dbconnection();
            db.CreateTable<Event>();
        }

        public void Add( Event e)
        {
            var db = Dbconnection();
            db.Insert(e);

        }
        public void Update( Event e)
        {
            var db = Dbconnection();
            db.Update(e);
        }
        public void Delete( Event e)
        {
            var db = Dbconnection();
            db.Delete<Event>(e.dateTime);
        }
       public  List<Event> Querybyday( string year, string month, string day)
        {
            var db = Dbconnection();
            var query = db.Table<Event>().Where(v => (v.year == year && v.month == month && v.day == day));
            List<Event> lt = new List<Event>();
            foreach (Event q in query)
            {
                lt.Add(q);
            }
            return lt;
        }





    }
}
