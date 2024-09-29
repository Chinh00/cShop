¬

h/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Basket/Command/Infrastructure/Extensions.cs
	namespace 	
Infrastructure
 
; 
public 
static 
class 

Extensions 
{ 
public 

static 
IServiceCollection $
AddGrpcClientCustom% 8
(8 9
this9 =
IServiceCollection> P
servicesQ Y
,Y Z
IConfiguration[ i
configurationj w
,w x
Action 
< 
IServiceCollection !
>! "
?" #
action$ *
=+ ,
null- 1
)1 2
{ 
services

 
.

 
AddGrpcClient

 
<

 
Catalog

 &
.

& '
CatalogClient

' 4
>

4 5
(

5 6
e

6 7
=>

8 :
e

; <
.

< =
Address

= D
=

E F
new

G J
Uri

K N
(

N O
$str

O g
)

g h
)

h i
;

i j
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
} 