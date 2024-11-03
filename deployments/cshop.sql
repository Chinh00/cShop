USE Db;
GO

EXEC sys.sp_cdc_enable_db;
GO


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
    CategoryId  uniqueidentifier not null
        constraint FK_Products_Categories_CategoryId
            references Categories
            on delete cascade,
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

