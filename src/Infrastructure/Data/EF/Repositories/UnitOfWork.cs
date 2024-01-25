using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Data.EF.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context) => _context = context;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (var transactionScope = new TransactionScope())
            {
                UpdateAuditableEntities();

                var retorno = _context.SaveChangesAsync(cancellationToken);
                transactionScope.Complete();

                return retorno;
            }
        }

        private void UpdateAuditableEntities()
        {
            IEnumerable<EntityEntry<IAuditableEntity>> entities =
                _context.ChangeTracker.Entries<IAuditableEntity>();

            foreach (EntityEntry<IAuditableEntity> entityEntry in entities)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Property(a => a.CreatedOnUtc).CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entityEntry.Property(a => a.ModifiedOnUtc).CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entityEntry.Property(a => a.DeletedOnUtc).CurrentValue = DateTime.Now;
                        break;
                }
            }
        }
    }
}
