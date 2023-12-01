using System;
using System.Collections.Generic;

namespace NewsletterAPI.Models;

public partial class Personnel
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? NationalCode { get; set; }

    public virtual ICollection<SendNewsletterLog> SendNewsletterLogs { get; set; } = new List<SendNewsletterLog>();
}
