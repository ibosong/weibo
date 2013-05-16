using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeibo.ViewModels
{
    public class HomeViewModel
    {
        public List<UserInfo> users { get; set; }
        public List<MsgInfo> msgs { get; set; }

    }
    public class UserInfo
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public DateTime userTime { get; set; }//活跃时间
    }
    public class MsgInfo
    {
        public Guid msgId { get; set; }
        public string msgContent { get; set; }
    }
}