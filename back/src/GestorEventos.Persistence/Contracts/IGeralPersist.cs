using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorEventos.Persistence.Contracts
{
    public interface IGeralPersist
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity)where T : class;
        // Task<bool> Delete(int id);
        // Task<bool> SaveChangesAsync();
    }
}