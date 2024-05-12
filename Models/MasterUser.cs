using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterUser
{
    public int UserId { get; set; }

    public string? UserLogin { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public int? TitleId { get; set; }

    public string? Nric { get; set; }

    public int? UnitNumberId { get; set; }
    public string? Email { get; set; }
    public string? MobileNo { get; set; }

    public string? HomeNo { get; set; }

    public string? Status { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? RoleId { get; set; }

    public MasterRole? Role{ get; set; }
    public int? SystemId { get; set; }
    public MasterSystem? System { get; set; }
    public MasterTitle? Title { get; set; }
    public MasterUnit? UnitNumber { get; set; }
}
