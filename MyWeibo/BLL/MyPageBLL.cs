using MyWeibo.Models;
using MyWeibo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeibo.BLL
{
    public class MyPageBLL
    {
        private WeiboContext db = new WeiboContext();
        
        public MyPageViewModel GetMyPageInfo(int userId)
        {
            MyPageViewModel model = new MyPageViewModel();
            model.myMsgs = GetMyTimeline(userId);
            return model;

        }
public List<Timeline> GetMyTimeline(int id)
        {




            var message =
                (from o in db.Messages
                 join c in db.UserProfiles
                 on o.UserId equals c.UserId
                 where o.UserId.Equals(id)
                 select new Timeline()
                 {
                     id = o.MsgId,
                     content = o.MsgContent,
                     name = c.UserName,
                     datetime = o.MsgDateTime,
                     copyMsgId = o.CopyMsgId,
                     copyMsgCount=o.CopyCount,
                     copyContent = (from y in db.Messages
                                    where y.MsgId.Equals(o.CopyMsgId)
                                    select y.MsgContent).FirstOrDefault(),
                     copyName = (from yy in db.Messages
                                 join xx in db.UserProfiles
                                 on yy.UserId equals (xx.UserId)
                                 where yy.MsgId.Equals(o.CopyMsgId)
                                 select xx.UserName).FirstOrDefault()

                 });

            List<Timeline> msg = message.ToList();



            return msg;
        }
        public void RemoveMessage(Guid id)
        {
            Message model = db.Messages.Find(id);
            db.Messages.Remove(model);
            db.SaveChanges();

        }
        public bool IsFollowing(int id1,int id2)//id1是否关注id2
        {
            List<int> f = (from m in db.Follows
                     where m.FollowingId.Equals(id1)
                     select m.UserId).ToList();
            if (f.Contains(id2)) return true;
            else return false;
 
        }

        public void RemoveFollowing(int id1, int id2)
        {
            Follow model = db.Follows.Where(m => m.FollowingId == id1 && m.UserId == id2).FirstOrDefault();
            db.Follows.Remove(model);
            db.SaveChanges();
        }
        public void AddFollowing(int id1, int id2)
        {
            db.Follows.Add(new Follow { UserId = id2, FollowingId = id1 });
            db.SaveChanges();
        }
    }
}