þ
e/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/MessageBus/Extensions.cs
	namespace 	

MessageBus
 
; 
public 
static 
class 

Extensions 
{		 
public 

static 
IServiceCollection $ 
AddCustomMasstransit% 9
(9 :
this: >
IServiceCollection? Q
servicesR Z
,Z [
IConfiguration\ j
configk q
)q r
{ 
services 
. 
AddMessageBus 
( 
config %
)% &
;& '
services 
. 
AddMassTransit 
(  
cfg  #
=>$ &
{ 	
cfg 
. -
!SetKebabCaseEndpointNameFormatter 1
(1 2
)2 3
;3 4
cfg 
. 
UsingInMemory 
( 
) 
;  
cfg 
. 
AddRider 
( 
rider 
=> !
{ 
rider 
. 
AddProducer !
<! "%
ProductCreatedDomainEvent" ;
>; <
(< =
nameof= C
(C D%
ProductCreatedDomainEventD ]
)] ^
)^ _
;_ `
rider 
. 
AddProducer !
<! ")
ProductNameUpdatedDomainEvent" ?
>? @
(@ A
nameofA G
(G H)
ProductNameUpdatedDomainEventH e
)e f
)f g
;g h
rider 
. 

UsingKafka  
(  !
(! "
context" )
,) *
k+ ,
), -
=>. 0
{ 
k 
. 
Host 
( 
config !
.! "
GetValue" *
<* +
string+ 1
>1 2
(2 3
$str3 K
)K L
)L M
;M N
k!! 
.!! 
TopicEndpoint!! #
<!!# $%
ProductCreatedDomainEvent!!$ =
>!!= >
(!!> ?
nameof!!? E
(!!E F%
ProductCreatedDomainEvent!!F _
)!!_ `
,!!` a
$str!!b k
,!!k l
c!!m n
=>!!o q
{"" 
c## 
.## 
AutoOffsetReset## )
=##* +
AutoOffsetReset##, ;
.##; <
Earliest##< D
;##D E
c$$ 
.$$ 
CreateIfMissing$$ )
($$) *
e$$* +
=>$$, .
e$$/ 0
.$$0 1
NumPartitions$$1 >
=$$? @
$num$$A B
)$$B C
;$$C D
}%% 
)%% 
;%% 
k'' 
.'' 
TopicEndpoint'' #
<''# $)
ProductNameUpdatedDomainEvent''$ A
>''A B
(''B C
nameof''C I
(''I J)
ProductNameUpdatedDomainEvent''J g
)''g h
,''h i
$str''j s
,''s t
c''u v
=>''w y
{(( 
c)) 
.)) 
AutoOffsetReset)) )
=))* +
AutoOffsetReset)), ;
.)); <
Earliest))< D
;))D E
c** 
.** 
CreateIfMissing** )
(**) *
e*** +
=>**, .
e**/ 0
.**0 1
NumPartitions**1 >
=**? @
$num**A B
)**B C
;**C D
}++ 
)++ 
;++ 
}.. 
).. 
;.. 
}00 
)00 
;00 
}33 	
)33	 

;33
 
return55 
services55 
;55 
}66 
}77 