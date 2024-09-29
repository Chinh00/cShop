Ë
q/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Application/UseCases/Dto/BasketDto.cs
	namespace 	
Application
 
. 
UseCases 
. 
Dto "
;" #
public 
class 
	BasketDto 
{ 
} û
{/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Application/UseCases/Commands/CommandHandler.cs
	namespace 	
Application
 
. 
UseCases 
. 
Commands '
;' (
public 
class 
CommandHandler 
: 
IRequestHandler -
<- .
Command. 5
.5 6
CreateBasket6 B
,B C
GuidD H
>H I
{		 
private 
readonly !
IClaimContextAccessor *
_contextAccessor+ ;
;; <
private 
readonly !
IEventStoreRepository *!
_eventStoreRepository+ @
;@ A
private 
readonly 
ILogger 
< 
CommandHandler +
>+ ,
_logger- 4
;4 5
public 

CommandHandler 
( !
IClaimContextAccessor /
contextAccessor0 ?
,? @!
IEventStoreRepositoryA V 
eventStoreRepositoryW k
,k l
ILoggerm t
<t u
CommandHandler	u ƒ
>
ƒ „
logger
… ‹
)
‹ Œ
{ 
_contextAccessor 
= 
contextAccessor *
;* +!
_eventStoreRepository 
=  
eventStoreRepository  4
;4 5
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Guid 
> 
Handle "
(" #
Command# *
.* +
CreateBasket+ 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 
return 
_contextAccessor 
.  
	GetUserId  )
() *
)* +
;+ ,
} 
} ô
a/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Application/Anchor.cs
	namespace 	
Application
 
; 
public 
record 
Anchor 
{ 
} 