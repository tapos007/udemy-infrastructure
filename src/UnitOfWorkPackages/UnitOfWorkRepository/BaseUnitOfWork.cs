using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdemyInfrastructure.UnitOfWorkRepository.Contract;
using UdemyInfrastructure.UnitOfWorkRepository.Interfaces;

namespace UdemyInfrastructure.UnitOfWorkRepository;

public class BaseUnitOfWork : IUnityOfWork
{
    public readonly DbContext Context;

        public BaseUnitOfWork(DbContext context) 
        {
            Context = context;
        }

        public Task BeginTransactionAsync()
        {
            return Context.Database.BeginTransactionAsync();
        }

        public Task RollBackTransactionAsync()
        {
            return Context.Database.RollbackTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            return Context.Database.CommitTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            SetProperties();
            var info = await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
            return info;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        private void SetProperties()
        {
            var now = DateTime.UtcNow;
            
            
            var entries = Context.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entityEntry  in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    if (entityEntry.Entity is IDateCreated created)
                    {
                        created.CreateDate = now;
                    }

                    if (entityEntry.Entity is IDateUpdated updated)
                    {
                        updated.LastUpdateDate = now;
                    }
                }else if (entityEntry.State == EntityState.Modified)
                {
                    if (entityEntry.Entity is IDateUpdated updated)
                    {
                        updated.LastUpdateDate = now;
                    }
                }
            }

            var modifiedEntries = Context.ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                //If the inserted object is an Auditable. 
                if (modifiedEntry is IDateUpdated auditableEntity)
                {
                    auditableEntity.LastUpdateDate = now;
                }
            }
        }
}