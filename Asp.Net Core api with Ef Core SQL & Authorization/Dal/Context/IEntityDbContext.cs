using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using $safeprojectname$.Entities;

namespace $safeprojectname$.Context
{
    public interface IEntityDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        DatabaseFacade Database { get; }
    }
}