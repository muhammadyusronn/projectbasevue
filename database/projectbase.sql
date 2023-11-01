USE [master]
GO
/****** Object:  Database [ProjectBaseVue]    Script Date: 11/1/2023 5:31:20 PM ******/
CREATE DATABASE [ProjectBaseVue]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectBaseVue_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProjectBaseVue.mdf' , SIZE = 6336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProjectBaseVue_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\ProjectBaseVue.ldf' , SIZE = 52416KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProjectBaseVue] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectBaseVue].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectBaseVue] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectBaseVue] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectBaseVue] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectBaseVue] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectBaseVue] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectBaseVue] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectBaseVue] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectBaseVue] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectBaseVue] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectBaseVue] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectBaseVue] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ProjectBaseVue]
GO
/****** Object:  Table [dbo].[APISetting]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APISetting](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppName] [varchar](250) NOT NULL,
	[Credential] [varchar](250) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](250) NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](10) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_API_Setting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Changelog]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Changelog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Version] [varchar](50) NULL,
	[VersionDate] [datetime] NULL,
	[Remarks] [varchar](250) NULL,
	[Active] [varchar](1) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](250) NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](1) NOT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_Changelog\] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangelogDetail]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangelogDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ChangelogId] [bigint] NOT NULL,
	[Description] [varchar](max) NULL,
	[Info1] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](250) NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](1) NOT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_ChangelogDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyDetail]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Company_Id] [bigint] NOT NULL,
	[LocationCode] [varchar](50) NOT NULL,
	[Address] [varchar](max) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Company_Detail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK__Departme__3214EC076912F136] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NULL,
	[email] [nvarchar](100) NULL,
	[phone] [int] NOT NULL,
	[salary] [int] NOT NULL,
	[departement] [nvarchar](100) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Example]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Example](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentNo] [varchar](100) NOT NULL,
	[CompanyCode] [varchar](50) NULL,
	[LocationCode] [varchar](50) NULL,
	[DocumentType] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](250) NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_Example] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExampleDetail]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExampleDetail](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ExampleId] [bigint] NOT NULL,
	[MaterialCode] [varchar](50) NOT NULL,
	[Qty] [decimal](18, 2) NOT NULL,
	[UOM] [varchar](50) NULL,
	[CreatedBy] [varchar](250) NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](1) NOT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_ExampleDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Group_Code] [varchar](50) NULL,
	[Group_Description] [varchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMenu]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMenu](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Group_Id] [bigint] NOT NULL,
	[Menu_Id] [bigint] NOT NULL,
	[View] [bit] NOT NULL,
	[Edit] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[Create] [bit] NOT NULL,
	[Print] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Group_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [bigint] NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [varchar](250) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK__Location__3214EC0740CC9884] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuName] [varchar](100) NULL,
	[MenuAction] [varchar](100) NULL,
	[MenuController] [varchar](100) NULL,
	[MenuIcon] [varchar](100) NULL,
	[Parent] [bigint] NULL,
	[Ordinal] [int] NULL,
	[Visible] [bit] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationLog]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](150) NULL,
	[Action] [varchar](50) NULL,
	[Subject] [varchar](100) NULL,
	[MailTo] [varchar](max) NULL,
	[MailCc] [varchar](max) NULL,
	[MailBody] [nvarchar](max) NULL,
	[Result] [varchar](50) NULL,
	[Created_By] [varchar](50) NULL,
	[Created_Date] [datetime] NULL,
 CONSTRAINT [PK_Notification_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestLog]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](150) NULL,
	[LocationCode] [varchar](50) NULL,
	[ActionRole] [nvarchar](50) NULL,
	[Action] [nvarchar](50) NULL,
	[Notes] [varchar](max) NULL,
	[ActionBy] [varchar](50) NULL,
	[ActionDate] [datetime] NULL,
 CONSTRAINT [PK_Request_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [varchar](100) NULL,
	[Modul] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Setting]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](100) NULL,
	[Value] [varchar](200) NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Full_Name] [varchar](150) NULL,
	[Password] [varchar](150) NULL,
	[Email] [varchar](80) NULL,
	[Location_Code] [varchar](50) NULL,
	[DepartmentCode] [varchar](50) NULL,
	[IsAdmin] [varchar](10) NOT NULL,
	[Use_AD] [varchar](10) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[LastAccessDate] [datetime] NULL,
	[FirstLogin] [varchar](10) NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCompany]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCompany](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[CompanyCode] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](250) NOT NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](250) NULL,
	[IsDeleted] [varchar](1) NOT NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](250) NULL,
 CONSTRAINT [PK_UserCompany] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Group_Id] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPassLog]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPassLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Pass_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[EditedBy] [varchar](50) NULL,
	[EditedDate] [datetime] NULL,
	[IsDeleted] [varchar](1) NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 11/1/2023 5:31:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Token] [varchar](max) NULL,
	[ExpiryDate] [datetime] NOT NULL,
	[Active] [varchar](10) NOT NULL,
	[Type] [varchar](20) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Token] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Is_Admin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Use_AD]  DEFAULT ((0)) FOR [Use_AD]
GO
ALTER TABLE [dbo].[ChangelogDetail]  WITH CHECK ADD  CONSTRAINT [FK_ChangelogDetail_Changelog] FOREIGN KEY([ChangelogId])
REFERENCES [dbo].[Changelog] ([Id])
GO
ALTER TABLE [dbo].[ChangelogDetail] CHECK CONSTRAINT [FK_ChangelogDetail_Changelog]
GO
ALTER TABLE [dbo].[CompanyDetail]  WITH CHECK ADD  CONSTRAINT [FK_Company_Detail_Company] FOREIGN KEY([Company_Id])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[CompanyDetail] CHECK CONSTRAINT [FK_Company_Detail_Company]
GO
ALTER TABLE [dbo].[ExampleDetail]  WITH CHECK ADD  CONSTRAINT [FK_ExampleDetail_Example] FOREIGN KEY([ExampleId])
REFERENCES [dbo].[Example] ([Id])
GO
ALTER TABLE [dbo].[ExampleDetail] CHECK CONSTRAINT [FK_ExampleDetail_Example]
GO
ALTER TABLE [dbo].[GroupMenu]  WITH CHECK ADD  CONSTRAINT [FK_Group_Menu_Group] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[GroupMenu] CHECK CONSTRAINT [FK_Group_Menu_Group]
GO
ALTER TABLE [dbo].[GroupMenu]  WITH CHECK ADD  CONSTRAINT [FK_Group_Menu_Menu] FOREIGN KEY([Menu_Id])
REFERENCES [dbo].[Menu] ([Id])
GO
ALTER TABLE [dbo].[GroupMenu] CHECK CONSTRAINT [FK_Group_Menu_Menu]
GO
ALTER TABLE [dbo].[UserCompany]  WITH CHECK ADD  CONSTRAINT [FK_UserCompany_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserCompany] CHECK CONSTRAINT [FK_UserCompany_User]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Group] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[Group] ([Id])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_Table_1_Group]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_Table_1_User]
GO
ALTER TABLE [dbo].[UserPassLog]  WITH CHECK ADD  CONSTRAINT [FK_User_Pass_Log_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserPassLog] CHECK CONSTRAINT [FK_User_Pass_Log_User]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_User_Role_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_User_Role_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_User_Role_User]
GO
USE [master]
GO
ALTER DATABASE [ProjectBaseVue] SET  READ_WRITE 
GO
