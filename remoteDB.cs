
using DaemonTime.models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaemonTime
{
    public class remoteDB
    {
        static string server = "23.83.250.168";
        static string database = "UWP";
        static string user = "root";
        static string pswd = "root";

        public static bool login(string email, string password)
        {
            string connectionString = "Server = " + server + ";database = " + database + ";uid = " + user + ";password = " + pswd + ";";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand checkLogin = new MySqlCommand("select password_hash, password_salt from users where email = \"" + email + "\"", connection);
                using (MySqlDataReader reader = checkLogin.ExecuteReader())
                {
                    reader.Read();
                    string hash = reader.GetString("password_hash");
                    string salt = reader.GetString("password_salt");

                    //                 bool result = passwordGenerator.compare(password, hash, salt);
                    bool result = true;
                    if (result)
                        return true;
                    else
                        return false;
                }
            }
        }

        public static int Registers(string name, string id, string password)
        {
            string connectionString = "Server = " + server + ";database = " + database + ";uid = " + user + ";password = " + pswd + ";charset=utf8;";
           
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                System.Text.EncodingProvider ppp;
                ppp = System.Text.CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(ppp);
                connection.Open();


                MySqlCommand addCommand = new MySqlCommand("INSERT INTO user (name,account,password) VALUES ('" + name + "','" + id + "','" + password + "')", connection);
                addCommand.ExecuteNonQuery();

                string op = "CREATE TABLE Event" + name + "(id INT NOT NULL AUTO_INCREMENT,Name VARCHAR(100) NOT NULL,fatherName VARCHAR(100) NOT NULL,PRIMARY KEY(id))";
                MySqlCommand createCommand = new MySqlCommand(op, connection);
                createCommand.ExecuteNonQuery();


            }
            return 0;
        }

        public static int Push(string id, string password)
        {
            string connectionString = "Server = " + server + ";database = " + database + ";uid = " + user + ";password = " + pswd + ";charset=utf8;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                System.Text.EncodingProvider ppp;
                ppp = System.Text.CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(ppp);
                connection.Open();
                string name="";
                string op = "select name from user where account='" + id + "' and password='" + password+"'";
                MySqlCommand queryCommand = new MySqlCommand(op, connection);
                MySqlDataReader reader;
                try
                {
                    reader = queryCommand.ExecuteReader();
                
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            name = reader.GetString(0);
                        }
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
                
                reader.Close();
                
                try
                {
                    op = "delete from Event" + name;
                    MySqlCommand deleteCommand = new MySqlCommand(op, connection);
                    deleteCommand.ExecuteNonQuery();

                    using (var conn = AppDatabase.GetDbConnection())
                    {
                        var dbEvents = conn.Table<Events>();
                        foreach (var Event in dbEvents)
                        {
                            op = "INSERT INTO Event" + name + " (Name,fatherName) VALUES ('" + Event.Name + "','" + Event.fatherName + "')";
                            MySqlCommand addCommand = new MySqlCommand(op, connection);
                            addCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch(Exception)
                {
                    return 1;
                }

            }
            return 0;
        }
        public static int Pull(string id, string password)
        {
            string connectionString = "Server = " + server + ";database = " + database + ";uid = " + user + ";password = " + pswd + ";charset=utf8;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                System.Text.EncodingProvider ppp;
                ppp = System.Text.CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(ppp);
                connection.Open();
                string name = "";
                string op = "select name from user where account='" + id + "' and password='" + password+"'";
                MySqlCommand queryCommand = new MySqlCommand(op, connection);
                MySqlDataReader reader;
                try
                {
                    reader = queryCommand.ExecuteReader();
                
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            name = reader.GetString(0);
                        }
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
                
                reader.Close();
                

                try
                {
                    AppDatabase.deleteEvents();
                op = "select * from Event" + name;
                queryCommand = new MySqlCommand(op, connection);

                reader = queryCommand.ExecuteReader();
                
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            using (var conn = AppDatabase.GetDbConnection())
                            {
                                // 需要添加的 Person 对象。
                                var addPerson = new Events() { Name = reader.GetString(1), fatherName = reader.GetString(2) };

                                // 受影响行数。
                                var count = conn.Insert(addPerson);

                                //          string msg = $"新增的 Person 对象的 Id 为 {addPerson.Id}，Name 为 {addPerson.Name}";
                                //          await new MessageDialog(msg).ShowAsync();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return 1;
                }
                finally
                {
                    reader.Close();
                }

                
                


            }
            return 0;
        }
    }
 }
