using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Domain;

namespace GestorEventos.Persistence.Contracts
{
    public interface IEventoPersist
    {
        Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes);
        Task<List<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes);
    }
}