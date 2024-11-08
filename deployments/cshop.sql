CREATE DATABASE Db;
GO

USE Db;
EXEC sys.sp_cdc_enable_db;


create table Categories
(
    Id          uniqueidentifier not null
        constraint PK_Categories
            primary key,
    Name        nvarchar(max),
    Description nvarchar(max),
    CreatedDate datetime2        not null,
    UpdatedDate datetime2
)
go

create table CustomerInfos
(
    Id          uniqueidentifier not null
        constraint PK_CustomerInfos
            primary key,
    CreatedDate datetime2        not null,
    UpdatedDate datetime2
)
go

create table OrderOutboxes
(
    Id            uniqueidentifier not null
        constraint PK_OrderOutboxes
            primary key,
    CreatedDate   datetime2        not null,
    UpdatedDate   datetime2,
    AggregateType nvarchar(max),
    AggregateId   nvarchar(max),
    Type          nvarchar(max),
    Payload       varbinary(max)
)
go

EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'OrderOutboxes', @role_name = NULL;
GO

create table Orders
(
    Id          uniqueidentifier not null
        constraint PK_Orders
            primary key,
    CustomerId  uniqueidentifier not null
        constraint FK_Orders_CustomerInfos_CustomerId
            references CustomerInfos
            on delete cascade,
    CreatedDate datetime2        not null,
    UpdatedDate datetime2
)
go

create index IX_Orders_CustomerId
    on Orders (CustomerId)
go

create table ProductInfos
(
    Id           uniqueidentifier not null
        constraint PK_ProductInfos
            primary key,
    ProductName  nvarchar(max),
    ProductPrice float            not null,
    CreatedDate  datetime2        not null,
    UpdatedDate  datetime2
)
go

create table OrderDetails
(
    Id            uniqueidentifier not null
        constraint PK_OrderDetails
            primary key,
    OrderId       uniqueidentifier not null
        constraint FK_OrderDetails_Orders_OrderId
            references Orders
            on delete cascade,
    ProductId     uniqueidentifier not null,
    Quantity      int              not null,
    ProductInfoId uniqueidentifier
        constraint FK_OrderDetails_ProductInfos_ProductInfoId
            references ProductInfos,
    CreatedDate   datetime2        not null,
    UpdatedDate   datetime2
)
go

create index IX_OrderDetails_OrderId
    on OrderDetails (OrderId)
go

create index IX_OrderDetails_ProductInfoId
    on OrderDetails (ProductInfoId)
go

create table ProductOutboxes
(
    Id            uniqueidentifier not null
        constraint PK_ProductOutboxes
            primary key,
    CreatedDate   datetime2        not null,
    UpdatedDate   datetime2,
    AggregateType nvarchar(max),
    AggregateId   nvarchar(max),
    Type          nvarchar(max),
    Payload       varbinary(max)
)
go

EXEC sys.sp_cdc_enable_table @source_schema = 'dbo', @source_name = 'ProductOutboxes', @role_name = NULL;
GO

create table Products
(
    Id          uniqueidentifier not null
        constraint PK_Products
            primary key,
    Name        nvarchar(max),
    Quantity    int              not null,
    Price       float            not null,
    ImageUrl    nvarchar(max),
    IsActive    bit              not null,
    CategoryId  uniqueidentifier
        constraint FK_Products_Categories_CategoryId
            references Categories,
    CreatedDate datetime2        not null,
    UpdatedDate datetime2,
    Version     bigint           not null
)
go

create index IX_Products_CategoryId
    on Products (CategoryId)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
        constraint PK___EFMigrationsHistory
            primary key,
    ProductVersion nvarchar(32)  not null
)
go

INSERT INTO CustomerInfos(Id, CreatedDate) VALUES ('efa99cdb-ac1f-4ef6-be49-1a1396016c6a', '2024-11-08 21:51:01.0000000')
