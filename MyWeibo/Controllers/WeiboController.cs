using MyWeibo.Models;
using MyWeibo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyWeibo.BLL;
using WebMatrix.WebData;
using MyWeibo.Filters;

namespace MyWeibo.Controllers
{
    [InitializeSimpleMembership]
    [Authorize]
    public class WeiboController : Controller
    {
        
        MyHomeBLL myhomeBLL = new MyHomeBLL();
        MyPageBLL mypageBLL = new MyPageBLL();
        //
        // GET: /Weibo/
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                MyHome myHome = new MyHome();
                int id = WebSecurity.GetUserId(User.Identity.Name);
                myHome.Timelines = myhomeBLL.GetTimeline(id);
                
                myHome.UserName = myhomeBLL.GetUserNameById(id);
                return View(myHome);
            }
            else return View();
        }

        [Authorize]
        public ActionResult PostMessage(string content,Guid copyMsgId)
        {
            Message postMsg = new Message();
            if (copyMsgId != Guid.Empty)//如果是转发微博，被转发微博的转发次数+1
            {
                myhomeBLL.GetMessage(copyMsgId).CopyCount++;
            }
            postMsg.CopyMsgId = copyMsgId;
            postMsg.UserId = WebSecurity.GetUserId(User.Identity.Name);
            postMsg.MsgDateTime = DateTime.Now;
            postMsg.CopyCount = 0;
            postMsg.MsgContent = content;
            string stateMsg="出现错误！";
            
            if(!string.IsNullOrWhiteSpace( postMsg.MsgContent))
            {
                
                myhomeBLL.PostMessage(postMsg);
                stateMsg="发送成功！";
            }

            return Json(new { message = stateMsg },JsonRequestBehavior.AllowGet);
        }


        public ActionResult DelMessage(Guid msgId)
        {
            string state = "删除成功！";
            try
            {
                mypageBLL.RemoveMessage(msgId);
            }
            catch
            {

                state = "出现错误！";
            }

            return Json(new { message = state }, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 转发微博时弹窗，用这个Partial View填充
        /// </summary>
        /// <param name="msgId">被转发微博的Id</param>
        /// <returns></returns>
        public ActionResult CopyMessagePartial(Guid msgId)
        {
            CopyMsgViewModel model = new CopyMsgViewModel();
            model.name = myhomeBLL.GetUserNameByMsgId(msgId);
            if (myhomeBLL.GetCopyMsgId(msgId) == Guid.Empty)//被转发的微博，没有转发其他微博
            {
                model.copyMsgId = msgId;
                model.userName = myhomeBLL.GetUserNameByMsgId(msgId);
                model.copyMsg = myhomeBLL.GetMessage(msgId).MsgContent;
                model.Msg = string.Empty;
            }
            else
            {
                Guid mid = myhomeBLL.GetCopyMsgId(msgId);
                model.copyMsgId = mid;
                model.Msg = myhomeBLL.GetMessage(msgId).MsgContent;
                model.userName = myhomeBLL.GetUserNameByMsgId(mid);
                model.copyMsg = myhomeBLL.GetMessage(mid).MsgContent;
                
            }
            return PartialView(model);
        }

        public ActionResult MyPage(int id)
        {
            MyPageViewModel model = mypageBLL.GetMyPageInfo(id);
            model.userName = myhomeBLL.GetUserNameById(id);
            model.createDate = WebSecurity.GetCreateDate(model.userName);
            model.userId = id;
            return View(model);
        }
        
        
        
        public ActionResult FollowPartial(int id)
        {

            int currentUserId = WebSecurity.GetUserId(WebSecurity.CurrentUserName);
            ViewBag.userId = id;
            if(mypageBLL.IsFollowing(currentUserId,id)) 
            {
                ViewBag.attention = "取消关注";
            }
            else ViewBag.attention = "关注";
            return PartialView("FollowPartial");
        }
        
        public ActionResult HandleAttention(int id)
        {
            int currentUserId = WebSecurity.GetUserId(WebSecurity.CurrentUserName);
            //string message;
            if (mypageBLL.IsFollowing(currentUserId, id))//已关注，需取消关注
            {
                //message = "关注";
                mypageBLL.RemoveFollowing(currentUserId, id);
            }
            else
            {
                mypageBLL.AddFollowing(currentUserId, id);
                //message = "取消关注";
            }
            return RedirectToAction("MyPage", new { id = id });
        }
    
    }
        
}
