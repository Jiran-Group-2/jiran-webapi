using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterAnnouncement
{
    public int AnnouncementId { get; set; }

    public string? AnnouncementSubject { get; set; }

    public string? AnnouncementDescription { get; set; }

    public int? AttachmentId { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
}
