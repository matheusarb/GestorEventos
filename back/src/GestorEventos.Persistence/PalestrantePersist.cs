using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Domain;
using GestorEventos.Persistence.Context;
using GestorEventos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GestorEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly GestorEventosContext _context;

        public PalestrantePersist(GestorEventosContext context)
            => _context = context;

        public async Task<List<Palestrante>> GetAllPalestrantesAsync(bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(pe => pe.RedesSociais);
            if(includeEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(PE => PE.Evento);
            
            query = query.AsNoTracking().OrderBy(pe => pe.Id);
            return await query.ToListAsync();
        }

        public async Task<List<Palestrante>> GetAllPalestrantesByNome(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(pe => pe.RedesSociais);
            if(includeEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(PE => PE.Evento);
            
            return await query.AsNoTracking().Where(pe => pe.Nome.Contains(name)).ToListAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                            .Include(pe => pe.RedesSociais);
            if(includeEventos)
                query = query.Include(pe => pe.PalestrantesEventos).ThenInclude(PE => PE.Evento);
            
            return await query.AsNoTracking().FirstOrDefaultAsync(pe => pe.Id == id);
        }
    }
}