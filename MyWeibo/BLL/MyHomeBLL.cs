using MyWeibo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyWeibo.ViewModels;
using System.Data;
namespace MyWeibo.BLL
{
    public class MyHomeBLL
    {
        private WeiboContext db = new WeiboContext();

        
        /// <summary>
        /// 获取时间线(微博id，微博内容，用户名，时间）
        /// </summary>
        /// <param name="user">
        /// 用户user
        /// </param>
        /// <returns>
        /// 返回关注用户的微博LIst
        /// </returns>
        public List<Timeline> GetTimeline(int id)
        {
            //关注用户的ID
            List<int> FollowsId =
                (from h in db.Follows
                 where id.Equals(h.FollowingId)
                 select h.UserId).ToList();
            //需包含自己发的微博
            FollowsId.Add(id);
           
            
            
            var message =
                (from o in db.Messages
                 join c in db.UserProfiles
                 on o.UserId equals c.UserId
                 where FollowsId.Contains(o.UserId)
                 select new Timeline()
                 {
                     id = o.MsgId,
                     content = o.MsgContent,
                     name = c.UserName,
                     userId=o.UserId,
                     datetime = o.MsgDateTime,
                     copyMsgId = o.CopyMsgId,
                     copyContent = (from y in db.Messages
                                    where y.MsgId.Equals(o.CopyMsgId)
                                    select y.MsgContent).FirstOrDefault(),
                     copyName = (from yy in db.Messages
                                 join xx in db.UserProfiles
                                 on yy.UserId equals (xx.UserId)
                                 where yy.MsgId.Equals(o.CopyMsgId)
                                 select xx.UserName).FirstOrDefault()

                 });

            List<Timeline> msg = message.OrderByDescending(m=>m.datetime).ToList();



            return msg;
        }

       
        public string GetUserNameById(int userId)
        {
            return (from o in db.UserProfiles
                    where o.UserId == userId
                    select o.UserName).FirstOrDefault();
        }
        public string GetUserNameByMsgId(Guid msgId)
        {

            string name = (from m in db.Messages
                           join n in db.UserProfiles
                           on m.UserId equals n.UserId
                          where m.MsgId == msgId
                          select m.User.UserName).FirstOrDefault();
            return name;
        }
        public void PostMessage(Message msg)
        {

            db.Messages.Add(msg);
            db.SaveChanges();
        }
        public Guid GetCopyMsgId(Guid msgId)
        {
            return (from m in db.Messages
                        where m.MsgId==msgId
                        select m.CopyMsgId).FirstOrDefault();
        }
        public Message GetMessage(Guid msgId)
        {
            return db.Messages.Find(msgId);
        }
        public void UpdateMessage(Message msg)
        {
            db.Entry(msg).State = EntityState.Modified;
        }

       
    }
    

}