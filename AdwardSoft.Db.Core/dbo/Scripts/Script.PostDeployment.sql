--/*
--Post-Deployment Script Template							
----------------------------------------------------------------------------------------
-- This file contains SQL statements that will be appended to the build script.		
-- Use SQLCMD syntax to include a file in the post-deployment script.			
-- Example:      :r .\myfile.sql								
-- Use SQLCMD syntax to reference a variable in the post-deployment script.		
-- Example:      :setvar TableName MyTable							
--               SELECT * FROM [$(TableName)]					
----------------------------------------------------------------------------------------
--*/
--IF NOT EXISTS(SELECT TOP 1 1 FROM [dbo].[Role])
--BEGIN
--	INSERT INTO [dbo].[Role] VALUES('Administrator','Admin', '',0)
--	INSERT INTO [dbo].[Role] VALUES('Manager','Manager', '', 0)
--	INSERT INTO [dbo].[Role] VALUES('Editor','Editor', '', 0)
--	INSERT INTO [dbo].[Role] VALUES('User','User', '', 1)
--END
--GO
--SET IDENTITY_INSERT [dbo].[User] ON 
--GO
--IF NOT EXISTS(SELECT TOP 1 1 FROM [dbo].[User])
--BEGIN
--INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [FullName], [Avatar], [Type]) 
--VALUES (1, N'admin@adwardsoft.com', N'ADMIN@ADWARDSOFT.COM', N'admin@adwardsoft.com', N'ADMIN@ADWARDSOFT.COM', 0, N'AQAAAAEAACcQAAAAEFBCo8KQXTV7oBu4T6cBsNFr4PmOCWmEU8Xv8q0EF1NrAYtWCZIwZGVNVIz4hNzVeg==', N'PLS66W5FHPMKUBOHSFHBGQT7IK7F7MKU', NULL, NULL, 0, 0, NULL, 1, 0, N'AdwardSoft', NULL, 1)

--INSERT INTO [dbo].[UserRole] VALUES(1,1)
--END
--GO
--SET IDENTITY_INSERT [dbo].[User] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Module] ON 
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (1, N'Bản tin', '#', 'icon-stack', '', 1, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (2, N'Thông báo', '#', 'icon-notification2', '', 1, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (3, N'Phòng nhân sự', '#', 'icon-users4', '', 3, 2)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (4, N'Quản lý ứng viên ', '#', 'icon-user-check', '', 3, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (5, N'Hồ sơ nhân viên', '#', 'icon-vcard', '', 3, 2)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (6, N'Phòng IT', '#', 'icon-display', '', 6, 3)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (7, N'Sơ đồ tổ chức', '#', 'icon-lan2', '', 6, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (8, N'Báo cáo sự cố', '#', 'icon-alert', '', 6, 2)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (9, N'Phân quyền', '#', 'icon-users', '', 9, 4)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (10, N'Người dùng', 'User', 'icon-user', 'User', 9, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (11, N'Quyền người dùng', 'Role', 'icon-user-lock', 'Role', 9, 2)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (12, N'Chi tiết quyền người dùng', 'Permission', 'icon-user-plus', 'Permission', 9, 3)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (13, N'Quản trị', '#', 'icon-cog', '', 13, 5)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (14, N'Module', 'Module', 'icon-list2', 'Module', 13, 1)
--GO
--INSERT [dbo].[Module] ([Id], [Title], [link], [ClassName], [ControllerName], [ParentId], [Sort]) VALUES (15, N'Phòng ban', 'Department', 'icon-office', 'Department', 13, 2)
--GO
--SET IDENTITY_INSERT [dbo].[Module] OFF
--GO
--SET IDENTITY_INSERT [dbo].[Department] ON 
--GO
--INSERT [dbo].[Department] ([Id], [Name], [Note], [ParentId]) VALUES (1, N'Phòng hành chính', '', 1)
--GO
--INSERT [dbo].[Department] ([Id], [Name], [Note], [ParentId]) VALUES (2, N'Phòng kinh doanh', '', 2)
--GO
--INSERT [dbo].[Department] ([Id], [Name], [Note], [ParentId]) VALUES (3, N'Phòng Tài chính Kế toán', '', 3)
--GO
--INSERT [dbo].[Department] ([Id], [Name], [Note], [ParentId]) VALUES (4, N'Văn phòng đại diện', '', 4)
--GO
--INSERT [dbo].[Department] ([Id], [Name], [Note], [ParentId]) VALUES (5, N'Phòng Quản lý kỹ thuật - Công nghệ', '', 5)
--GO
--SET IDENTITY_INSERT [dbo].[Department] OFF
--GO

IF NOT EXISTS(SELECT TOP 1 1 FROM [dbo].[Role])
BEGIN
	INSERT INTO [dbo].[Role] VALUES('Administrator','Admin', '',0)
	INSERT INTO [dbo].[Role] VALUES('Manager','Manager', '', 0)
	INSERT INTO [dbo].[Role] VALUES('Editor','Editor', '', 0)
	INSERT INTO [dbo].[Role] VALUES('User','User', '', 1)
END
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
IF NOT EXISTS(SELECT TOP 1 1 FROM [dbo].[User])
BEGIN
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [FullName], [Avatar], [Type]) 
VALUES (1, N'admin@adwardsoft.com', N'ADMIN@ADWARDSOFT.COM', N'admin@adwardsoft.com', N'ADMIN@ADWARDSOFT.COM', 1, N'AQAAAAEAACcQAAAAEFBCo8KQXTV7oBu4T6cBsNFr4PmOCWmEU8Xv8q0EF1NrAYtWCZIwZGVNVIz4hNzVeg==', N'PLS66W5FHPMKUBOHSFHBGQT7IK7F7MKU', NULL, NULL, 0, 0, NULL, 1, 0, N'AdwardSoft', NULL, 1)

INSERT INTO [dbo].[UserRole] VALUES(1,1)
END
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO