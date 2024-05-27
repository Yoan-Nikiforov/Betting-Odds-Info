using Microsoft.EntityFrameworkCore;
using UltraPlayTask.Data.Models;

namespace UltraPlayTask.Data.DbHandlers
{
    public abstract class BaseDbHandler
    {
        protected readonly ApplicationDbContext _context;

        public BaseDbHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddEntity<T>(T entity) where T : BaseDataModel
        {
            _context.Set<T>().Add(entity);

            _context.SaveChanges();
        }

        public T GetEntityById<T>(string id) where T : BaseDataModel
        {
            return _context.Set<T>().FirstOrDefault(x => x.ID == id);
        }

        public List<T> GetEntities<T>() where T : BaseDataModel
        {
            return _context.Set<T>().ToList();
        }

        public void UpdateEntity<T>(T entity) where T : BaseDataModel
        {
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.ID.Equals(entity.ID));

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}