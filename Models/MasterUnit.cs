using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterUnit
{
    public int UnitNumberId { get; set; }

    public int? UserId { get; set; }

    public string? UnitNumber { get; set; }

    public int? BlockId { get; set; }

    public int? FloorId { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }
}
