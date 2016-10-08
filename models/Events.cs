using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;


namespace DaemonTime.models
{
    public class Events
    {
        [PrimaryKey]// 主键。
        [AutoIncrement]// 自动增长。
        public int Id
        {
            get;
            set;
        }
        
        [MaxLength(5)]// 对应到数据库 varchar 的大小。
        public string Name
        {
            get;
            set;
        }
        public string fatherName
        {
            get;
            set;
        }
        

    }
}
