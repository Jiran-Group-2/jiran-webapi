USE [master]
GO
/****** Object:  Database [JiranApp]    Script Date: 5/1/2024 12:15:15 AM ******/
CREATE DATABASE [JiranApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JiranApp', FILENAME = N'/var/opt/mssql/data/JiranApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JiranApp_log', FILENAME = N'/var/opt/mssql/data/JiranApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JiranApp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JiranApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JiranApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JiranApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JiranApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JiranApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JiranApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [JiranApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JiranApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JiranApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JiranApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JiranApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JiranApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JiranApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JiranApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JiranApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JiranApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JiranApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JiranApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JiranApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JiranApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JiranApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JiranApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JiranApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JiranApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [JiranApp] SET  MULTI_USER 
GO
ALTER DATABASE [JiranApp] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [JiranApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JiranApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JiranApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JiranApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JiranApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JiranApp', N'ON'
GO
ALTER DATABASE [JiranApp] SET QUERY_STORE = OFF
GO
USE [JiranApp]
GO
/****** Object:  Table [dbo].[Master_Announcement]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Announcement](
	[Announcement_ID] [int] IDENTITY(1,1) NOT NULL,
	[Announcement_Subject] [nvarchar](100) NULL,
	[Announcement_Description] [nvarchar](max) NULL,
	[Attachment_ID] [int] NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Announcement_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Attachment]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Attachment](
	[Attachment_ID] [int] IDENTITY(1,1) NOT NULL,
	[Attachment_File_Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Attachment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Bill]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Bill](
	[Bill_ID] [int] IDENTITY(1,1) NOT NULL,
	[Bill_Subject] [nvarchar](100) NULL,
	[Bill_Description] [nvarchar](max) NULL,
	[Bill_Rate] [decimal](18, 0) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Bill_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Block]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Block](
	[Block_ID] [int] IDENTITY(1,1) NOT NULL,
	[Block_Name] [nvarchar](50) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Block_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Complaint]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Complaint](
	[Complaint_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Complaint_Category_ID] [int] NULL,
	[Complaint_Location] [nvarchar](100) NULL,
	[Complaint_Subject] [nvarchar](50) NULL,
	[Complaint_Description] [nvarchar](500) NULL,
	[Attachment_ID] [int] NULL,
	[Feedback_ID] [int] NULL,
	[Status] [char](1) NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Complaint_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Complaint_Category]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Complaint_Category](
	[Complaint_Category_ID] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [nvarchar](100) NULL,
	[Category_Description] [nvarchar](500) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Complaint_Category_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Feedback]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Feedback](
	[Feedback_ID] [int] IDENTITY(1,1) NOT NULL,
	[Feedback_Subject] [nvarchar](100) NULL,
	[Feedback_Description] [nvarchar](max) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Feedback_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Floor]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Floor](
	[Floor_ID] [int] IDENTITY(1,1) NOT NULL,
	[Floor_Name] [nvarchar](50) NULL,
	[Block_ID] [int] NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Floor_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Role]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Role](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](50) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_System]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_System](
	[System_ID] [int] IDENTITY(1,1) NOT NULL,
	[Version] [nvarchar](50) NULL,
	[Area_Name] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[Office_Number_1] [nvarchar](15) NULL,
	[Office_Number_2] [nvarchar](15) NULL,
	[Fax] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[System_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Title]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Title](
	[Title_ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Title_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Unit]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Unit](
	[Unit_Number_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_ID] [int] NULL,
	[Unit_Number] [nvarchar](50) NULL,
	[Block_ID] [int] NULL,
	[Floor_ID] [int] NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Unit_Number_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Unit_Bill]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Unit_Bill](
	[User_Bill_ID] [int] IDENTITY(1,1) NOT NULL,
	[Bill_ID] [int] NULL,
	[Unit_Number_ID] [int] NULL,
	[User_ID] [int] NULL,
	[Amount] [decimal](18, 0) NULL,
	[Paid] [decimal](18, 0) NULL,
	[Balance] [decimal](18, 0) NULL,
	[Created_By_ID] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[User_Bill_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_User]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_User](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[User_Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](100) NULL,
	[Title] [int] NULL,
	[NRIC] [nvarchar](max) NULL,
	[Unit_Number_ID] [int] NULL,
	[Mobile_No] [nvarchar](15) NULL,
	[Home_No] [nvarchar](15) NULL,
	[Status] [char](1) NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Role_ID] [int] NULL,
	[System_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Master_Visitor]    Script Date: 5/1/2024 12:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Master_Visitor](
	[Visitor_ID] [int] IDENTITY(1,1) NOT NULL,
	[Visitor_Name] [nvarchar](100) NULL,
	[Visitor_NRIC] [nvarchar](10) NULL,
	[Visitor_Mobile_No] [nvarchar](15) NULL,
	[Visitor_Quantity] [int] NULL,
	[Visitor_Purpose_Of_Visit] [nvarchar](100) NULL,
	[Visitor_Vehicle_Type] [int] NULL,
	[Visitor_Vehicle] [nvarchar](100) NULL,
	[Visitor_Vehicle_Plate] [nvarchar](15) NULL,
	[Approval_Status] [char](1) NULL,
	[Unit_Number_ID] [int] NULL,
	[Created_By_ID] [int] NULL,
	[Created_Date] [datetime] NULL,
	[QR_File_Name] [nvarchar](100) NULL,
	[QR_Expiry_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Visitor_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Master_Block] ON 
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (1, N'Block A', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (2, N'Block B', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (3, N'Block C', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (4, N'Block D', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (5, N'Block E', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
INSERT [dbo].[Master_Block] ([Block_ID], [Block_Name], [Created_By_ID], [Created_Date], [System_ID]) VALUES (6, N'Block F', 1, CAST(N'2024-04-30T08:59:31.973' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Master_Block] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_Floor] ON 
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (1, N'Floor 1', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (2, N'Floor 2', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (3, N'Floor 3', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (4, N'Floor 4', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (5, N'Floor 5', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (6, N'Floor 6', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (7, N'Floor 7', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (8, N'Floor 8', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (9, N'Floor 9', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (10, N'Floor 10', 1, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (11, N'Floor 1', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (12, N'Floor 2', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (13, N'Floor 3', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (14, N'Floor 4', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (15, N'Floor 5', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (16, N'Floor 6', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (17, N'Floor 7', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (18, N'Floor 8', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (19, N'Floor 9', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (20, N'Floor 10', 2, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (21, N'Floor 1', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (22, N'Floor 2', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (23, N'Floor 3', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (24, N'Floor 4', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (25, N'Floor 5', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (26, N'Floor 6', 3, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (27, N'Floor 1', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (28, N'Floor 2', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (29, N'Floor 3', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (30, N'Floor 4', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (31, N'Floor 5', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (32, N'Floor 6', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (33, N'Floor 7', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (34, N'Floor 8', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (35, N'Floor 9', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (36, N'Floor 10', 4, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (37, N'Floor 1', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (38, N'Floor 2', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (39, N'Floor 3', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (40, N'Floor 4', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (41, N'Floor 5', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (42, N'Floor 6', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (43, N'Floor 7', 5, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (44, N'Floor 7', 6, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (45, N'Floor 8', 6, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (46, N'Floor 9', 6, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
INSERT [dbo].[Master_Floor] ([Floor_ID], [Floor_Name], [Block_ID], [Created_By_ID], [Created_Date]) VALUES (47, N'Floor 10', 6, 1, CAST(N'2024-04-30T09:02:46.660' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_Floor] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_Role] ON 
GO
INSERT [dbo].[Master_Role] ([Role_ID], [Role_Name], [Created_By_ID], [Created_Date]) VALUES (1, N'Administrator', 1, CAST(N'2024-04-24T11:40:53.340' AS DateTime))
GO
INSERT [dbo].[Master_Role] ([Role_ID], [Role_Name], [Created_By_ID], [Created_Date]) VALUES (2, N'Management', 1, CAST(N'2024-04-24T11:40:53.347' AS DateTime))
GO
INSERT [dbo].[Master_Role] ([Role_ID], [Role_Name], [Created_By_ID], [Created_Date]) VALUES (3, N'User', 1, CAST(N'2024-04-24T11:40:53.347' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Master_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_System] ON 
GO
INSERT [dbo].[Master_System] ([System_ID], [Version], [Area_Name], [Address], [Office_Number_1], [Office_Number_2], [Fax], [Email]) VALUES (1, N'1.0', N'Empire Damansara', N'Empire Damansara 47820, Petaling Jaya, Selangor', N'0311223344', N'0388888888', N'0382828282', N'admin_empire@gmail.com')
GO
INSERT [dbo].[Master_System] ([System_ID], [Version], [Area_Name], [Address], [Office_Number_1], [Office_Number_2], [Fax], [Email]) VALUES (2, N'jn', NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Master_System] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_Title] ON 
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (1, N'Mr')
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (2, N'Mrs')
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (3, N'Miss')
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (4, N'Ms')
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (5, N'Dr')
GO
INSERT [dbo].[Master_Title] ([Title_ID], [Title]) VALUES (6, N'Prof')
GO
SET IDENTITY_INSERT [dbo].[Master_Title] OFF
GO
SET IDENTITY_INSERT [dbo].[Master_User] ON 
GO
INSERT [dbo].[Master_User] ([User_ID], [User_Login], [Password], [Name], [Title], [NRIC], [Unit_Number_ID], [Mobile_No], [Home_No], [Status], [Created_By_ID], [Created_Date], [Role_ID], [System_ID]) VALUES (1, N'admin', N'admin', N'admin', 1, N'999999999999', NULL, N'60179264006', NULL, N'A', 1, CAST(N'2024-04-24T11:42:46.693' AS DateTime), 1, 1)
GO
INSERT [dbo].[Master_User] ([User_ID], [User_Login], [Password], [Name], [Title], [NRIC], [Unit_Number_ID], [Mobile_No], [Home_No], [Status], [Created_By_ID], [Created_Date], [Role_ID], [System_ID]) VALUES (2, N'hadzim', N'haha', N'muhammad farhan', 2, N'123456789', 10, N'123123123', N'11111111', N'A', 2, CAST(N'2024-04-24T14:27:54.520' AS DateTime), 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Master_User] OFF
GO
USE [master]
GO
ALTER DATABASE [JiranApp] SET  READ_WRITE 
GO
