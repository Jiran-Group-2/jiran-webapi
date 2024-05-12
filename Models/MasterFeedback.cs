using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterFeedback
{
    public int FeedbackId { get; set; }
    public int ComplaintId { get; set; }
    public string? FeedbackSubject { get; set; }

    public string? FeedbackDescription { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
    public MasterComplaint? Complaint { get; set; }
}
