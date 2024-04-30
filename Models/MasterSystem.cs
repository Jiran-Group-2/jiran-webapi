using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterSystem
{
    public int SystemId { get; set; }

    public string? Version { get; set; }

    public string? AreaName { get; set; }

    public string? Address { get; set; }

    public string? OfficeNumber1 { get; set; }

    public string? OfficeNumber2 { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }
}
