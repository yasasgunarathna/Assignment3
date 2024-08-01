-- Insert data into Brands
SET IDENTITY_INSERT [dbo].[Brands] ON;
INSERT INTO [dbo].[Brands] ([Id], [Name], [Description], [PhotoPath]) VALUES 
(1, 'Jordan', NULL, 'jordan.png'),
(2, 'Adidas', NULL, 'adidas.png'),
(3, 'Nike', NULL, 'nike.png'),
(4, 'Supreme', NULL, 'supreme.png');
SET IDENTITY_INSERT [dbo].[Brands] OFF;

-- Insert data into Categories
SET IDENTITY_INSERT [dbo].[Categories] ON;
INSERT INTO [dbo].[Categories] ([Id], [Name]) VALUES 
(1, 'Footwear'),
(2, 'Clothing');
SET IDENTITY_INSERT [dbo].[Categories] OFF;

-- Insert data into Colors
SET IDENTITY_INSERT [dbo].[Colors] ON;
INSERT INTO [dbo].[Colors] ([Id], [Name]) VALUES 
(1, 'White'),
(2, 'Black'),
(3, 'Blue'),
(4, 'Yellow'),
(5, 'Gray'),
(6, 'Red'),
(7, 'Other'),
(8, 'Orange');
SET IDENTITY_INSERT [dbo].[Colors] OFF;

-- Insert data into Genders
SET IDENTITY_INSERT [dbo].[Genders] ON;
INSERT INTO [dbo].[Genders] ([Id], [Name]) VALUES 
(1, 'Male'),
(2, 'Female'),
(3, 'Unknown');
SET IDENTITY_INSERT [dbo].[Genders] OFF;

-- Insert data into OrderProducts
INSERT INTO [dbo].[OrderProducts] ([ProductId], [OrderId], [StockId], [Quantity]) VALUES 
(1, 14, 1, 1),
(1, 15, 1, 1),
(1, 16, 1, 1),
(1, 15, 2, 1),
(1, 16, 2, 1),
(1, 17, 3, 1),
(2, 14, 8, 2),
(2, 17, 8, 1),
(3, 17, 23, 1);

-- Insert data into Orders
SET IDENTITY_INSERT [dbo].[Orders] ON;
INSERT INTO [dbo].[Orders] ([Id], [Address1], [Address2], [City], [PostCode], [FirstName], [LastName], [Email], [UserId], [IsSent], [OrderDate], [OrderSentDate]) VALUES 
(1, 'admin', NULL, 'admin', '51-627', 'admin', 'admin', 'admin@store.com', 1, 1, CAST('2020-04-15T16:38:01.9548109' AS DateTime2), CAST('2020-04-15T16:38:12.0586184' AS DateTime2)),
(2, 'user', NULL, 'user', '51-627', 'user', 'user', 'user@store.com', 2, 1, CAST('2020-04-15T17:12:22.4463558' AS DateTime2), CAST('2020-04-15T17:13:38.6560085' AS DateTime2)),
(4, 'Wroblewskiego 25', NULL, 'Wroclaw', '51-627', 'Jakub', 'Pietrus', 'pietrusjakub@gmail.com', 3, 0, CAST('2020-04-15T17:16:36.5648958' AS DateTime2), NULL),
(5, 'Wroblewskiego 25', NULL, 'Wroclaw', '51-627', 'Jakub', 'Pietrus', 'pietrusjakub@gmail.com', 3, 1, CAST('2020-04-15T17:16:47.4630023' AS DateTime2), CAST('2020-04-15T17:17:16.8337613' AS DateTime2)),
(6, 'Wroblewskiego 25', NULL, 'Wroclaw', '51-627', 'Jakub', 'Pietrus', 'pietrusjakub@gmail.com', 3, 0, CAST('2020-04-15T17:17:00.5815675' AS DateTime2), NULL),
(7, 'Kazimierz', 'Wielki', 'Dalachow', '43-324', 'kaziu', 'mraziu', 'kazik@gmail.com', 4, 1, CAST('2020-04-15T17:18:16.3270813' AS DateTime2), CAST('2020-04-15T17:18:47.6499901' AS DateTime2)),
(8, 'Kazimierz', 'Wielki', 'Dalachow', '43-324', 'kaziu', 'mraziu', 'kazik@gmail.com', 4, 1, CAST('2020-04-15T17:18:25.3155505' AS DateTime2), CAST('2020-04-16T20:51:13.4397049' AS DateTime2)),
(14, 'admin', NULL, 'admin', '51-627', 'admin', 'admin', 'admin@store.com', 1, 0, CAST('2020-04-16T20:37:55.8244172' AS DateTime2), NULL),
(15, 'admin', NULL, 'admin', '51-627', 'admin', 'admin', 'admin@store.com', 1, 0, CAST('2020-04-16T20:47:40.3941221' AS DateTime2), NULL),
(16, 'admin', NULL, 'admin', '51-627', 'admin', 'admin', 'admin@store.com', 1, 0, CAST('2020-04-16T20:49:00.2840352' AS DateTime2), NULL),
(17, 'admin', NULL, 'admin', '51-627', 'admin', 'admin', 'admin@store.com', 1, 0, CAST('2020-04-16T23:28:10.8349375' AS DateTime2), NULL);
SET IDENTITY_INSERT [dbo].[Orders] OFF;

