“"
l/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Swagger/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Swagger &
;& '
public 
static 
class 

Extensions 
{ 
public		 

static		 
IServiceCollection		 $
AddSwaggerCustom		% 5
(		5 6
this		6 :
IServiceCollection		; M
services		N V
,		V W
IConfiguration		X f
configuration		g t
,		t u
Action

 
<

 
IServiceCollection

 !
>

! "
?

" #
action

$ *
=

+ ,
null

- 1
)

1 2
{ 
services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
services 
. 
AddSwaggerGen 
( 
)  
;  !
services 
. 
AddApiVersioning !
(! "
options" )
=>* ,
{ 	
options 
. 
ReportApiVersions %
=& '
true( ,
;, -
options 
. 
DefaultApiVersion %
=& '
new( +

ApiVersion, 6
(6 7
$num7 8
)8 9
;9 :
} 	
)	 

. 	
AddApiExplorer	 
( 
options 
=>  "
{ 	
options 
. 
GroupNameFormat #
=$ %
$str& .
;. /
options 
. %
SubstituteApiVersionInUrl -
=. /
true0 4
;4 5
} 	
)	 

;
 
services 
. 
AddTransient 
< 
IConfigureOptions /
</ 0
SwaggerGenOptions0 A
>A B
,B C%
ConfigureSwaggerGenOptionD ]
>] ^
(^ _
)_ `
;` a
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
}   
public"" 

static"" 
WebApplication""  
ConfigureSwagger""! 1
(""1 2
this""2 6
WebApplication""7 E
app""F I
,""I J
IConfiguration""K Y
configuration""Z g
,""g h
Action""i o
<""o p
WebApplication""p ~
>""~ 
?	"" Ä
action
""Å á
=
""à â
null
""ä é
)
""é è
{## 
app%% 
.%% 

UseSwagger%% 
(%% 
)%% 
;%% 
app&& 
.&& 
UseSwaggerUI&& 
(&& 
option&& 
=>&&  "
{'' 	
foreach(( 
((( 
var(( !
apiVersionDescription(( .
in((/ 1
app((2 5
.((5 6
DescribeApiVersions((6 I
(((I J
)((J K
)((K L
{)) 
var** 
url** 
=** 
$"** 
$str** %
{**% &!
apiVersionDescription**& ;
.**; <
	GroupName**< E
}**E F
$str**F S
"**S T
;**T U
var++ 
name++ 
=++ 
$"++ 
$str++ 
{++ !
apiVersionDescription++ 4
.++4 5

ApiVersion++5 ?
}++? @
"++@ A
;++A B
option-- 
.-- 
SwaggerEndpoint-- &
(--& '
url--' *
,--* +
name--, 0
)--0 1
;--1 2
}.. 
}// 	
)//	 

;//
 
app11 
.11 
MapGet11 
(11 
$str11 
,11 
(11 
)11 
=>11 
Results11 %
.11% &
Redirect11& .
(11. /
$str11/ 9
)119 :
)11: ;
;11; <
action44 
?44 
.44 
Invoke44 
(44 
app44 
)44 
;44 
return55 
app55 
;55 
}66 
}77 ·
{/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Swagger/ConfigureSwaggerGenOption.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Swagger &
;& '
public		 
class		 %
ConfigureSwaggerGenOption		 &
:		' (
IConfigureOptions		) :
<		: ;
SwaggerGenOptions		; L
>		L M
{

 
private 
readonly *
IApiVersionDescriptionProvider 3
	_provider4 =
;= >
public 
%
ConfigureSwaggerGenOption $
($ %*
IApiVersionDescriptionProvider% C
providerD L
)L M
{ 
	_provider 
= 
provider 
; 
} 
public 

void 
	Configure 
( 
SwaggerGenOptions +
options, 3
)3 4
{ 
foreach 
( 
var )
providerApiVersionDescription 2
in3 5
	_provider6 ?
.? @"
ApiVersionDescriptions@ V
)V W
{ 	
var 
openApiInfo 
= 
new !
OpenApiInfo" -
(- .
). /
{ 
Title 
= )
providerApiVersionDescription 5
.5 6

ApiVersion6 @
.@ A
ToStringA I
(I J
)J K
,K L
Version 
= )
providerApiVersionDescription 7
.7 8

ApiVersion8 B
.B C
ToStringC K
(K L
)L M
,M N
} 
; 
options 
. 

SwaggerDoc 
( )
providerApiVersionDescription <
.< =
	GroupName= F
,F G
openApiInfoH S
)S T
;T U
} 	
options 
. 
CustomSchemaIds 
(  
type  $
=>% '
type( ,
., -
ToString- 5
(5 6
)6 7
.7 8
Replace8 ?
(? @
$str@ C
,C D
$strE H
)H I
)I J
;J K
} 
}## ﬁ
m/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Mediator/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Mediator '
;' (
public 
static 
class 

Extensions 
{ 
public 

static 
IServiceCollection $
AddMediatorDefault% 7
(7 8
this8 <
IServiceCollection= O
servicesP X
,X Y
TypeZ ^
[^ _
]_ `
typea e
,e f
Action 
< 
IServiceCollection !
>! "
?" #
action$ *
=+ ,
null- 1
)1 2
{ 
services		 
.		 

AddMediatR		 
(		 
e		 
=>		  
e		! "
.		" #*
RegisterServicesFromAssemblies		# A
(		A B
type		B F
.		F G
Select		G M
(		M N
t		N O
=>		P R
t		S T
.		T U
Assembly		U ]
)		] ^
.		^ _
ToArray		_ f
(		f g
)		g h
)		h i
)		i j
;		j k
action

 
?

 
.

 
Invoke

 
(

 
services

 
)

  
;

  !
return 
services 
; 
} 
} Ú
l/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Logging/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Logging &
;& '
public 
static 
class 

Extensions 
{ 
public 

static 
IServiceCollection $
AddLoggingCustom% 5
(5 6
this6 :
IServiceCollection; M
servicesN V
,V W
IConfigurationX f
configurationg t
,t u
stringv |
applicationName	} å
,
å ç
Action		 
<		 
IServiceCollection		 !
>		! "
?		" #
action		$ *
=		+ ,
null		- 1
)		1 2
{

 
var 
logger 
= 
new 
LoggerConfiguration ,
(, -
)- .
.. /
Enrich/ 5
.5 6
FromLogContext6 D
(D E
)E F
.F G
EnrichG M
.M N
WithPropertyN Z
(Z [
$str[ m
,m n
applicationNameo ~
)~ 
.	 Ä
WriteTo
Ä á
.
á à
Console
à è
(
è ê
)
ê ë
.
ë í
CreateLogger
í û
(
û ü
)
ü †
;
† °
Log 
. 
Logger 
= 
logger 
; 
services 
. 

AddSerilog 
( 
) 
; 
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
} °
~/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/IdentityServer/IClaimContextAccessor.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
IdentityServer -
;- .
public 
	interface !
IClaimContextAccessor &
{ 
Guid 
	GetUserId	 
( 
) 
; 
Guid 
GetUserMail	 
( 
) 
; 
} ò
}/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/IdentityServer/ClaimContextAccessor.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
IdentityServer -
;- .
public 
class  
ClaimContextAccessor !
:" #!
IClaimContextAccessor$ 9
{ 
private 
readonly  
IHttpContextAccessor ) 
_httpContextAccessor* >
;> ?
public		 
 
ClaimContextAccessor		 
(		   
IHttpContextAccessor		  4
httpContextAccessor		5 H
)		H I
{

  
_httpContextAccessor 
= 
httpContextAccessor 2
;2 3
} 
public 

Guid 
	GetUserId 
( 
) 
{ 
return 
Guid 
. 
Parse 
(  
_httpContextAccessor .
.. /
HttpContext/ :
?: ;
.; <
User< @
.@ A
FindFirstValueA O
(O P

ClaimTypesP Z
.Z [
NameIdentifier[ i
)i j
)j k
;k l
} 
public 

Guid 
GetUserMail 
( 
) 
{ 
throw 
new #
NotImplementedException )
() *
)* +
;+ ,
} 
} Î
d/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
; 
public 
static 
class 

Extensions 
{ 
public 

static 
IConfiguration  
GetConfiguration! 1
(1 2
Type2 6
anchor7 =
)= >
{ 
var 
configuration 
= 
new  
ConfigurationBuilder  4
(4 5
)5 6
.		 
SetBasePath		 
(		 
	Directory		 "
.		" #
GetCurrentDirectory		# 6
(		6 7
)		7 8
)		8 9
.

 
AddJsonFile

 
(

 
$str

 +
)

+ ,
. 
AddJsonFile 
( 
$str 7
)7 8
. 
Build 
( 
) 
; 
return 
configuration 
; 
} 
} „
x/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/StoreEventConverter.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
class 
StoreEventConverter  
(  !
)! "
:# $
ValueConverter% 3
<3 4
IDomainEvent4 @
?@ A
,A B
stringC I
>I J
(J K
@event 

=> 
JsonConvert 
. 
SerializeObject )
() *
@event* 0
,0 1
typeof2 8
(8 9
IDomainEvent9 E
)E F
,F G
new		 "
JsonSerializerSettings		 "
(		" #
)		# $
{		% &
TypeNameHandling		' 7
=		8 9
TypeNameHandling		: J
.		J K
Auto		K O
}		P Q
)		Q R
,		R S

jsonString

 
=>

 
JsonConvert

 
.

 
DeserializeObject

 /
<

/ 0
IDomainEvent

0 <
>

< =
(

= >

jsonString

> H
,

H I
new "
JsonSerializerSettings "
(" #
)# $
{% &
TypeNameHandling' 7
=8 9
TypeNameHandling: J
.J K
AutoK O
}P Q
)Q R
)R S
;S T 
z/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/IEventStoreRepository.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
	interface !
IEventStoreRepository &
{ 
public 

Task 
AppendEventAsync  
(  !

StoreEvent! +
@event, 2
,2 3
CancellationToken4 E
cancellationTokenF W
)W X
;X Y
public

 

Task

 
<

 
TEntity

 
>

 $
LoadAggregateEventsAsync

 1
<

1 2
TEntity

2 9
>

9 :
(

: ;
Guid

; ?
aggregateId

@ K
,

K L
CancellationToken

M ^
cancellationToken

_ p
)

p q
where

r w
TEntity

x 
:


Ä Å
AggregateBase


Ç è
,


è ê
new


ë î
(


î ï
)


ï ñ
;


ñ ó
} Û
o/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
static 
class 

Extensions 
{ 
public 

static 
IServiceCollection $"
AddEventStoreDbContext% ;
<; <

TDbContext< F
>F G
(G H
thisH L
IServiceCollectionM _
services` h
,h i
IConfigurationj x
configuration	y Ü
,
Ü á
Action 
< 
IServiceCollection !
>! "
?" #
action$ *
=+ ,
null- 1
)1 2
where		 

TDbContext		 
:		 #
EventStoreDbContextBase		 2
{

 
services 
. 
AddDbContext 
< 

TDbContext (
>( )
() *
(* +
provider+ 3
,3 4
builder5 <
)< =
=>> @
{ 	
builder 
. 
UseSqlServer  
(  !
configuration! .
.. /
GetConnectionString/ B
(B C
$strC O
)O P
,P Q

sqlOptionsR \
=>] _
{ 

sqlOptions 
.  
EnableRetryOnFailure /
(/ 0
)0 1
;1 2
} 
) 
; 
} 	
)	 

;
 
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
} …
}/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/EventStoreRepositoryBase.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
class $
EventStoreRepositoryBase %
<% &

TDbContext& 0
>0 1
:2 3!
IEventStoreRepository4 I
where 	

TDbContext
 
: #
EventStoreDbContextBase .
{		 
private

 
readonly

 

TDbContext

 
_context

  (
;

( )
public 
$
EventStoreRepositoryBase #
(# $

TDbContext$ .
context/ 6
)6 7
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
AppendEventAsync &
(& '

StoreEvent' 1
@event2 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 
await 
_context 
. 
Set 
< 

StoreEvent %
>% &
(& '
)' (
.( )
AddAsync) 1
(1 2
@event2 8
,8 9
cancellationToken: K
)K L
;L M
await 
_context 
. 
SaveChangesAsync '
(' (
cancellationToken( 9
)9 :
;: ;
} 
public 

async 
Task 
< 
TEntity 
> $
LoadAggregateEventsAsync 7
<7 8
TEntity8 ?
>? @
(@ A
GuidA E
aggregateIdF Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
where 
TEntity 
: 
AggregateBase %
,% &
new' *
(* +
)+ ,
{ 
var 

listEvents 
= 
await 
_context '
.' (
Set( +
<+ ,

StoreEvent, 6
>6 7
(7 8
)8 9
.9 :
Where: ?
(? @
e@ A
=>B D
eE F
.F G
AggregateIdG R
==S U
aggregateIdV a
)a b
.b c
ToListAsyncc n
(n o
cancellationToken	o Ä
:
Ä Å
cancellationToken
Ç ì
)
ì î
;
î ï
var 
	aggregate 
= 
new 
TEntity #
(# $
)$ %
;% &
	aggregate 
. 
ApplyEvents 
( 

listEvents (
.( )
Select) /
(/ 0
e0 1
=>2 4
e5 6
.6 7
Event7 <
)< =
.= >
ToList> D
(D E
)E F
)F G
;G H
return!! 
	aggregate!! 
;!! 
}"" 
}## ç
|/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/EventStoreDbContextBase.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
class #
EventStoreDbContextBase $
:% &
	DbContext' 0
{ 
public 
#
EventStoreDbContextBase "
(" #
DbContextOptions# 3
options4 ;
); <
:= >
base? C
(C D
optionsD K
)K L
{ 
}

 
} ∆
|/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/DesignTimeDbContextBase.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
class #
DesignTimeDbContextBase $
<$ %

TDbContext% /
>/ 0
:1 2'
IDesignTimeDbContextFactory3 N
<N O

TDbContextO Y
>Y Z
where 	

TDbContext
 
: #
EventStoreDbContextBase .
{ 
public 


TDbContext 
CreateDbContext %
(% &
string& ,
[, -
]- .
args/ 3
)3 4
{ 
var 
configuration 
= 
new  
ConfigurationBuilder  4
(4 5
)5 6
. 
SetBasePath 
( 
	AppDomain "
." #
CurrentDomain# 0
.0 1
BaseDirectory1 >
)> ?
. 
AddJsonFile 
( 
$str +
,+ ,
true- 1
)1 2
. 
AddJsonFile 
( 
$" 
$str '
{' (
Environment( 3
.3 4"
GetEnvironmentVariable4 J
(J K
$strK c
)c d
}d e
$stre j
"j k
,k l
truem q
)q r
. #
AddEnvironmentVariables $
($ %
)% &
. 
Build 
( 
) 
; 
var 
conn 
= 
configuration  
.  !
GetConnectionString! 4
(4 5
$str5 A
)A B
;B C
Console 
. 
	WriteLine 
( 
$str n
)n o
;o p
var 
options 
= 
new #
DbContextOptionsBuilder 1
<1 2

TDbContext2 <
>< =
(= >
)> ?
.? @
UseSqlServer@ L
(L M
$str	M °
,
° ¢
builder
£ ™
=>
´ ≠
{ 	
builder 
.  
EnableRetryOnFailure (
(( )
)) *
;* +
} 	
)	 

;
 
return 
( 

TDbContext 
) 
	Activator $
.$ %
CreateInstance% 3
(3 4
typeof4 :
(: ;

TDbContext; E
)E F
,F G
optionsH O
.O P
OptionsP W
)W X
;X Y
} 
} ∑
Ç/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/EventStore/DbContextMigrateHostedService.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

EventStore )
;) *
public 
class )
DbContextMigrateHostedService *
<* +

TDbContext+ 5
>5 6
:7 8
IHostedService9 G
where 	

TDbContext
 
: 
	DbContext  
{ 
private 
readonly 
IServiceProvider %
_serviceProvider& 6
;6 7
public

 
)
DbContextMigrateHostedService

 (
(

( )
IServiceProvider

) 9
serviceProvider

: I
)

I J
{ 
_serviceProvider 
= 
serviceProvider *
;* +
} 
public 

async 
Task 

StartAsync  
(  !
CancellationToken! 2
cancellationToken3 D
)D E
{ 
using 
var 
scope 
= 
_serviceProvider *
.* +
CreateScope+ 6
(6 7
)7 8
;8 9
var 
	dbContext 
= 
scope 
. 
ServiceProvider -
.- .
GetRequiredService. @
<@ A

TDbContextA K
>K L
(L M
)M N
;N O
if 

( 
	dbContext 
is 
not 
null !
)! "
{ 	
await 
	dbContext 
. 
Database $
.$ %
MigrateAsync% 1
(1 2
cancellationToken2 C
:C D
cancellationTokenE V
)V W
;W X
} 	
} 
public 

Task 
	StopAsync 
( 
CancellationToken +
cancellationToken, =
)= >
=>? A
TaskB F
.F G
CompletedTaskG T
;T U
} ∞
s/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Controller/BaseController.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 

Controller )
;) *
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
public 
class 
BaseController 
: 
ControllerBase ,
{		 
private

 
ISender

 
	_mediator

 
;

 
	protected 
ISender 
Mediator 
=> !
	_mediator" +
??=, /
HttpContext0 ;
.; <
RequestServices< K
.K L

GetServiceL V
<V W
ISenderW ^
>^ _
(_ `
)` a
;a b
} Ë
}/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Bus/Masstransit/MasstransitBusEvent.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Bus "
." #
Masstransit# .
;. /
public 
class 
MasstransitBusEvent  
(  ! 
IServiceScopeFactory! 5
serviceScopeFactory6 I
,I J
ILoggerK R
<R S
MasstransitBusEventS f
>f g
loggerh n
)n o
: 
	IBusEvent 
{ 
private		 
readonly		 
ILogger		 
<		 
MasstransitBusEvent		 0
>		0 1
_logger		2 9
=		: ;
logger		< B
;		B C
public 

async 
Task 
Publish 
< 
TMessage &
>& '
(' (
TMessage( 0
message1 8
,8 9
CancellationToken: K
cancellationTokenL ]
=^ _
default` g
)g h
wherei n
TMessageo w
:x y
Message	z Å
{ 
var 
producer 
= 
serviceScopeFactory *
.* +
CreateScope+ 6
(6 7
)7 8
.8 9
ServiceProvider9 H
.H I
GetRequiredServiceI [
<[ \
ITopicProducer\ j
<j k
TMessagek s
>s t
>t u
(u v
)v w
;w x
_logger 
. 
LogInformation 
( 
$str ?
,? @
messageA H
)H I
;I J
await 
producer 
. 
Produce 
( 
message &
,& '
cancellationToken( 9
)9 :
;: ;
} 
}  
g/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Bus/IBusEvent.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Bus "
;" #
public 
	interface 
	IBusEvent 
{ 
public		 

Task		 
Publish		 
<		 
TMessage		  
>		  !
(		! "
TMessage		" *
message		+ 2
,		2 3
CancellationToken		4 E
cancellationToken		F W
=		X Y
default		Z a
)		a b
where		c h
TMessage		i q
:		r s
Message		t {
;		{ |
}

 π	
h/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Bus/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Bus "
;" #
public 
static 
class 

Extensions 
{ 
public 

static 
IServiceCollection $
AddMessageBus% 2
(2 3
this3 7
IServiceCollection8 J
servicesK S
,S T
IConfigurationU c
configurationd q
,q r
Action		 
<		 
IServiceCollection		 !
>		! "
?		" #
action		$ *
=		+ ,
null		- 1
)		1 2
{

 
services 
. 
	AddScoped 
< 
	IBusEvent $
,$ %
MasstransitBusEvent& 9
>9 :
(: ;
); <
;< =
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
} §"
i/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure/Auth/Extensions.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
Auth #
;# $
public 
static 
class 

Extensions 
{ 
private 
const 
string 
Cors 
= 
$str  &
;& '
public

 

static

 
IServiceCollection

 $$
AddAuthenticationDefault

% =
(

= >
this

> B
IServiceCollection

C U
services

V ^
,

^ _
IConfiguration

` n
configuration

o |
,

| }
Action 
< 
IServiceCollection !
>! "
?" #
action$ *
=+ ,
null- 1
)1 2
{ 
services 
. "
AddHttpContextAccessor '
(' (
)( )
;) *
services 
. 
	AddScoped 
< !
IClaimContextAccessor 0
,0 1 
ClaimContextAccessor2 F
>F G
(G H
)H I
;I J
services 
. 
AddCors 
( 
options  
=>! #
{ 	
options 
. 
	AddPolicy 
( 
Cors "
," #
builder$ +
=>, .
{ 
builder 
. 
AllowAnyOrigin &
(& '
)' (
.( )
AllowAnyHeader) 7
(7 8
)8 9
.9 :
AllowAnyMethod: H
(H I
)I J
;J K
} 
) 
; 
} 	
)	 

;
 
services 
. 
AddAuthentication "
(" #
)# $
.$ %
AddJwtBearer% 1
(1 2
JwtBearerDefaults2 C
.C D 
AuthenticationSchemeD X
,X Y
optionsZ a
=>b d
{ 	
options 
. 
	Authority 
= 
configuration  -
.- .
GetValue. 6
<6 7
string7 =
>= >
(> ?
$str? S
)S T
??U W
throwX ]
new^ a
	Exceptionb k
(k l
$str	l å
)
å ç
;
ç é
options 
.  
RequireHttpsMetadata (
=) *
false+ 0
;0 1
options 
. %
TokenValidationParameters -
.- .
ValidateIssuer. <
== >
false? D
;D E
options 
. %
TokenValidationParameters -
.- .
ValidateAudience. >
=? @
falseA F
;F G
} 	
)	 

;
 
services 
. 
AddAuthorization !
(! "
)" #
;# $
action!! 
?!! 
.!! 
Invoke!! 
(!! 
services!! 
)!!  
;!!  !
return"" 
services"" 
;"" 
}## 
public&& 

static&& 
WebApplication&&  $
UseAuthenticationDefault&&! 9
(&&9 :
this&&: >
WebApplication&&? M
app&&N Q
,&&Q R
IConfiguration&&S a
configuration&&b o
,&&o p
Action&&q w
<&&w x
WebApplication	&&x Ü
>
&&Ü á
?
&&á à
action
&&â è
=
&&ê ë
null
&&í ñ
)
&&ñ ó
{'' 
app)) 
.)) 
UseCors)) 
()) 
Cors)) 
))) 
;)) 
app++ 
.++ 
UseAuthentication++ 
(++ 
)++ 
;++  
app,, 
.,, 
UseAuthorization,, 
(,, 
),, 
;,, 
action.. 
?.. 
... 
Invoke.. 
(.. 
app.. 
).. 
;.. 
return00 
app00 
;00 
}11 
}22 