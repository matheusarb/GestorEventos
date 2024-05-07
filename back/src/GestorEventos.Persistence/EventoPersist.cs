using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using GestorEventos.Domain;
using GestorEventos.Persistence.Context;
using GestorEventos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GestorEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly GestorEventosContext _context;

        public EventoPersist(GestorEventosContext context)
            => _context = context;
            
        public async Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(ev => ev.Lotes)
                                        .Include(ev => ev.RedesSociais);
            if(includePalestrantes)
                query = query.Include(ev => ev.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            

            query = query.OrderBy(ev => ev.Data).AsNoTracking();
            return await query.ToListAsync();            
        }

        public async Task<List<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(ev => ev.Lotes)
                                        .Include(ev => ev.RedesSociais);
            
            if(includePalestrantes)
                query = query.Include(ev => ev.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            
            query = query.OrderBy(ev => ev.Id).Where(ev => ev.Tema.Contains(tema.ToLower())).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                        .Include(ev => ev.Lotes)
                                        .Include(ev => ev.RedesSociais);
            
            if(includePalestrantes)
                query = query.Include(ev => ev.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);

            return await query.AsNoTracking().FirstOrDefaultAsync(ev => ev.Id == id);
        }
    }
}