-- Insert data into Products
SET IDENTITY_INSERT [dbo].[Products] ON;
INSERT INTO [dbo].[Products] ([Id], [Name], [PhotoPath], [Price], [ColorId], [BrandId], [GenderId], [Description], [CategoryId]) VALUES 
(1, 'Air Max 97', 'nikeairmax97.png', CAST(399.99 AS DECIMAL(15, 2)), 2, 3, 3, 'Shoe from 97!', 1),
(2, 'Cortez', 'nikecortez.png', CAST(199.99 AS DECIMAL(15, 2)), 1, 3, 1, 'Classic worn by Forrest Gump!', 1),
(3, '30', 'jordan30.png', CAST(599.99 AS DECIMAL(15, 2)), 6, 1, 2, 'Another model from the best basketball player in history!', 1),
(4, 'Bogo Red', 'bogored.png', CAST(999.99 AS DECIMAL(15, 2)), 6, 4, 1, 'Most popular model of Supreme hoodie!', 2),
(5, 'Buju Banton Tee', 'bujubanton.png', CAST(599.99 AS DECIMAL(15, 2)), 1, 4, 1, 'Great T-Shirt from Supreme!', 2),
(6, 'Camp Cap', 'supremecapblack.png', CAST(199.99 AS DECIMAL(15, 2)), 1, 4, 3, 'Black cap from Supreme!', 2),
(7, 'Neo White', 'adidasneowhite.png', CAST(299.99 AS DECIMAL(15, 2)), 1, 2, 2, 'White sneakers from Adidas!', 1),
(8, 'MARS 270', '61ae7916-88e6-4c15-998b-7eefa8a579e5_mars270.jpg', CAST(300.00 AS DECIMAL(15, 2)), 2, 1, 1, NULL, 1),
(9, 'JUMPMAN DIAMOND MID', '1eab9c3a-47f9-4731-91fd-f7ebdb1ff967_JUMPMAN-DIAMOND-MID.jpg', CAST(398.00 AS DECIMAL(15, 2)), 2, 1, 1, NULL, 1),
(10, 'AURA', '400527d9-9573-418c-bc1e-ef90bd02c56b_AURA.png', CAST(369.00 AS DECIMAL(15, 2)), 1, 1, 1, NULL, 1),
(11, 'JUMPMAN FLEECE', 'b10993e6-5448-4262-b634-b179d4116b3c_jordanbluza.png', CAST(219.00 AS DECIMAL(15, 2)), 2, 1, 1, NULL, 2),
(12, 'STEFAN JANOSKI', '601fd970-37f2-4f57-a277-99bc0890d457_original.jpg', CAST(249.00 AS DECIMAL(15, 2)), 2, 3, 1, NULL, 1),
(13, 'Motion Logo Hooded', '56d8764d-037d-40dc-90a5-7b40544192f5_motionsup.jpg', CAST(800.00 AS DECIMAL(15, 2)), 4, 4, 1, NULL, 2),
(14, 'Sailboat', '78ca064d-565d-4d58-8013-9066f85fcbf8_sailboat.jpg', CAST(450.00 AS DECIMAL(15, 2)), 2, 4, 3, NULL, 2),
(15, 'Crossover Hooded', '7b84bd78-93f4-4330-9b4e-6f0cc86c4e62_crossover.jpg', CAST(730.00 AS DECIMAL(15, 2)), 1, 4, 3, NULL, 2),
(16, 'Sportswear Club Fleece', '829a8940-c439-42fb-85d9-c71b91a73546_joggery-meskie-sportswear-club-fleece-phSWZS.jpg', CAST(300.00 AS DECIMAL(15, 2)), 2, 3, 1, NULL, 2),
(17, 'Sportswear Club Fleece', 'c346797d-406d-40b8-9835-26ffd744dcc5_joggery-meskie-sportswear-club-fleece-phSWZS (1).jpg', CAST(350.00 AS DECIMAL(15, 2)), 8, 1, 1, NULL, 1);
SET IDENTITY_INSERT [dbo].[Products] OFF;

