Ñ

g/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/RequestDispatcher.cs
	namespace 	
WebApi
 
; 
public 
class 
RequestDispatcher 
:  
IRequestHandler! 0
<0 1
IRequest1 9
<9 :
object: @
>@ A
,A B
objectC I
>I J
{ 
private 
readonly 
ISender 
	_mediator &
;& '
public		 

RequestDispatcher		 
(		 
ISender		 $
mediator		% -
)		- .
{

 
	_mediator 
= 
mediator 
; 
} 
public 

async 
Task 
< 
object 
> 
Handle $
($ %
IRequest% -
<- .
object. 4
>4 5
request6 =
,= >
CancellationToken? P
cancellationTokenQ b
)b c
{ 
return 
( 
await 
	_mediator 
.  
Send  $
($ %
request% ,
,, -
cancellationToken. ?
)? @
)@ A
;A B
} 
} Õ
]/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/Program.cs
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
builder 
. 
Services 
. 
AddLoggingCustom 
( 
builder 
. 
Configuration +
,+ ,
$str- ?
)? @
. $
AddAuthenticationDefault 
( 
builder %
.% &
Configuration& 3
)3 4
. 
AddSwaggerCustom 
( 
builder 
. 
Configuration +
)+ ,
. 
AddMediatorDefault 
( 
[ 
typeof 
(  
Program  '
)' (
,( )
typeof* 0
(0 1
Anchor1 7
)7 8
]8 9
)9 :
; 
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
app 
. $
UseAuthenticationDefault 
( 
builder $
.$ %
Configuration% 2
)2 3
. 
ConfigureSwagger 
( 
builder 
. 
Configuration +
)+ ,
;, -
app 
. 
Run 
( 
) 	
;	 
 
^/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/Commands.cs
	namespace 	
WebApi
 
; 
public 
static 
class 
Commands 
{ 
public		 

record		 
CreateBasket		 
(		 
)		  
{

 
public 
static 
Command 
. 
CreateBasket *
Command+ 2
(2 3
Guid3 7
userId8 >
)> ?
=>@ B
newC F
(G H
userIdH N
)N O
;O P
} 
} Ã
q/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/Application/IApplicationApi.cs
	namespace 	
WebApi
 
. 
Application 
; 
public 
	interface 
IApplicationApi  
{ 
public 

Task 
< 
	TResponse 
> 
Send 
<  
TRequest  (
,( )
	TResponse* 3
>3 4
(4 5
TRequest5 =
request> E
,E F
CancellationTokenG X
cancellationTokenY j
=k l
defaultm t
)t u
;u v
} ö
p/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/Application/ApplicationApi.cs
	namespace 	
WebApi
 
. 
Application 
; 
public 
static 
class 
ApplicationApi "
{ 
public		 

static		 
async		 
Task		 
<		 
	TResponse		 &
>		& '
Send		( ,
<		, -
TRequest		- 5
,		5 6
	TResponse		7 @
>		@ A
(		A B
ISender		B I
sender		J P
,		P Q
TRequest		R Z
request		[ b
,		b c
CancellationToken		d u
cancellationToken			v ‡
)
		‡ ˆ
{

 
var 
result 
= 
await 
sender !
.! "
Send" &
(& '
request' .
,. /
cancellationToken0 A
)A B
;B C
return 
( 
	TResponse 
) 
result  
;  !
} 
} ¼
d/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/WebApi/Apis/BasketApi.cs
	namespace 	
WebApi
 
. 
Apis 
; 
public 
static 
class 
	BasketApi 
{ 
private 
const 
string 
BaseUrl  
=! "
$str# F
;F G
public		 

static		 *
IVersionedEndpointRouteBuilder		 0
MapBasketApiV1		1 ?
(		? @
this		@ D*
IVersionedEndpointRouteBuilder		E c
	endpoints		d m
)		m n
{

 
var 
group 
= 
	endpoints 
. 
MapGroup &
(& '
BaseUrl' .
). /
./ 0
HasApiVersion0 =
(= >
$num> ?
)? @
;@ A
group 
. 
MapPost 
( 
string 
. 
Empty "
," #
async$ )
(* +
ISender+ 2
sender3 9
,9 :
CancellationToken; L
cancellationTokenM ^
)^ _
=>` b
{ 	
var 
result 
= 
await 
sender %
.% &
Send& *
(* +
new+ .
Commands/ 7
.7 8
CreateBasket8 D
(D E
)E F
,F G
cancellationTokenH Y
)Y Z
;Z [
return 
Results 
. 
Ok 
( 
result $
)$ %
;% &
} 	
)	 

;
 
return 
	endpoints 
; 
} 
} 