¨
i/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Domain/Entities/BasketItem.cs
	namespace 	
Domain
 
. 
Entities 
; 
public 
class 

BasketItem 
: 

EntityBase $
{ 
public 

Guid 
BasketId 
{ 
get 
; 
set  #
;# $
}% &
public

 

Guid

 
	ProductId

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
public 

int 
Quantity 
{ 
get 
; 
set "
;" #
}$ %
public 

float 
Price 
{ 
get 
; 
set !
;! "
}# $
} ◊
e/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Domain/Entities/Basket.cs
	namespace 	
Domain
 
. 
Entities 
; 
public 
class 
Basket 
: 
AggregateBase #
{ 
public 

Guid 
UserId 
{ 
get 
; 
set !
;! "
}# $
public		 

float		 

TotalPrice		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public 

List 
< 

BasketItem 
> 
BasketItems '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 

void 
CreateBasket 
( 
Command $
.$ %
CreateBasket% 1
createBasket2 >
)> ?
{ 
UserId 
= 
createBasket 
. 
UserId $
;$ %

TotalPrice 
= 
$num 
; 

RaiseEvent 
( 
version 
=> 
new !
DomainEvents" .
.. /
BasketCreated/ <
(< =
Id= ?
,? @
UserIdA G
,G H
versionI P
)Q R
)R S
;S T
} 
public 

void 
AddBasketItem 
( 
Command %
.% &
AddBasketItem& 3
addBasketItem4 A
)A B
{ 
BasketItems 
??= 
[ 
] 
; 
BasketItems 
. 
Add 
( 
new 

BasketItem &
(& '
)' (
{ 	
	ProductId 
= 
addBasketItem %
.% &
	ProductId& /
,/ 0
Quantity 
= 
addBasketItem $
.$ %
Quantity% -
,- .
Price 
= 
addBasketItem !
.! "
Price" '
} 	
)	 

;
 

RaiseEvent 
( 
version 
=> 
new !
DomainEvents" .
.. /
BasketItemAdded/ >
(> ?
Id? A
,A B
UserIdC I
,I J
addBasketItemK X
.X Y
	ProductIdY b
,b c
addBasketItemd q
.q r
Pricer w
,w x
version	y Ä
)
Ä Å
)
Å Ç
;
Ç É
}   
public## 

override## 
void## 

ApplyEvent## #
(### $
IDomainEvent##$ 0
@event##1 7
)##7 8
{$$ 
throw%% 
new%% #
NotImplementedException%% )
(%%) *
)%%* +
;%%+ ,
}&& 
}'' 