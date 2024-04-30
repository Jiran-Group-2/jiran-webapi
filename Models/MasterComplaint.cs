using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterComplaint
{
    public int ComplaintId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? ComplaintCategoryId { get; set; }

    public string? ComplaintLocation { get; set; }

    public string? ComplaintSubject { get; set; }

    public string? ComplaintDescription { get; set; }

    public int? AttachmentId { get; set; }

    public int? FeedbackId { get; set; }

    public string? Status { get; set; }

    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
}
