using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterFloor
{
    public int FloorId { get; set; }

    public string? FloorName { get; set; }

    public int? BlockId { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public MasterBlock? Block { get; set; }
}
