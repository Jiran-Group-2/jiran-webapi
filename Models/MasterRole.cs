using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }
}
