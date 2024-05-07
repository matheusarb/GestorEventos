using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using GestorEventos.Application.Contracts;
using GestorEventos.Domain;
using GestorEventos.Persistence;
using GestorEventos.Persistence.Context;

namespace GestorEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly EventoPersist _eventoPersist;
        private readonly GeralPersist _geralPersist;

        public EventoService(EventoPersist eventoPersist, GeralPersist geralPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.Commit())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);                    
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
                if(evento == null)
                    throw new Exception("evento não encontrado");
                
                evento.Update(model);
                _geralPersist.Update<Evento>(evento);

                if(await _geralPersist.Commit())
                {
                    return await _eventoPersist.GetEventoByIdAsync(id, false);
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> DeleteEvento(int id)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
                if(evento == null)
                    throw new Exception("evento não encontrado");
                
                _geralPersist.Delete<Evento>(evento);

                if(await _geralPersist.Commit())
                {
                    return new { message = "evento deletado" };
                }

                return null;
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                if(includePalestrantes)
                    return await _eventoPersist.GetAllEventosAsync(true);

                return await _eventoPersist.GetAllEventosAsync(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Evento>> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                if(includePalestrantes)
                    return await _eventoPersist.GetAllEventosByTemaAsync(tema, true);
                
                return await _eventoPersist.GetAllEventosByTemaAsync(tema, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false)
        {
            try
            {
                if(includePalestrantes)
                    return await _eventoPersist.GetEventoByIdAsync(id, true);
                
                return await _eventoPersist.GetEventoByIdAsync(id, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}