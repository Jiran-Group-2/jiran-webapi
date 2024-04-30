using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterUnitBill
{
    public int UserBillId { get; set; }

    public int? BillId { get; set; }

    public int? UnitNumberId { get; set; }

    public int? UserId { get; set; }

    public decimal? Amount { get; set; }

    public decimal? Paid { get; set; }

    public decimal? Balance { get; set; }

    public string? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
}
