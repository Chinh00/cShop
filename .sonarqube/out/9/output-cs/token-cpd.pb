ﬁ
m/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Contracts/Services/Catalog/Command.cs
	namespace 	
cShop
 
. 
	Contracts 
. 
Services "
." #
Catalog# *
;* +
public 
static 
class 
Command 
{ 
public 

record 
CreateCatalog 
(  
string  &
Name' +
,+ ,
float- 2
Price3 8
,8 9
string: @
ImageSrcA I
,I J
GuidK O

CategoryIdP Z
)Z [
:\ ]
Message^ e
;e f
}		 ß	
p/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Contracts/Services/Basket/DomainEvent.cs
	namespace 	
cShop
 
. 
	Contracts 
. 
Services "
." #
Basket# )
;) *
public 
static 
class 
DomainEvents  
{ 
public 

record 
BasketCreated 
(  
Guid  $
BasketId% -
,- .
Guid/ 3
UserId4 :
,: ;
long< @
VersionA H
)H I
:J K
MessageL S
,S T
IDomainEventU a
;a b
public

 

record

 
BasketItemAdded

 !
(

! "
Guid

" &
BasketId

' /
,

/ 0
Guid

1 5
UserId

6 <
,

< =
Guid

> B
	ProductId

C L
,

L M
float

N S
Price

T Y
,

Y Z
long

[ _
Version

` g
)

g h
:

i j
Message

k r
,

r s
IDomainEvent	

t Ä
{


Å Ç
}


Ç É
} ‡
l/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Contracts/Services/Basket/Command.cs
	namespace 	
cShop
 
. 
	Contracts 
. 
Services "
." #
Basket# )
;) *
public 
static 
class 
Command 
{ 
public 

record 
CreateBasket 
( 
Guid #
UserId$ *
)* +
:, -
Message. 5
,5 6
IRequest7 ?
<? @
Guid@ D
>D E
{		 
}

 
public 

record 
AddBasketItem 
(  
Guid  $
BasketId% -
,- .
Guid/ 3
UserId4 :
,: ;
Guid< @
	ProductIdA J
,J K
intL O
QuantityP X
,X Y
floatZ _
Price` e
)e f
:g h
Messagei p
,p q
IRequestr z
{{ |
}| }
} Ç

w/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Contracts/Events/DomainEvents/PartialClasses.cs
	namespace 	
cShop
 
. 
	Contracts 
. 
Events  
.  !
DomainEvents! -
;- .
public 
record %
ProductCreatedDomainEvent '
(' (
Guid 
	ProductId	 
, 
string 

Name 
, 
float 	
CurrentCost
 
, 
string 

ImageSrc 
, 
Guid 
? 	

CategoryId
 
, 
long 
Version	 
) 
: 
Message 
, 
IDomainEvent )
,) *
INotification+ 8
{ 
} 
public 
record )
ProductNameUpdatedDomainEvent +
(+ ,
Guid, 0
	ProductId1 :
,: ;
string< B
NameC G
,G H
longI M
VersionN U
)U V
:W X
MessageY `
,` a
IDomainEventb n
,n o
INotificationp }
{ 
} §
l/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Contracts/Abstractions/StoreEvent.cs
	namespace 	
cShop
 
. 
	Contracts 
. 
Abstractions &
;& '
public 
sealed 
record 

StoreEvent 
(  
Guid  $
AggregateId% 0
,0 1
string2 8
AggregateType9 F
,F G
stringH N
	EventTypeO X
,X Y
IDomainEventZ f
@Eventg m
,m n
longo s
Versiont {
,{ |
DateTime	} Ö
	CreatedAt
Ü è
)
è ê
{ 
public 

static 

StoreEvent 
Create #
(# $
AggregateBase$ 1
aggregateBase2 ?
,? @
IDomainEventA M
@eventN T
)T U
=>V X
new		 

StoreEvent		 
(		 
aggregateBase		 $
.		$ %
Id		% '
,		' (
aggregateBase		) 6
.		6 7
GetType		7 >
(		> ?
)		? @
.		@ A
Name		A E
,		E F
@event		G M
.		M N
GetType		N U
(		U V
)		V W
.		W X
Name		X \
,		\ ]
@event		^ d
,		d e
@event		f l
.		l m
Version		m t
,		t u
@event		v |
.		| }
CreateAt			} Ö
)
		Ö Ü
;
		Ü á
}

 