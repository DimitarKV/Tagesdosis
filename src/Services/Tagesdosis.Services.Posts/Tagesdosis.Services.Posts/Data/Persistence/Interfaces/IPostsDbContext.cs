﻿using Microsoft.EntityFrameworkCore;
using Tagesdosis.Services.Posts.Data.Entities;

namespace Tagesdosis.Services.Posts.Data.Persistence.Interfaces;

public interface IPostDbContext
{
    public DbSet<Post>? Posts { get; set; }
    public DbSet<Author>? Authors { get; set; }
    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}