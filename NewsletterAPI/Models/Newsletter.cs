using System;
using System.Collections.Generic;

namespace NewsletterAPI.Models;

public partial class Newsletter
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? SendDate { get; set; }

    public string? NewsTitle { get; set; }

    public virtual ICollection<SendNewsletterLog> SendNewsletterLogs { get; set; } = new List<SendNewsletterLog>();
}
