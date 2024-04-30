using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterBill
{
    public int BillId { get; set; }

    public string? BillSubject { get; set; }

    public string? BillDescription { get; set; }

    public decimal? BillRate { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }
}
