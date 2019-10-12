
USE [Okaylah_Demo]
GO

SET IDENTITY_INSERT [dbo].[Modules] ON
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (1, N'Dashboard', N'/Home/Dashboard', 1, 0, N'icon-home4', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (2, N'Thành phố', N'/Cities/Index', 1, 0, N'icon-map5', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (3, N'Địa điểm', N'/Places/Index', 1, 0, N'icon-office', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (4, N'Món ăn', N' ', 1, 0, N'icon-list-unordered', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (5, N'Loại món ăn', N'/FoodCategories/Index', 1, 0, N'icon-home4', 4, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (6, N'Danh sách món ăn', N'/Foods/Index', 1, 0, N'icon-home4', 4, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (7, N'Đặc sản mang về', N' ', 1, 0, N'icon-gift', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (8, N'Loại đặc sản', N'/SpecialtyCategories/Index', 1, 0, N'icon-home4', 7, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (9, N'Danh sách đặc sản', N'/Specialties/Index', 1, 0, N'icon-home4', 7, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (10, N'Chia sẻ kinh nghiệm', N'/ShareExperiences/Index', 1, 0, N'icon-thumbs-up2', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (11, N'Yêu thích', N'/Favourites/Index', 1, 0, N'icon-heart5', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (12, N'Quản lý thành viên', N'/Users/Index', 1, 0, N'icon-people', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (13, N'Quản lý bình luận', N' ', 1, 0, N'icon-comments', 0, 0, 0)
GO

INSERT [dbo].[Modules] ([Id], [Title], [Link], [Sort], [Type], [ClassName], [ParentId], [ParentSort], [IsInactive]) VALUES (14, N'Địa điểm', N'/PlaceComments/Index', 1, 0, N'icon-home4', 13, 0, 0)
GO

SET IDENTITY_INSERT [dbo].[Modules] OFF
GO
