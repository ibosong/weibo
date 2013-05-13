using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyWeibo.Models
{
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MsgId { get; set; }
        [StringLength(280)]
        public string MsgContent { get; set; }
        
        public DateTime MsgDateTime { get; set; }
        [Required]
        public int CopyCount { get; set; }
        
        public Guid CopyMsgId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserProfile User { get; set; }
    }
}