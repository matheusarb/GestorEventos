using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestorEventos.Persistence.Context
{
    public class GestorEventosContext : DbContext
    {
        public GestorEventosContext(DbContextOptions<GestorEventosContext> options)
            : base(options)
        {            
        }

        public DbSet<Evento> Eventos {get; set;}
        public DbSet<Lote> Lotes {get; set;}
        public DbSet<Palestrante> Palestrantes {get; set;}
        public DbSet<RedeSocial> RedesSociais {get; set;}
        public DbSet<PalestranteEvento> PalestranteEventos {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new { PE.EventoId, PE.PalestranteId });
            
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.Lotes)
                .WithOne(lt => lt.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Palestrante>()
                .HasMany(pl => pl.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}