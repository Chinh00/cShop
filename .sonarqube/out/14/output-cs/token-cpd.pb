Á
p/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Domain/Enumrations/ProductStatus.cs
	namespace 	
Domain
 
. 
Enumrations 
; 
public 
enum 
ProductStatus 
{ 
[ 
Description 
( 
$str $
)$ %
]% &
Active 

,
 
[		 
Description		 
(		 
$str		 %
)		% &
]		& '
Inactive

 
,

 
} º
g/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Domain/Entities/Product.cs
	namespace 	
Domain
 
. 
Entities 
; 
public 
class 
Product 
: 
AggregateBase $
{		 
public

 

string

 
Name

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 

float 
CurrentCost 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 
ImageSrc 
{ 
get  
;  !
set" %
;% &
}' (
public 

ProductStatus 
Status 
{  !
get" %
;% &
set' *
;* +
}, -
public 

Guid 
? 

CategoryId 
{ 
get !
;! "
set# &
;& '
}( )
public 

virtual 
Category 
Category $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 

void 
CreateProduct 
( 
Command %
.% &
CreateCatalog& 3
createCatalog4 A
)A B
{ 
Name 
= 
createCatalog 
. 
Name !
;! "
CurrentCost 
= 
createCatalog #
.# $
Price$ )
;) *
ImageSrc 
= 
createCatalog  
.  !
ImageSrc! )
;) *

CategoryId 
= 
createCatalog "
." #

CategoryId# -
;- .

RaiseEvent 
( 
version 
=> 
new !%
ProductCreatedDomainEvent" ;
(; <
Id< >
,> ?
Name@ D
,D E
CurrentCostF Q
,Q R
ImageSrcS [
,[ \

CategoryId] g
,g h
versioni p
)p q
)q r
;r s
} 
public 

void 
UpdateProductName !
(! "
string" (
name) -
)- .
{ 
Name   
=   
name   
;   

RaiseEvent!! 
(!! 
version!! 
=>!! 
new!! !)
ProductNameUpdatedDomainEvent!!" ?
(!!? @
Id!!@ B
,!!B C
name!!D H
,!!H I
(!!J K
int!!K N
)!!N O
version!!O V
)!!V W
)!!W X
;!!X Y
}"" 
public$$ 

void$$ 
With$$ 
($$ %
ProductCreatedDomainEvent$$ .
@event$$/ 5
)$$5 6
{%% 
(&& 	
Id&&	 
,&& 
Name&& 
,&& 
CurrentCost&& 
,&& 
ImageSrc&&  (
,&&( )

CategoryId&&* 4
,&&4 5
Version&&6 =
)&&= >
=&&? @
@event&&A G
;&&G H
}'' 
public)) 

void)) 
With)) 
()) )
ProductNameUpdatedDomainEvent)) 2
@event))3 9
)))9 :
{** 
(++ 	
Id++	 
,++ 
Name++ 
,++ 
Version++ 
)++ 
=++ 
@event++ $
;++$ %
},, 
public.. 

override.. 
void.. 

ApplyEvent.. #
(..# $
IDomainEvent..$ 0
@event..1 7
)..7 8
=>..9 ;
With..< @
(..@ A
@event..A G
as..H J
dynamic..K R
)..R S
;..S T
}11 °
h/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Command/Domain/Entities/Category.cs
	namespace 	
Domain
 
. 
Entities 
; 
public 
class 
Category 
: 
AggregateBase %
{ 
public 

string 
Name 
{ 
get 
; 
set !
;! "
}# $
public		 

string		 

PictureSrc		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 

override

 
void

 

ApplyEvent

 #
(

# $
IDomainEvent

$ 0
@event

1 7
)

7 8
{ 
throw 
new #
NotImplementedException )
() *
)* +
;+ ,
} 
} 