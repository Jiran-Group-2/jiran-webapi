using System;
using System.Collections.Generic;

namespace Jiran.Models;

public partial class MasterVisitor
{
    public int VisitorId { get; set; }

    public string? VisitorName { get; set; }
    public string? VisitorMobileNo { get; set; }
    public string? VisitorNRIC  { get; set; }

    public int? VisitorQuantity { get; set; }

    public string? VisitorPurposeOfVisit { get; set; }

    public int? VisitorVehicleType { get; set; }

    public string? VisitorVehicle { get; set; }

    public string? VisitorVehiclePlate { get; set; }

    public string? ApprovalStatus { get; set; }

    public int? UnitNumberId { get; set; }

    public int? CreatedById { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? QrFileName { get; set; }

    public DateTime? QrExpiryDate { get; set; }

    public MasterUnit Unit { get; set; }   
    
}
