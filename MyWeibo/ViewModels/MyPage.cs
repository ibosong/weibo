using MyWeibo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWeibo.ViewModels
{
    public class MyPageViewModel
    {
        public List<Timeline> myMsgs { get; set; }
        public string userName { get; set; }
        public DateTime createDate { get; set; }
        public int userId { get; set; }
        public string attentionTxt { get; set; }

    }
}