using System;
using System.Collections.Generic;

namespace NewsletterAPI.Data.Models
{
    public partial class SendNewsletterLog : Base
    {
        public int? PersonnelId { get; set; }
        public int? NewsletterId { get; set; }
        public string? NewsTitle { get; set; }
        public SendStatus? SendStatus { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public bool? ViewStatus { get; set; }
        public virtual Newsletter? Newsletter { get; set; }
        public virtual Personnel? Personnel { get; set; }

    }
    public enum SendStatus
    {
        None = 0,
        Sent,
        Sending
    }
}
