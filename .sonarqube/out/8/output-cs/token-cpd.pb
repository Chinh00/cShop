ô+
n/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/ProjectionRepository.cs
	namespace 	
Projections
 
; 
public 
class  
ProjectionRepository !
<! "
TProjection" -
>- .
:/ 0!
IProjectionRepository1 F
<F G
TProjectionG R
>R S
where 	
TProjection
 
: 
ProjectionBase &
{		 
private

 
readonly

 
ILogger

 
<

 
TProjection

 (
>

( )
_logger

* 1
;

1 2
private 
readonly  
IProjectionDbContext ) 
_projectionDbContext* >
;> ?
public 
 
ProjectionRepository 
(  
ILogger  '
<' (
TProjection( 3
>3 4
logger5 ;
,; < 
IProjectionDbContext= Q
projectionDbContextR e
)e f
{ 
_logger 
= 
logger 
;  
_projectionDbContext 
= 
projectionDbContext 2
;2 3
} 
public 

async 
Task %
ReplaceOrInsertEventAsync /
(/ 0
TProjection0 ;
replace< C
,C D

ExpressionE O
<O P
FuncP T
<T U
TProjectionU `
,` a
boolb f
>f g
>g h
filteri o
,o p
CancellationToken	q Ç
cancellationToken
É î
)
î ï
{ 
_logger 
. 
LogInformation 
( 
$str F
,F G
typeofH N
(N O
TProjectionO Z
)Z [
.[ \
Name\ `
)` a
;a b
await  
_projectionDbContext "
." #
GetCollection# 0
<0 1
TProjection1 <
>< =
(= >
)> ?
.? @
ReplaceOneAsync@ O
(O P
filterP V
,V W
replaceX _
,_ `
newa d
ReplaceOptionse s
(s t
)t u
{v w
IsUpsert	x Ä
=
Å Ç
true
É á
}
à â
,
â ä
cancellationToken
ã ú
:
ú ù
cancellationToken
û Ø
)
Ø ∞
;
∞ ±
} 
public 

async 
Task 
UpdateFieldAsync &
<& '
TField' -
,- .
TId/ 2
>2 3
(3 4
TId4 7
id8 :
,: ;
long< @
versionA H
,H I

ExpressionJ T
<T U
FuncU Y
<Y Z
TProjectionZ e
,e f
TFieldg m
>m n
>n o
fieldp u
,u v
TFieldw }
value	~ É
,
É Ñ
CancellationToken 
cancellationToken +
)+ ,
{ 
await  
_projectionDbContext "
." #
GetCollection# 0
<0 1
TProjection1 <
>< =
(= >
)> ?
.? @
UpdateOneAsync@ N
(N O
filter 
: 
e 
=> 
e 
. 
Id 
. 
Equals $
($ %
id% '
)' (
&&) +
e, -
.- .
Version. 5
<6 7
version8 ?
,? @
update 
: 
new "
ObjectUpdateDefinition .
<. /
TProjection/ :
>: ;
(; <
new< ?
object@ F
(F G
)G H
)H I
.I J
SetJ M
(M N
fieldN S
,S T
valueU Z
)Z [
,[ \
cancellationToken   
:   
cancellationToken   0
)!! 	
;!!	 

}"" 
public$$ 

async$$ 
Task$$ 
<$$ 
TProjection$$ !
>$$! "
FindByIdAsync$$# 0
<$$0 1
TId$$1 4
>$$4 5
($$5 6
TId$$6 9
id$$: <
,$$< =
CancellationToken$$> O
cancellationToken$$P a
)$$a b
{%% 
var&& 
entity&& 
=&& 
await&&  
_projectionDbContext&& /
.&&/ 0
GetCollection&&0 =
<&&= >
TProjection&&> I
>&&I J
(&&J K
)&&K L
.&&L M
	FindAsync&&M V
(&&V W
e&&W X
=>&&Y [
e&&\ ]
.&&] ^
Id&&^ `
==&&a c
Guid&&d h
.&&h i
Parse&&i n
(&&n o
id&&o q
.&&q r
ToString&&r z
(&&z {
)&&{ |
)&&| }
,&&} ~
cancellationToken	&& ê
:
&&ê ë
cancellationToken
&&í £
)
&&£ §
;
&&§ •
return'' 
await'' 
entity'' 
.'' 
FirstOrDefaultAsync'' /
(''/ 0
cancellationToken''0 A
:''A B
cancellationToken''C T
)''T U
;''U V
}(( 
})) ﬂ
m/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/ProjectionDbContext.cs
	namespace 	
Projections
 
; 
public 
class 
ProjectionDbContext  
:! " 
IProjectionDbContext# 7
{ 
private

 
readonly

 
IMongoDatabase

 #
_mongoDatabase

$ 2
;

2 3
public 

ProjectionDbContext 
( 
IOptions '
<' (
MongoDbOptions( 6
>6 7
options8 ?
)? @
{ 
var 
mongoClient 
= 
new 
MongoClient )
() *
options* 1
.1 2
Value2 7
.7 8
ToString8 @
(@ A
)A B
)B C
;C D
_mongoDatabase 
= 
mongoClient $
.$ %
GetDatabase% 0
(0 1
options1 8
.8 9
Value9 >
.> ?
DatabaseName? K
)K L
;L M
} 
public 

IMongoCollection 
< 
T 
> 
GetCollection ,
<, -
T- .
>. /
(/ 0
)0 1
=>2 4
_mongoDatabase5 C
.C D
GetCollectionD Q
<Q R
TR S
>S T
(T U
typeofU [
([ \
T\ ]
)] ^
.^ _
Name_ c
)c d
;d e
} §
h/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/MongoDbOptions.cs
	namespace 	
Projections
 
; 
public 
class 
MongoDbOptions 
{ 
public 

const 
string 
MongoDb 
=  !
$str" +
;+ ,
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
}( )
=* +
default, 3
!3 4
;4 5
public		 

string		 
Password		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public 

string 
DatabaseName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

string 
CollectionName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 

string 

ServerName 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
$str- 8
;8 9
public 

int 
Port 
{ 
get 
; 
set 
; 
}  !
=" #
$num$ )
;) *
public 

override 
string 
ToString #
(# $
)$ %
{ 
return 
$" 
$str 
{ 
Username $
}$ %
$str% &
{& '
Password' /
}/ 0
$str0 1
{1 2

ServerName2 <
}< =
$str= >
{> ?
Port? C
}C D
$strD E
{E F
DatabaseNameF R
}R S
$strS d
"d e
;e f
} 
} á
o/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/IProjectionRepository.cs
	namespace 	
