using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyWeibo.Models
{
    public class WeiboContext:DbContext
    {
        public WeiboContext()
            : base("DefaultConnection")
        { }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Follow> Follows { get; set; }
    }
}