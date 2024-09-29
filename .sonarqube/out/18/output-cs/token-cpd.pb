“
{/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/Migrations/20240925073137_InitDb.cs
	namespace 	

EventStore
 
. 

Migrations 
{ 
public		 

partial		 
class		 
InitDb		 
:		  !
	Migration		" +
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str "
," #
columns 
: 
table 
=> !
new" %
{ 
AggregateId 
=  !
table" '
.' (
Column( .
<. /
Guid/ 3
>3 4
(4 5
type5 9
:9 :
$str; M
,M N
nullableO W
:W X
falseY ^
)^ _
,_ `
Version 
= 
table #
.# $
Column$ *
<* +
long+ /
>/ 0
(0 1
type1 5
:5 6
$str7 ?
,? @
nullableA I
:I J
falseK P
)P Q
,Q R
AggregateType !
=" #
table$ )
.) *
Column* 0
<0 1
string1 7
>7 8
(8 9
type9 =
:= >
$str? N
,N O
nullableP X
:X Y
falseZ _
)_ `
,` a
	EventType 
= 
table  %
.% &
Column& ,
<, -
string- 3
>3 4
(4 5
type5 9
:9 :
$str; J
,J K
nullableL T
:T U
falseV [
)[ \
,\ ]
Event 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 F
,F G
nullableH P
:P Q
falseR W
)W X
,X Y
	CreatedAt 
= 
table  %
.% &
Column& ,
<, -
DateTime- 5
>5 6
(6 7
type7 ;
:; <
$str= H
,H I
nullableJ R
:R S
falseT Y
)Y Z
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 4
,4 5
x6 7
=>8 :
new; >
{? @
xA B
.B C
AggregateIdC N
,N O
xP Q
.Q R
VersionR Y
}Z [
)[ \
;\ ]
} 
) 
; 
} 	
	protected   
override   
void   
Down    $
(  $ %
MigrationBuilder  % 5
migrationBuilder  6 F
)  F G
{!! 	
migrationBuilder"" 
."" 
	DropTable"" &
(""& '
name## 
:## 
$str## "
)##" #
;### $
}$$ 	
}%% 
}&& ¬
e/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/Extensions.cs
	namespace 	

EventStore
 
; 
public 
static 
class 

Extensions 
{ 
public		 

static		 
IServiceCollection		 $
AddEventStore		% 2
(		2 3
this		3 7
IServiceCollection		8 J
services		K S
,		S T
IConfiguration		U c
configuration		d q
,		q r
Action		s y
<		y z
IServiceCollection			z å
>
		å ç
?
		ç é
action
		è ï
=
		ñ ó
null
		ò ú
)
		ú ù
{

 
services 
. "
AddEventStoreDbContext #
<# $&
CatalogEventStoreDbContext$ >
>> ?
(? @
configuration@ M
)M N
. 
AddHostedService 
< )
DbContextMigrateHostedService ;
<; <&
CatalogEventStoreDbContext< V
>V W
>W X
(X Y
)Y Z
;Z [
services 
. 
	AddScoped 
< !
IEventStoreRepository 0
,0 1'
CatalogEventStoreRepository2 M
>M N
(N O
)O P
;P Q
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
} –
Å/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/Configurations/EventStoreConfiguration.cs
	namespace 	

EventStore
 
. 
Configurations #
;# $
public		 
class		 #
EventStoreConfiguration		 $
:		% &$
IEntityTypeConfiguration		' ?
<		? @

StoreEvent		@ J
>		J K
{

 
public 

void 
	Configure 
( 
EntityTypeBuilder +
<+ ,

StoreEvent, 6
>6 7
builder8 ?
)? @
{ 
builder 
. 
HasKey 
( 
e 
=> 
new 
{  !
e" #
.# $
AggregateId$ /
,/ 0
e1 2
.2 3
Version3 :
}; <
)< =
;= >
builder 
. 
Property 
( 
e 
=> 
e 
.  
AggregateId  +
)+ ,
., -

IsRequired- 7
(7 8
)8 9
;9 :
builder 
. 
Property 
( 
e 
=> 
e 
.  
AggregateType  -
)- .
.. /

IsRequired/ 9
(9 :
): ;
;; <
builder 
. 
Property 
( 
e 
=> 
e 
.  
	EventType  )
)) *
.* +

IsRequired+ 5
(5 6
)6 7
;7 8
builder 
. 
Property 
( 
e 
=> 
e 
. 
Version 
) 
. 

IsRequired 
( 
) 
; 
builder 
. 
Property 
( 
e 
=> 
e 
.  
	CreatedAt  )
)) *
.* +

IsRequired+ 5
(5 6
)6 7
;7 8
builder 
. 
Property 
( 
e 
=> 
e 
.  
Event  %
)% &
.& '
HasConversion' 4
<4 5
StoreEventConverter5 H
>H I
(I J
) 	
.	 


IsRequired
 
( 
) 
; 
} 
} ö
v/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/CatalogEventStoreRepository.cs
	namespace 	

EventStore
 
; 
public 
class '
CatalogEventStoreRepository (
(( )&
CatalogEventStoreDbContext) C
contextD K
)K L
: $
EventStoreRepositoryBase 
< &
CatalogEventStoreDbContext 9
>9 :
(: ;
context; B
)B C
;C D•
u/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/CatalogEventStoreDbContext.cs
	namespace 	

EventStore
 
; 
public 
class &
CatalogEventStoreDbContext '
(' (
DbContextOptions( 8
options9 @
)@ A
: #
EventStoreDbContextBase 
( 
options %
)% &
{ 
	protected		 
override		 
void		 
OnModelCreating		 +
(		+ ,
ModelBuilder		, 8
modelBuilder		9 E
)		E F
{

 
base 
. 
OnModelCreating 
( 
modelBuilder )
)) *
;* +
modelBuilder 
. +
ApplyConfigurationsFromAssembly 4
(4 5
typeof5 ;
(; <&
CatalogEventStoreDbContext< V
)V W
.W X
AssemblyX `
)` a
;a b
} 
} î
u/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/EventStore/CatalogDesignTimeDbContext.cs
	namespace 	

EventStore
 
; 
public 
class &
CatalogDesignTimeDbContext '
:( )#
DesignTimeDbContextBase* A
<A B&
CatalogEventStoreDbContextB \
>\ ]
{ 
} 