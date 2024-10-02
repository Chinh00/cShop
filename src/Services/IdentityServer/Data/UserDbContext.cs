using IdentityServer.Data.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    
}