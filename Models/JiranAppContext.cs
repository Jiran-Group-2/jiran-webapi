using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Jiran.Models;

public partial class JiranAppContext : DbContext
{
    public JiranAppContext()
    {
    }

    public JiranAppContext(DbContextOptions<JiranAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MasterAnnouncement> MasterAnnouncements { get; set; }

    public virtual DbSet<MasterAttachment> MasterAttachments { get; set; }

    public virtual DbSet<MasterBill> MasterBills { get; set; }

    public virtual DbSet<MasterBlock> MasterBlocks { get; set; }

    public virtual DbSet<MasterComplaint> MasterComplaints { get; set; }

    public virtual DbSet<MasterComplaintCategory> MasterComplaintCategories { get; set; }

    public virtual DbSet<MasterFeedback> MasterFeedbacks { get; set; }

    public virtual DbSet<MasterFloor> MasterFloors { get; set; }

    public virtual DbSet<MasterRole> MasterRoles { get; set; }

    public virtual DbSet<MasterSystem> MasterSystems { get; set; }

    public virtual DbSet<MasterTitle> MasterTitles { get; set; }

    public virtual DbSet<MasterUnit> MasterUnits { get; set; }

    public virtual DbSet<MasterUnitBill> MasterUnitBills { get; set; }

    public virtual DbSet<MasterUser> MasterUsers { get; set; }

    public virtual DbSet<MasterVisitor> MasterVisitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    if (!optionsBuilder.IsConfigured)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    }
        //=> optionsBuilder.UseSqlServer("Server=Hadzims-MacBook-Pro.local,1433;Database=JiranApp;User=SA;Password=MuhammadHadzim1;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MasterAnnouncement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Master_A__853AB7CF55EB871D");

            entity.ToTable("Master_Announcement");

