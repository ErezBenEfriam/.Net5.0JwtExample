Simple Example for Working with jwt on .net

startup.cs - configure jwt create Authorization based Policies

AuthController.cs: 

Create Authorization
Authenticate - Authenticate the user with no policy or roles
AuthenticateWithRole -Authenticate the user with role ("admin")
AuthenticateWithClaim - Authenticate the user with Policy("adminPolicy")

Check Authorization
GetAuthorize -compatible to  Authenticate
GetAuthorizeForByRole compatible to AuthenticateWithRole
GetAuthorizeByPolicy_Roles compatible to AuthenticateWithRole
GetAuthorizeForByPolicy_Claims compatible to AuthenticateWithClaim

JWTAuthenticationManager.cs:
Authenticate 
AuthenticateWithRole
AuthenticateWithCustomClaims

ApllicationDbContext.cs and Repository.cs are
for resemble a real application with a Data base with users table
but it not mandatory for jwt. 


