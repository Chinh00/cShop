Ù
w/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/GrpcService/Implements/CatalogGrpcService.cs
	namespace 	
GrpcService
 
. 

Implements  
;  !
public 
class 
CatalogGrpcService 
:  !
Catalog" )
.) *
CatalogBase* 5
{ 
private		 
readonly		 !
IProjectionRepository		 *
<		* +
CatalogProjection		+ <
>		< =(
_catalogProjectionRepository		> Z
;		Z [
public 

CatalogGrpcService 
( !
IProjectionRepository 3
<3 4
CatalogProjection4 E
>E F'
catalogProjectionRepositoryG b
)b c
{ (
_catalogProjectionRepository $
=% &'
catalogProjectionRepository' B
;B C
} 
public 

override 
async 
Task 
< "
GetProductByIdResponse 5
>5 6
getProductById7 E
(E F!
GetProductByIdRequestF [
request\ c
,c d
ServerCallContexte v
contextw ~
)~ 
{ 
var 
product 
= 
await (
_catalogProjectionRepository 8
.8 9
FindByIdAsync9 F
(F G
requestG N
.N O
IdO Q
,Q R
defaultS Z
)Z [
;[ \
return 
new "
GetProductByIdResponse )
() *
)* +
{ 	
	ProductId 
= 
product 
.  
Id  "
." #
ToString# +
(+ ,
), -
,- .
ProductName 
= 
product !
.! "
Name" &
} 	
;	 

} 
} ú
d/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/GrpcService/Extensions.cs
	namespace 	
GrpcService
 
; 
public 
class 

Extensions 
{ 
} 