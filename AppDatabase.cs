using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaemonTime.models;
using Windows.Storage;

namespace DaemonTime
{
    public static class AppDatabase
    {
        /// <summary>
        /// 数据库文件所在路径，这里使用 LocalFolder，数据库文件名叫 test.db。
        /// </summary>
        public readonly static string DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "test.db");

        public static SQLiteConnection GetDbConnection()
        {
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            conn.CreateTable<Events>();
            return conn;
        }
        public static SQLiteConnection deleteEvents()
        {
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            var conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            
            conn.DeleteAll<Events>();
            return conn;
        }
        public static SQLiteConnection update(string origin,string past)
        {
      
            using (var conn = AppDatabase.GetDbConnection())
            {
                var dbEvents = conn.Table<Events>();
                foreach (var Event in dbEvents)
                {
                    if (Event.Name == origin)
                    {
                   
                        var eve = new Events() { Id=Event.Id,Name = past ,fatherName=Event.fatherName};
                        conn.Update(eve);
                    }
                }
                return conn;
            }
        }
        public static SQLiteConnection deleteItem(string name)
        {
            using (var conn = AppDatabase.GetDbConnection())
            {
                var dbEvents = conn.Table<Events>();
                foreach (var Event in dbEvents)
                {
                    if (Event.Name == name)
                    {
                        conn.Delete(Event);
                        break;
                    }
                }
                return conn;
            }
            

                    }
                }
    }
