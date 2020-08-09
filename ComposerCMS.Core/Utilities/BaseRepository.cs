using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ComposerCMS.Core.DAL;
using ComposerCMS.Core.Entity;
using ComposerCMS.Core.Identity;
using ComposerCMS.Core.Interface;
using ComposerCMS.Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ComposerCMS.Core.Utility
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        private ComposerCMSContext _context { get; set; }
        public ComposerCMSContext ComposerCMSContext
        {
            get
            {
                if (_context == null)
                {
                    this._context = new ComposerCMSContext();
                }

                return _context;
            }
        }

        public DbSet<TEntity> Table
        {
            get
            {
                return this.ComposerCMSContext.Set<TEntity>();
            }
        }

        private readonly UserResolver _userResolver;

        public BaseRepository(UserResolver userResolver)
        {
            this._userResolver = userResolver;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            IEntityTracking _tracking = (IEntityTracking)entity;

            _tracking.DateAdded = DateTime.UtcNow;
            _tracking.DateLastUpdated = DateTime.UtcNow;

            Guid _currentUserID = await this._userResolver.GetCurrentUserIDAsync();

            _tracking.UserIDAdded = _currentUserID;
            _tracking.UserIDLastUpdated = _currentUserID;

            await this.Table.AddAsync(entity);
            await this.ComposerCMSContext.SaveChangesAsync();

            if (typeof(TEntity) != typeof(ActivityHistory))
            {
                await this.ComposerCMSContext.ActivityHistory.AddAsync(new ActivityHistory()
                {
                    Entity = typeof(TEntity).Name,
                    Action = "Added",
                    DateAdded = DateTime.UtcNow,
                    UserIDAdded = _currentUserID
                });

                await this.ComposerCMSContext.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Guid _currentUserID = await this._userResolver.GetCurrentUserIDAsync();

            IEntityTracking _tracking = (IEntityTracking)entity;

            _tracking.DateLastUpdated = DateTime.UtcNow;
            _tracking.UserIDLastUpdated = _currentUserID;

            this.ComposerCMSContext.Entry(entity).State = EntityState.Modified;
            await this.ComposerCMSContext.SaveChangesAsync();

            if (typeof(TEntity) != typeof(ActivityHistory))
            {
                await this.ComposerCMSContext.ActivityHistory.AddAsync(new ActivityHistory()
                {
                    Entity = typeof(TEntity).Name,
                    Action = "Updated",
                    DateAdded = DateTime.UtcNow,
                    UserIDAdded = _currentUserID
                });
            }
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            this.Table.Remove(entity);
            await this.ComposerCMSContext.SaveChangesAsync();

            if (typeof(TEntity) != typeof(ActivityHistory))
            {
                await this.ComposerCMSContext.ActivityHistory.AddAsync(new ActivityHistory()
                {
                    Entity = typeof(TEntity).Name,
                    Action = "Deleted",
                    DateAdded = DateTime.UtcNow,
                });
            }
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool isTracked = true)
        {
            if (isTracked)
            {
                return await this.Table.AsTracking().FirstOrDefaultAsync(predicate);
            }
            else
            {
                return await this.Table.AsNoTracking().FirstOrDefaultAsync(predicate);
            }
        }

        public virtual async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> entities = await this.Table.Where(predicate).ToListAsync();
            return entities;
        }

        public virtual List<TEntity> ListAll(bool isTracking = true)
        {
            if (isTracking)
            {
                return this.Table.AsTracking().ToList();
            }
            else
            {
                return this.Table.AsNoTracking().ToList();
            }
        }
    }
}