create table [dbo].[Fruit] (
	[Id] int PRIMARY KEY IDENTITY,
	[Name] varchar(255) NOT NULL,
	[Color] varchar(255) NOT NULL,
	[Size] varchar(255) NOT NULL)
GO

insert into [dbo].[Fruit]
	([Name], [Color], [Size])
values
	('Apple', 'Red', 'MEDIUM'),
	('Banana', 'Yellow', 'MEDIUM'),
	('Orange', 'Orange', 'MEDIUM'),
	('Grape', 'Purple', 'SMALL'),
	('Watermelon', 'Green', 'LARGE')
GO
