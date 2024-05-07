using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Domain;

namespace GestorEventos.Persistence.Contracts
{
    public interface IPalestrantePersist
    {
        Task<List<Palestrante>> GetAllPalestrantesAsync(bool includeEventos);
        Task<List<Palestrante>> GetAllPalestrantesByNome(string name, bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEventos);
    }
}