            entity.Property(e => e.AnnouncementId).HasColumnName("Announcement_ID");
            entity.Property(e => e.AnnouncementDescription).HasColumnName("Announcement_Description");
            entity.Property(e => e.AnnouncementSubject)
                .HasMaxLength(100)
                .HasColumnName("Announcement_Subject");
            entity.Property(e => e.AttachmentId).HasColumnName("Attachment_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
        });

        modelBuilder.Entity<MasterAttachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Master_A__97E4BED7894A96CE");

            entity.ToTable("Master_Attachment");

            entity.Property(e => e.AttachmentId).HasColumnName("Attachment_ID");
            entity.Property(e => e.AttachmentFileName)
                .HasMaxLength(100)
                .HasColumnName("Attachment_File_Name");
        });

        modelBuilder.Entity<MasterBill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Master_B__CF6E7D4304C57904");

            entity.ToTable("Master_Bill");

            entity.Property(e => e.BillId).HasColumnName("Bill_ID");
            entity.Property(e => e.BillDescription).HasColumnName("Bill_Description");
            entity.Property(e => e.BillRate)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Bill_Rate");
            entity.Property(e => e.BillSubject)
                .HasMaxLength(100)
                .HasColumnName("Bill_Subject");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
        });

        modelBuilder.Entity<MasterBlock>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PK__Master_B__A848958603F77F7B");

            entity.ToTable("Master_Block");

            entity.Property(e => e.BlockId)
                .ValueGeneratedNever()
                .HasColumnName("Block_ID");
            entity.Property(e => e.BlockName)
                .HasMaxLength(50)
                .HasColumnName("Block_Name");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
        });

        modelBuilder.Entity<MasterComplaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Master_C__0C536DA6FE61AB52");

            entity.ToTable("Master_Complaint");

            entity.Property(e => e.ComplaintId).HasColumnName("Complaint_ID");
            entity.Property(e => e.AttachmentId).HasColumnName("Attachment_ID");
            entity.Property(e => e.ComplaintCategoryId).HasColumnName("Complaint_Category_ID");
            entity.Property(e => e.ComplaintDescription)
                .HasMaxLength(500)
                .HasColumnName("Complaint_Description");
            entity.Property(e => e.ComplaintLocation)
                .HasMaxLength(100)
                .HasColumnName("Complaint_Location");
            entity.Property(e => e.ComplaintSubject)
                .HasMaxLength(50)
                .HasColumnName("Complaint_Subject");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FeedbackId).HasColumnName("Feedback_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<MasterComplaintCategory>(entity =>
        {
            entity.HasKey(e => e.ComplaintCategoryId).HasName("PK__Master_C__496DD96AFD25265D");

            entity.ToTable("Master_Complaint_Category");

            entity.Property(e => e.ComplaintCategoryId).HasColumnName("Complaint_Category_ID");
            entity.Property(e => e.CategoryDescription)
                .HasMaxLength(500)
                .HasColumnName("Category_Description");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("Category_Name");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
        });

        modelBuilder.Entity<MasterFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Master_F__CD3992F8EACCC0BD");

            entity.ToTable("Master_Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("Feedback_ID");
            entity.Property(e => e.ComplaintId).HasColumnName("Complaint_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FeedbackDescription).HasColumnName("Feedback_Description");
            entity.Property(e => e.FeedbackSubject)
                .HasMaxLength(100)
                .HasColumnName("Feedback_Subject");
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
        });

        modelBuilder.Entity<MasterFloor>(entity =>
        {
            entity.HasKey(e => e.FloorId).HasName("PK__Master_F__9ABEF2D942ED57F0");

            entity.ToTable("Master_Floor");

            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.BlockId).HasColumnName("Block_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FloorName)
                .HasMaxLength(50)
                .HasColumnName("Floor_Name");
        });

        modelBuilder.Entity<MasterRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Master_R__D80AB49B8825A804");

            entity.ToTable("Master_Role");

            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<MasterSystem>(entity =>
        {
            entity.HasKey(e => e.SystemId).HasName("PK__Master_S__C7178F12726792CE");

            entity.ToTable("Master_System");

            entity.Property(e => e.SystemId).HasColumnName("System_ID");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AreaName)
                .HasMaxLength(500)
                .HasColumnName("Area_Name");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Fax).HasMaxLength(15);
            entity.Property(e => e.OfficeNumber1)
                .HasMaxLength(15)
                .HasColumnName("Office_Number_1");
            entity.Property(e => e.OfficeNumber2)
                .HasMaxLength(15)
                .HasColumnName("Office_Number_2");
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<MasterTitle>(entity =>
        {
            entity.HasKey(e => e.TitleId).HasName("PK__Master_T__01D44740354AFA64");

            entity.ToTable("Master_Title");

            entity.Property(e => e.TitleId).HasColumnName("Title_ID");
            entity.Property(e => e.Title).HasMaxLength(10);
        });

        modelBuilder.Entity<MasterUnit>(entity =>
        {
            entity.HasKey(e => e.UnitNumberId).HasName("PK__Master_U__CB4B7EA2273349BE");

            entity.ToTable("Master_Unit");

            entity.Property(e => e.UnitNumberId).HasColumnName("Unit_Number_ID");
            entity.Property(e => e.BlockId).HasColumnName("Block_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.UnitNumber)
                .HasMaxLength(50)
                .HasColumnName("Unit_Number");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<MasterUnitBill>(entity =>
        {
            entity.HasKey(e => e.UserBillId).HasName("PK__Master_U__F8B8457059237A94");

            entity.ToTable("Master_Unit_Bill");

            entity.Property(e => e.UserBillId).HasColumnName("User_Bill_ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillId).HasColumnName("Bill_ID");
            entity.Property(e => e.CreatedById)
                .HasMaxLength(50)
                .HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Paid).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
            entity.Property(e => e.UnitNumberId).HasColumnName("Unit_Number_ID");
            entity.Property(e => e.UserId).HasColumnName("User_ID");
        });

        modelBuilder.Entity<MasterUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Master_U__206D9190BBD3D675");

            entity.ToTable("Master_User");

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.TitleId).HasColumnName("Title_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.HomeNo)
                .HasMaxLength(15)
                .HasColumnName("Home_No");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(15)
                .HasColumnName("Mobile_No");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nric).HasColumnName("NRIC");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("Role_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SystemId).HasColumnName("System_ID");
            entity.Property(e => e.UnitNumberId).HasColumnName("Unit_Number_ID");
            entity.Property(e => e.UserLogin)
                .HasMaxLength(50)
                .HasColumnName("User_Login");
        });

        modelBuilder.Entity<MasterVisitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("PK__Master_V__E16AB2AD28196C76");

            entity.ToTable("Master_Visitor");

            entity.Property(e => e.VisitorId).HasColumnName("Visitor_ID");
            entity.Property(e => e.ApprovalStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Approval_Status");
            entity.Property(e => e.CreatedById).HasColumnName("Created_By_ID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.QrExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("QR_Expiry_Date");
            entity.Property(e => e.QrFileName)
                .HasMaxLength(100)
                .HasColumnName("QR_File_Name");
            entity.Property(e => e.UnitNumberId).HasColumnName("Unit_Number_ID");
            entity.Property(e => e.VisitorName)
                .HasMaxLength(100)
                .HasColumnName("Visitor_Name");
            entity.Property(e => e.VisitorPurposeOfVisit)
                .HasMaxLength(100)
                .HasColumnName("Visitor_Purpose_Of_Visit");
            entity.Property(e => e.VisitorQuantity).HasColumnName("Visitor_Quantity");
            entity.Property(e => e.VisitorVehicle)
                .HasMaxLength(100)
                .HasColumnName("Visitor_Vehicle");
            entity.Property(e => e.VisitorVehiclePlate)
                .HasMaxLength(15)
                .HasColumnName("Visitor_Vehicle_Plate");
            entity.Property(e => e.VisitorVehicleType).HasColumnName("Visitor_Vehicle_Type");
            entity.Property(e => e.VisitorMobileNo).HasColumnName("Visitor_Mobile_No");
            entity.Property(e => e.VisitorNRIC).HasColumnName("Visitor_NRIC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
