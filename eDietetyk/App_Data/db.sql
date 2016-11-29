drop table dbo.UsersInfo
drop table dbo.Metrics
drop table dbo.UserMeals
drop table dbo.Meals
drop table dbo.Diets

GO

create table dbo.UsersInfo
(
	Id nvarchar(128) not null,
	FirstName nvarchar(50)not null,
	LastName nvarchar(50) not null,
	BirthDate datetime not null,
	constraint [PK@Users@Id] primary key(id),
	constraint [FK@Users@Id@AspNetUsers@Id] foreign key(Id) references dbo.AspNetUsers (Id) 
)

GO

create table dbo.Metrics
(
	Id int identity not null,
	IdUser nvarchar(128) not null,
	Height int not null,
	Weight numeric(3,2) not null,
	Chest int null,
	Waist int null,
	Thigh int null,
	Arm int null,
	IsTarget bit not null constraint [DK@Metrics@IsTarget] default  0,
	IsActive bit not null constraint [DK@Metrics@IsActive] default  0,
	CreateDate DateTime not null constraint [DK@Metrics@CreateDate] default  getdate(),
	constraint [PK@Metrics@Id] primary key (id),
	constraint [FK@Metrics@Id@AspNetUsers@Id] foreign key(IdUser) references dbo.AspNetUsers (Id)
)

GO

create table dbo.Meals
(
	Id int identity(1,1) not null,
	Name nvarchar(100) not null,
	Calories int not null,
	Description nvarchar(100) null,
	CreateDate datetime not null constraint [DK@Meals@CreateDate] default  getdate(),
	constraint [PK@Meals@Id] primary key (Id)
)

GO

create table dbo.UserMeals
(
	Id int identity(1,1) not null,
	IdUser nvarchar(128) not null,
	IdMeal int not null,
	Weight int not null,
	CreateDate datetime not null constraint [DK@UserMeals@CreateDate] default  getdate(),
	constraint [PK@UserMeals@Id] primary key (Id),
	constraint [FK@UserMeals@Id@AspNetUsers@Id] foreign key(IdUser) references dbo.AspNetUsers (Id),
	constraint [FK@UserMeals@Id@Meals@Id] foreign key(IdMeal) references dbo.Meals (Id),
)

GO

create table dbo.Diets
(
	Id int identity(1,1) not null,
	IdUser nvarchar(128) not null,
	CaloriesPerDay int not null,
	CreateDate datetime not null constraint [DK@Diets@CreateDate] default  getdate(),
	constraint [PK@Diets@Id] primary key (Id),
	constraint [FK@Diets@Id@AspNetUsers@Id] foreign key(IdUser) references dbo.AspNetUsers (Id)
)