-- Insert data into Roles
SET IDENTITY_INSERT [dbo].[Roles] ON;
INSERT INTO [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES 
(1, 'Admin', 'ADMIN', '440cdcb3-fa68-4302-bd7d-d2daf999ef95'),
(2, 'User', 'USER', 'dafd95da-5783-41c1-b730-a74137b2bb93');
SET IDENTITY_INSERT [dbo].[Roles] OFF;

-- Insert data into Stock
SET IDENTITY_INSERT [dbo].[Stock] ON;
INSERT INTO [dbo].[Stock] ([Id], [Quantity], [ProductId], [Name], [IsLastElementOrdered]) VALUES 
(1, 0, 1, '10', 1),
(2, 0, 1, '11', 1),
(3, 0, 1, '12', 1),
(4, 1, 4, 'S', 0),
(5, 2, 4, 'L', 0),
(6, 0, 4, 'XL', 1),
(7, 1, 7, '9', 0),
(8, 2, 2, '11', 0),
(9, 0, 13, 'XL', 1),
(10, 2, 13, 'L', 0),
(11, 1, 13, 'S', 0),
(12, 3, 12, '9', 0),
(13, 2, 12, '10', 0),
(14, 3, 12, '11', 0),
(15, 5, 12, '12', 0),
(16, 6, 12, '13', 0),
(17, 12, 11, 'M', 0),
(18, 5, 11, 'L', 0),
(19, 5, 11, 'S', 0),
(20, 1, 8, '12', 0),
(21, 3, 8, '13', 0),
(22, 12, 6, 'L', 0),
(23, 3, 3, '10', 0),
(24, 1, 9, '13', 0),
(25, 2, 9, '14', 0),
(26, 1, 10, '11', 0),
(27, 4, 10, '13', 0),
(28, 11, 15, 'L', 0),
(29, 10, 15, 'XL', 0),
(30, 28, 15, 'XXL', 0),
(31, 12, 16, 'L', 0),
(32, 5, 16, 'XL', 0),
(33, 9, 17, 'L', 0),
(34, 2, 17, 'XL', 0);
SET IDENTITY_INSERT [dbo].[Stock] OFF;

-- Insert data into UserRoles
INSERT INTO [dbo].[UserRoles] ([UserId], [RoleId]) VALUES 
(1, 1),
(2, 2),
(3, 2),
(4, 2);

-- Insert data into Users
SET IDENTITY_INSERT [dbo].[Users] ON;
INSERT INTO [dbo].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [GenderId], [PhoneNumber], [PhotoPath], [Address1], [Address2], [City], [PostCode]) VALUES 
(1, 'admin', 'ADMIN', 'admin@store.com', 'ADMIN@STORE.COM', 1, 'AQAAAAEAACcQAAAAEPoX0cLbm3yVlhqmRSc19zzd4Uh6/yOjF5r7yXckASqahFSE9533au5xtwl2aK3pHw==', '662d9587-7baf-43a8-9765-39450e7a1af2', '78b8c1e0-da8c-4213-86cb-e6d2778cbae0', 0, 0, NULL, 0, 0, 'admin', 'admin', 3, '123456789', NULL, 'admin', NULL, 'admin', '51-627'),
(2, 'user', 'USER', 'user@store.com', 'USER@STORE.COM', 1, 'AQAAAAEAACcQAAAAEOdnw4rzIrEj76iU5UKYsnAYmD1ii4m6vDjL0kndN0MqXmFHqM9AkhNDrko6gzxKWQ==', '2f9aca15-40a7-4374-9f2e-15b9727d9388', 'd991460e-627b-4cd9-b014-256440b6b5aa', 0, 0, NULL, 0, 0, 'user', 'user', 1, '987654321', NULL, 'user', NULL, 'user', '51-627'),
(3, 'kubix', 'KUBIX', 'pietrusjakub@gmail.com', 'PIETRUSJAKUB@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEALc+HAA379nxQ3gUoTlnMznBNS4Qm2dQl1TQ5kWCUlJSOFilEkY3tHMQFNJL/mrHA==', 'GOH3JQ4XM7H6AQBGBJOLGRAAUAPZ736T', '12a6dec4-8dbb-47c8-b5c9-25f247d8b9a6', 0, 0, NULL, 1, 0, 'Jakub', 'Pietrus', 1, '883198611', 'aa1637b5-b3bd-4337-b1c0-9fe9f1b645da_sasuke-avatar.jpg', 'Wroblewskiego 25', NULL, 'Wroclaw', '51-627'),
(4, 'kazik', 'KAZIK', 'kazik@gmail.com', 'KAZIK@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEMdp98cfy/V2DUM7x5xzN0m9jKTXwqFsxavVcd9Uclxs2g/ibEKfi2AVRXnIekwc8g==', 'ANUMCAGDPB547GBR4VS5UFT7ATLLNNNY', '0ad7f475-166d-420c-87ac-d10bd6b03649', 0, 0, NULL, 1, 0, 'kaziu', 'mraziu', 1, NULL, NULL, 'Kazimierz', 'Wielki', 'Dalachow', '43-324');
SET IDENTITY_INSERT [dbo].[Users] OFF;