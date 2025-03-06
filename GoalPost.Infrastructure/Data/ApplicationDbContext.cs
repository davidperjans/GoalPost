using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GoalPost.Core.Domain;
using GoalPost.Core.Interfaces;

namespace GoalPost.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Här kan vi lägga till konfigurationer för våra entiteter senare
        // Till exempel:
        // builder.Entity<Goal>().HasOne(g => g.User).WithMany(u => u.Goals)
    }
} 