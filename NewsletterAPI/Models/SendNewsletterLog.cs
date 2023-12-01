using System;
using System.Collections.Generic;

namespace NewsletterAPI.Models;

public partial class SendNewsletterLog
{
    public int Id { get; set; }

    public int? NewsletterId { get; set; }

    public string? NewsTitle { get; set; }

    public DateTime? SendTime { get; set; }

    public DateTime? ReceiveTime { get; set; }

    public bool? ViewStatus { get; set; }

    public int? PersonnelId { get; set; }

    public virtual Newsletter? Newsletter { get; set; }

    public virtual Personnel? Personnel { get; set; }
}
