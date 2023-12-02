using System;
using System.Collections.Generic;

namespace NewsletterAPI.Data.Models
{
    public partial class SendNewsletterLog : Base
    {
        public string? NewsTitle { get; set; }
        public SendStatus? SendStatus { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public bool? ViewStatus { get; set; }
        public int NewsletterId { get; set; }
        public int PersonnelId { get; set; }

    }
    public enum SendStatus
    {
        None = 0,
        Sent,
        Sending
    }

    //public ICollection<Personnel> Personnel { get; set; }
}
