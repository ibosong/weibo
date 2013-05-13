using MyWeibo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeibo.ViewModels
{
    public class MyHome
    {
        public string UserName { get; set; }
        
        public List<Timeline> Timelines { get; set; }
        
    }
    public class CopyMsgViewModel
    {
        //要发送的内容
        public string postMsg { get; set; }
        //要转发的原微博
        public string copyMsg { get; set; }
        public Guid copyMsgId { get; set; }
        //被转发的原微博的用户
        public string userName { get; set; }

        //被转发微博
        public string Msg { get; set; }
        //被转发微博的用户
        public string name{ get; set; }

    }
    public class Timeline
    {
        public Guid id { get; set; }//微博id
        public string content { get; set; }//内容
        public int userId { get; set; }//发送人ID
        public string name { get; set; }//发送人
        public DateTime datetime { get; set; }//时间
        public Guid copyMsgId { get; set; }//被转发微博ID
        public string copyName { get; set; }//被转发的用户名
        public string copyContent { get; set; }//转发内容
    }
    
}