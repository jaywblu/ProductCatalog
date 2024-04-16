DROP TABLE Inventories
DROP TABLE Stores
DROP TABLE ProductProperties
DROP TABLE Properties
DROP TABLE Products
DROP TABLE Categories

CREATE TABLE Categories (
	Id int identity not null primary key,
	CategoryName nvarchar(50) not null unique
)

CREATE TABLE Products (
	Id int identity not null primary key,
	Title nvarchar(100) not null,
	ProductDescription nvarchar(max) null,
	Price money not null default(0),
	CategoryId int not null references Categories(Id)
)

CREATE TABLE Properties (
	Id int identity not null primary key,
	Title nvarchar(100) not null,
	PropertyValue nvarchar(100) not null,
)

CREATE TABLE ProductProperties (
	ProductId int not null references Products(Id),
	PropertyId int not null references Properties(Id),

	primary key (ProductId, PropertyId)
)

CREATE TABLE Stores (
	Id int identity not null primary key,
	StoreName nvarchar(100) not null
)

CREATE TABLE Inventories (
	StoreId int not null references Stores(Id),
	ProductId int not null references Products(Id),
	Amount int not null default(0),

	primary key (StoreId, ProductId)
)