ß
ã/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Application/UseCases/Events/ProductUpdatedDomainEventInternal.cs
	namespace 	
Application
 
. 
UseCases 
. 
Events %
;% &
public 
class -
!ProductUpdatedDomainEventInternal .
:/ 0 
INotificationHandler1 E
<E F)
ProductNameUpdatedDomainEventF c
>c d
{		 
private

 
readonly

 !
IProjectionRepository

 *
<

* +
CatalogProjection

+ <
>

< =(
_catalogProjectionRepository

> Z
;

Z [
private 
readonly 
ILogger 
< -
!ProductUpdatedDomainEventInternal >
>> ?
_logger@ G
;G H
public 
-
!ProductUpdatedDomainEventInternal ,
(, -!
IProjectionRepository- B
<B C
CatalogProjectionC T
>T U'
catalogProjectionRepositoryV q
,q r
ILoggers z
<z {.
!ProductUpdatedDomainEventInternal	{ ú
>
ú ù
logger
û §
)
§ •
{ (
_catalogProjectionRepository $
=% &'
catalogProjectionRepository' B
;B C
_logger 
= 
logger 
; 
} 
public 

async 
Task 
Handle 
( )
ProductNameUpdatedDomainEvent :
notification; G
,G H
CancellationTokenI Z
cancellationToken[ l
)l m
{ 
_logger 
. 
LogInformation 
( 
$" !
$str! I
{I J
notificationJ V
}V W
"W X
)X Y
;Y Z
await (
_catalogProjectionRepository *
.* +
UpdateFieldAsync+ ;
(; <
notification< H
.H I
	ProductIdI R
,R S
notificationT `
.` a
Versiona h
,h i
ej k
=>l n
eo p
.p q
Nameq u
,u v
notification 
. 
Name 
, 
cancellationToken 0
)0 1
;1 2
await (
_catalogProjectionRepository *
.* +
UpdateFieldAsync+ ;
(; <
notification< H
.H I
	ProductIdI R
,R S
notificationT `
.` a
Versiona h
,h i
ej k
=>l n
eo p
.p q
Versionq x
,x y
notification 
. 
Version  
,  !
cancellationToken" 3
)3 4
;4 5
} 
} ˆ
ä/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Application/UseCases/Events/ProductCreateDomainEventInternal.cs
	namespace 	
Application
 
. 
UseCases 
. 
Events %
;% &
public 
class ,
 ProductCreateDomainEventInternal -
:. / 
INotificationHandler0 D
<D E%
ProductCreatedDomainEventE ^
>^ _
{ 
private		 
readonly		 
ILogger		 
<		 ,
 ProductCreateDomainEventInternal		 =
>		= >
_logger		? F
;		F G
private

 
readonly

 !
IProjectionRepository

 *
<

* +
CatalogProjection

+ <
>

< =
_repository

> I
;

I J
public 
,
 ProductCreateDomainEventInternal +
(+ ,
ILogger, 3
<3 4,
 ProductCreateDomainEventInternal4 T
>T U
loggerV \
,\ ]!
IProjectionRepository^ s
<s t
CatalogProjection	t Ö
>
Ö Ü

repository
á ë
)
ë í
{ 
_logger 
= 
logger 
; 
_repository 
= 

repository  
;  !
} 
public 

async 
Task 
Handle 
( %
ProductCreatedDomainEvent 6
notification7 C
,C D
CancellationTokenE V
cancellationTokenW h
)h i
{ 
_logger 
. 
LogInformation 
( 
$str <
,< =
notification> J
.J K
	ProductIdK T
)T U
;U V
await 
_repository 
. %
ReplaceOrInsertEventAsync 3
(3 4
new 
CatalogProjection !
(! "
)" #
{ 
Id 
= 
notification #
.# $
	ProductId$ -
,- .
Version/ 6
=7 8
notification9 E
.E F
VersionF M
,M N
NameO S
=T U
notificationV b
.b c
Namec g
,g h
CurrentCosti t
=u v
notification	w É
.
É Ñ
CurrentCost
Ñ è
}
è ê
,
ê ë
e 
=> 
e 
. 
Id 
== 
notification %
.% &
	ProductId& /
&&0 2
notification3 ?
.? @
Version@ G
>H I
eJ K
.K L
VersionL S
,S T
cancellationTokenU f
)f g
;g h
} 
} æ
a/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Application/Program.cs

AppContext

 

.


 
	SetSwitch

 
(

 
$str

 Q
,

Q R
true

S W
)

W X
;

X Y
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddLoggingCustom !
(! "
builder" )
.) *
Configuration* 7
,7 8
$str9 J
)J K
;K L
builder 
. 
Services 
. 
AddProjections 
(  
builder  '
.' (
Configuration( 5
)5 6
;6 7
builder 
. 
Services 
. 
AddConsumerCustom "
(" #
builder# *
.* +
Configuration+ 8
)8 9
;9 :
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
builder 
. 
Services 
. 
AddGrpc 
( 
) 
; 
builder 
. 
WebHost 
. 
ConfigureKestrel  
(  !
options! (
=>) +
{ 
options 
. 
Listen 
( 
	IPAddress 
. 
Any  
,  !
$num" &
,& '
listenOptions( 5
=>6 8
{ 
listenOptions 
. 
	Protocols 
=  !
HttpProtocols" /
./ 0
Http20 5
;5 6
listenOptions 
. 
UseHttps 
( 
)  
;  !
} 
) 
; 
} 
) 
; 
builder## 
.## 
Services## 
.## 

AddMediatR## 
(## 
e## 
=>##  
e##! "
.##" #*
RegisterServicesFromAssemblies### A
(##A B
[##B C
typeof##C I
(##I J-
!ProductCreatedDomainEventConsumer##J k
)##k l
.##l m
Assembly##m u
,##u v
typeof##w }
(##} ~
Program	##~ Ö
)
##Ö Ü
.
##Ü á
Assembly
##á è
]
##è ê
)
##ê ë
)
##ë í
;
##í ì
builder%% 
.%% 
Services%% 
.%%  
AddCustomMasstransit%% %
(%%% &
builder%%& -
.%%- .
Configuration%%. ;
)%%; <
.%%< =
AddEventBus%%= H
(%%H I
builder%%I P
.%%P Q
Configuration%%Q ^
)%%^ _
;%%_ `
var(( 
app(( 
=(( 	
builder((
 
.(( 
Build(( 
((( 
)(( 
;(( 
app** 
.** 
MapControllers** 
(** 
)** 
;** 
app++ 
.++ 

UseSwagger++ 
(++ 
)++ 
;++ 
app,, 
.,, 
UseSwaggerUI,, 
(,, 
),, 
;,, 
app.. 
... 
MapGrpcService.. 
<.. 
CatalogGrpcService.. %
>..% &
(..& '
)..' (
;..( )
app00 
.00 
Run00 
(00 
)00 	
;00	 
