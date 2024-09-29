Ï%
a/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/EventBus/Extensions.cs
	namespace 	
EventBus
 
; 
public 
static 
class 

Extensions 
{		 
public

 

static

 
IServiceCollection

 $
AddConsumerCustom

% 6
(

6 7
this

7 ;
IServiceCollection

< N
services

O W
,

W X
IConfiguration

Y g
configuration

h u
,

u v
Action

w }
<

} ~
IServiceCollection	

~ ê
>


ê ë
?


ë í
action


ì ô
=


ö õ
null


ú †
)


† °
{ 
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
public 

static 
IServiceCollection $
AddEventBus% 0
(0 1
this1 5
IServiceCollection6 H
servicesI Q
,Q R
IConfigurationS a
configurationb o
,o p
Actionq w
<w x
IServiceCollection	x ä
>
ä ã
?
ã å
action
ç ì
=
î ï
null
ñ ö
)
ö õ
{ 
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
public 

static 
IServiceCollection $ 
AddCustomMasstransit% 9
(9 :
this: >
IServiceCollection? Q
servicesR Z
,Z [
IConfiguration\ j
configk q
,q r
Actions y
<y z
IServiceCollection	z å
>
å ç
?
ç é
action
è ï
=
ñ ó
null
ò ú
)
ú ù
{   
services"" 
."" 
AddMassTransit"" 
(""  
cfg""  #
=>""$ &
{## 	
cfg$$ 
.$$ -
!SetKebabCaseEndpointNameFormatter$$ 1
($$1 2
)$$2 3
;$$3 4
cfg&& 
.&& 
UsingInMemory&& 
(&& 
)&& 
;&&  
cfg(( 
.(( 
AddRider(( 
((( 
rider(( 
=>(( !
{)) 
rider** 
.** 
AddConsumer** !
<**! "-
!ProductCreatedDomainEventConsumer**" C
>**C D
(**D E
)**E F
;**F G
rider++ 
.++ 
AddConsumer++ !
<++! "-
!ProductUpdatedDomainEventConsumer++" C
>++C D
(++D E
)++E F
;++F G
rider.. 
... 

UsingKafka..  
(..  !
(..! "
context.." )
,..) *
k..+ ,
).., -
=>... 0
{// 
k00 
.00 
Host00 
(00 
config00 !
.00! "
GetValue00" *
<00* +
string00+ 1
>001 2
(002 3
$str003 K
)00K L
)00L M
;00M N
k11 
.11 
TopicEndpoint11 #
<11# $%
ProductCreatedDomainEvent11$ =
>11= >
(11> ?
nameof11? E
(11E F%
ProductCreatedDomainEvent11F _
)11_ `
,11` a
$str11b t
,11t u
c22 
=>22 
{33 
c44 
.44 
ConfigureConsumer44 /
<44/ 0-
!ProductCreatedDomainEventConsumer440 Q
>44Q R
(44R S
context44S Z
)44Z [
;44[ \
}55 
)55 
;55 
k66 
.66 
TopicEndpoint66 #
<66# $)
ProductNameUpdatedDomainEvent66$ A
>66A B
(66B C
nameof66C I
(66I J)
ProductNameUpdatedDomainEvent66J g
)66g h
,66h i
$str66j |
,66| }
c77 
=>77 
{88 
c99 
.99 
ConfigureConsumer99 /
<99/ 0-
!ProductUpdatedDomainEventConsumer990 Q
>99Q R
(99R S
context99S Z
)99Z [
;99[ \
}:: 
):: 
;:: 
}>> 
)>> 
;>> 
}?? 
)?? 
;?? 
}AA 	
)AA	 

;AA
 
actionDD 
?DD 
.DD 
InvokeDD 
(DD 
servicesDD 
)DD  
;DD  !
returnEE 
servicesEE 
;EE 
}FF 
}HH ≈	
z/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/EventBus/Consumers/ProductUpdatedDomainEvent.cs
	namespace 	
EventBus
 
. 
	Consumers 
; 
public 
class -
!ProductUpdatedDomainEventConsumer .
:/ 0
	IConsumer1 :
<: ;)
ProductNameUpdatedDomainEvent; X
>X Y
{ 
private		 
readonly		 
	IMediator		 
	_mediator		 (
;		( )
public 
-
!ProductUpdatedDomainEventConsumer ,
(, -
	IMediator- 6
mediator7 ?
)? @
{ 
	_mediator 
= 
mediator 
; 
} 
public 

async 
Task 
Consume 
( 
ConsumeContext ,
<, -)
ProductNameUpdatedDomainEvent- J
>J K
contextL S
)S T
=>U W
awaitX ]
	_mediator^ g
.g h
Publishh o
(o p
contextp w
.w x
Messagex 
)	 Ä
;
Ä Å
} √	
Ç/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/EventBus/Consumers/ProductCreatedDomainEventConsumer.cs
	namespace 	
EventBus
 
. 
	Consumers 
; 
public 
class -
!ProductCreatedDomainEventConsumer .
:/ 0
	IConsumer1 :
<: ;%
ProductCreatedDomainEvent; T
>T U
{ 
private		 
readonly		 
	IMediator		 
	_mediator		 (
;		( )
public 
-
!ProductCreatedDomainEventConsumer ,
(, -
	IMediator- 6
mediator7 ?
)? @
{ 
	_mediator 
= 
mediator 
; 
} 
public 

async 
Task 
Consume 
( 
ConsumeContext ,
<, -%
ProductCreatedDomainEvent- F
>F G
contextH O
)O P
=>Q S
await 
	_mediator 
. 
Publish 
(  
context  '
.' (
Message( /
)/ 0
;0 1
} 