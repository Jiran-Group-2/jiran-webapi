using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterComplaintCategory
{
    public int ComplaintCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? CategoryDescription { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }
}
