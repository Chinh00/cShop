þ
v/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Application/UseCases/Commands/Commands.cs
	namespace 	
Application
 
. 
UseCases 
. 
Commands '
;' (
public 
record 
Commands 
{		 
public

 

record

 
CreateCatalog

 
(

  
string

  &
Name

' +
,

+ ,
float

- 2
CurrentCost

3 >
,

> ?
string

@ F
ImageSrc

G O
,

O P
string

Q W

CategoryId

X b
)

b c
:

d e
IRequest

f n
<

n o
Guid

o s
>

s t
{ 
} 
} Ú
}/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Application/UseCases/Commands/CommandHandlers.cs
	namespace 	
Application
 
. 
UseCases 
. 
Commands '
;' (
public

 
class

 
CommandHandlers

 
:

 
IRequestHandler

 .
<

. /
Commands

/ 7
.

7 8
CreateCatalog

8 E
,

E F
Guid

G K
>

K L
{ 
private 
readonly 
ILogger 
< 
CommandHandlers ,
>, -
_logger. 5
;5 6
private 
readonly !
IEventStoreRepository *!
_eventStoreRepository+ @
;@ A
private 
readonly 
	IBusEvent 
	_busEvent (
;( )
public 

CommandHandlers 
( 
ILogger "
<" #
CommandHandlers# 2
>2 3
logger4 :
,: ;!
IEventStoreRepository< Q 
eventStoreRepositoryR f
,f g
	IBusEventh q
busEventr z
)z {
{ 
_logger 
= 
logger 
; !
_eventStoreRepository 
=  
eventStoreRepository  4
;4 5
	_busEvent 
= 
busEvent 
; 
} 
public 

async 
Task 
< 
Guid 
> 
Handle "
(" #
Commands# +
.+ ,
CreateCatalog, 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 
Product 
product 
= 
new 
( 
) 
;  
foreach 
( 
var 
productDomainEvent '
in( *
product+ 2
.2 3
DomainEvents3 ?
)? @
{ 	
await 
	_busEvent 
. 
Publish #
(# $
($ %
dynamic% ,
), -
productDomainEvent- ?
,? @
cancellationTokenA R
)R S
;S T
await !
_eventStoreRepository '
.' (
AppendEventAsync( 8
(8 9

StoreEvent9 C
.C D
CreateD J
(J K
productK R
,R S
productDomainEventT f
)f g
,g h
cancellationToken !
)! "
;" #
}   	
return"" 
Guid"" 
."" 
NewGuid"" 
("" 
)"" 
;"" 
}## 
}$$  
c/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Application/Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder

 
.

 
Services

 
.

 
AddCors

 
(

 
options

  
=>

! #
{ 
options 
. 
	AddPolicy 
( 
$str 
, 
policyBuilder +
=>, .
{ 
policyBuilder 
. 
AllowAnyHeader $
($ %
)% &
.& '
AllowAnyOrigin' 5
(5 6
)6 7
.7 8
AllowAnyMethod8 F
(F G
)G H
;H I
} 
) 
; 
} 
) 
; 
builder 
. 
Services 
. 
AddLoggingCustom 
( 
builder 
. 
Configuration +
,+ ,
$str- @
)@ A
. $
AddAuthenticationDefault 
( 
builder %
.% &
Configuration& 3
)3 4
. 
AddEventStore 
( 
builder 
. 
Configuration (
)( )
.  
AddCustomMasstransit 
( 
builder !
.! "
Configuration" /
)/ 0
. 

AddMediatR 
( 
e 
=> 
e 
. (
RegisterServicesFromAssembly 3
(3 4
typeof4 :
(: ;
Program; B
)B C
.C D
AssemblyD L
)L M
)M N
;N O
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
var"" 
app"" 
="" 	
builder""
 
."" 
Build"" 
("" 
)"" 
;"" 
app$$ 
.$$ 
UseAuthentication$$ 
($$ 
)$$ 
;$$ 
app%% 
.%% 
UseAuthorization%% 
(%% 
)%% 
;%% 
if'' 
('' 
app'' 
.'' 
Environment'' 
.'' 
IsDevelopment'' !
(''! "
)''" #
)''# $
{(( 
app)) 
.)) 

UseSwagger)) 
()) 
))) 
;)) 
app** 
.** 
UseSwaggerUI** 
(** 
)** 
;** 
}++ 
app,, 
.,, 
UseCors,, 
(,, 
$str,, 
),, 
;,, 
app-- 
.-- 

UseSwagger-- 
(-- 
)-- 
;-- 
app.. 
... 
UseSwaggerUI.. 
(.. 
).. 
;.. 
app// 
.// 
MapControllers// 
(// 
)// 
;// 
app11 
.11 
Run11 
(11 
)11 	
;11	 
–
y/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Application/Controllers/CatalogController.cs
	namespace 	
Application
 
. 
Controllers !
;! "
public 
class 
CatalogController 
:  
BaseController! /
{ 
[		 
HttpPost		 
]		 
public

 
async

 
Task

 
<

 
IActionResult

 $
>

$ %$
HandleCreateCatalogAsync

& >
(

> ?
Commands

? G
.

G H
CreateCatalog

H U
command

V ]
,

] ^
CancellationToken

_ p
cancellationToken	

q ‚
)


‚ ƒ
{ 
return	 
Ok 
( 
await 
Mediator !
.! "
Send" &
(& '
command' .
,. /
cancellationToken0 A
)A B
)B C
;C D
} 
} 