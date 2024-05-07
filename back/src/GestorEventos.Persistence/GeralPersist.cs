using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Persistence.Context;
using GestorEventos.Persistence.Contracts;

namespace GestorEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly GestorEventosContext _context;

        public GeralPersist(GestorEventosContext context)
            => _context = context;

        public void Add<T>(T entity) where T : class
            => _context.Add(entity);

        public void Update<T>(T entity) where T : class
            => _context.Update(entity);

        public void Delete<T>(T entity) where T : class
            => _context.Remove(entity);

        public void DeleteRange<T>(List<T> entities) where T : class
            => _context.RemoveRange(entities);

        public async Task<bool> Commit()
            => await (_context.SaveChangesAsync()) > 0;
    }
}