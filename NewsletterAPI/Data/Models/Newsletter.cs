﻿using System;
using System.Collections.Generic;

namespace NewsletterAPI.Data.Models
{
    public  class Newsletter : Base
    {
        public Newsletter()
        {
            SendNewsletterLogs = new HashSet<SendNewsletterLog>();
        }

        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? SendDate { get; set; }

        public virtual ICollection<SendNewsletterLog> SendNewsletterLogs { get; set; }
    }
}
