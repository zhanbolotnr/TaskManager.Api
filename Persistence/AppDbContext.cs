using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskManager.Api.Domain;

namespace TaskManager.Api.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}


