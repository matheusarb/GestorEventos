using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Application.Contracts;
using GestorEventos.Domain;
using GestorEventos.Domain.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GestorEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : Controller
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventos()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync();
                if(eventos.Count == 0)
                    return NotFound("Não foi possível completar a requisição");
                
                return Ok(eventos);
            }
            catch (Exception ex)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetEventosByTema(string tema)
        {
          try
          {
            var eventosByTema = await _eventoService.GetAllEventosByTemaAsync(tema);
            if(eventosByTema.Count == 0)
              return NotFound("Eventos não encontrados com esse tema");
            return Ok(eventosByTema);
          }
          catch (Exception ex)
          {                
              return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
          }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventoById(int id)
        {
          try
          {
            var evento = await _eventoService.GetEventoByIdAsync(id);
            if(evento == null || !IsValid(evento))
              return NotFound("Evento não encontrado");
            return Ok(evento);
          }
          catch (Exception ex)
          {                
              return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
          }
        }

        private static bool IsValid(Evento evento)
        {
          return !evento.Tema.IsNullOrEmptyOrWhiteSpace();
        }
    }
}