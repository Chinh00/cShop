≤
o/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure.MongoDb/MongoDbConfig.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
MongoDb &
;& '
public 
class 
MongoDbConfig 
{ 
public 

string 

ServerName 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
$str- 8
;8 9
public 

int 
Port 
{ 
get 
; 
set 
; 
}  !
public 

string 
? 
Username 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
public		 

string		 
CollectionName		  
{		! "
get		# &
;		& '
set		( +
;		+ ,
}		- .
public 

override 
string 
ToString #
(# $
)$ %
{ 
if 

( 
Username 
is 
null 
) 
{ 	
return 
$" 
$str 
{  

ServerName  *
}* +
$str+ ,
{, -
Port- 1
}1 2
"2 3
;3 4
} 	
return 
$str 
; 
} 
} Å
y/Users/bachviet/Documents/Personal/dotnet/cShop/src/BuildingBlock/cShop.Infrastructure.MongoDb/Context/IMongoDbContext.cs
	namespace 	
cShop
 
. 
Infrastructure 
. 
MongoDb &
.& '
Context' .
;. /
public 
class 
IMongoDbContext 
{ 
} 