Projections
 
; 
public 
class 
CatalogProjection 
:  
ProjectionBase! /
{ 
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

float		 
CurrentCost		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
}

 
public 
	interface !
IProjectionRepository &
<& '
T' (
>( )
where 	
T
 
: 
ProjectionBase 
{ 
public 

Task %
ReplaceOrInsertEventAsync )
() *
T* +
replace, 3
,3 4

Expression5 ?
<? @
Func@ D
<D E
TE F
,F G
boolH L
>L M
>M N
filterO U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
;{ |
public 

Task 
UpdateFieldAsync  
<  !
TField! '
,' (
TId) ,
>, -
(- .
TId. 1
id2 4
,4 5
long6 :
version; B
,B C

ExpressionD N
<N O
FuncO S
<S T
TT U
,U V
TFieldW ]
>] ^
>^ _
field` e
,e f
TFieldg m
valuen s
,s t
CancellationToken	u Ü
cancellationToken
á ò
)
ò ô
;
ô ö
public 

Task 
< 
T 
> 
FindByIdAsync  
<  !
TId! $
>$ %
(% &
TId& )
id* ,
,, -
CancellationToken. ?
cancellationToken@ Q
)Q R
;R S
} Ê
n/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/IProjectionDbContext.cs
	namespace 	
Projections
 
; 
public 
	interface  
IProjectionDbContext %
{ 
public 

IMongoCollection 
< 
T 
> 
GetCollection ,
<, -
T- .
>. /
(/ 0
)0 1
;1 2
} •
d/Users/bachviet/Documents/Personal/dotnet/cShop/src/Services/Catalog/Query/Projections/Extensions.cs
	namespace 	
Projections
 
; 
public 
static 
class 

Extensions 
{ 
public		 

static		 
IServiceCollection		 $
AddProjections		% 3
(		3 4
this		4 8
IServiceCollection		9 K
services		L T
,		T U
IConfiguration		V d
configuration		e r
,		r s
Action		t z
<		z {
IServiceCollection			{ ç
>
		ç é
?
		é è
action
		ê ñ
=
		ó ò
null
		ô ù
)
		ù û
{

 
services 
. 
	Configure 
< 
MongoDbOptions )
>) *
(* +
configuration+ 8
.8 9

GetSection9 C
(C D
MongoDbOptionsD R
.R S
MongoDbS Z
)Z [
)[ \
;\ ]
services 
. 
	AddScoped 
<  
IProjectionDbContext /
,/ 0
ProjectionDbContext1 D
>D E
(E F
)F G
;G H
services 
. 
	AddScoped 
( 
typeof !
(! "!
IProjectionRepository" 7
<7 8
>8 9
)9 :
,: ;
typeof< B
(B C 
ProjectionRepositoryC W
<W X
>X Y
)Y Z
)Z [
;[ \
BsonSerializer 
. 
RegisterSerializer )
() *
new* -
GuidSerializer. <
(< =
BsonType= E
.E F
StringF L
)L M
)M N
;N O
action 
? 
. 
Invoke 
( 
services 
)  
;  !
return 
services 
; 
} 
} 