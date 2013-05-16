using MyWeibo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWeibo.ViewModels;
namespace MyWeibo.BLL
{
    public class HomeBLL
    {
        private WeiboContext db = new WeiboContext();

        //获取最近活跃用户
        public List<UserInfo> GetHomeUserInfo(int count)
        {
            return (from m in db.Messages
                    join n in db.UserProfiles
                        on m.UserId equals n.UserId
                    select new
                    {
                        userId = n.UserId,
                        userName = n.UserName,
                        msgDate = m.MsgDateTime
                    }).OrderByDescending(z => z.msgDate).Select(x => new UserInfo { userId = x.userId, userName = x.userName, userTime = x.msgDate }).Take(count).ToList();
        }
    }

}