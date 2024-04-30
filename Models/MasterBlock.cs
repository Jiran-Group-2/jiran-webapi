using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterBlock
{
    public int BlockId { get; set; }

    public string? BlockName { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
}
