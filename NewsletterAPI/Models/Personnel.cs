using System;
using System.Collections.Generic;

namespace NewsletterAPI.Models
{
    public partial class Personnel
    {
        public Personnel()
        {
            NewsletterStatuses = new HashSet<NewsletterStatus>();
        }

        public int PersonnelId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<NewsletterStatus> NewsletterStatuses { get; set; }
    }
}
