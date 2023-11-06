using System;
using System.Collections.Generic;

namespace NewsletterAPI.Models
{
    public partial class Newsletter
    {
        public Newsletter()
        {
            NewsletterStatuses = new HashSet<NewsletterStatus>();
        }

        public int NewsletterId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? SendDate { get; set; }

        public virtual ICollection<NewsletterStatus> NewsletterStatuses { get; set; }
    }
}
