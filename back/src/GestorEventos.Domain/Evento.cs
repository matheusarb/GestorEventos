using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestorEventos.Domain.Extensions;

namespace GestorEventos.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime? Data { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }

        public Evento Update(Evento model)
        {
            Id = Id;
            Local = model.Local.IsNullOrEmptyOrWhiteSpace() ? Local : model.Local;
            Data = model.Data == null ? Data : model.Data;
            Tema = model.Tema.IsNullOrEmptyOrWhiteSpace() ? Tema : model.Tema;
            QtdPessoas = model.QtdPessoas == 0 ? QtdPessoas : model.QtdPessoas;
            ImagemURL = model.ImagemURL.IsNullOrEmptyOrWhiteSpace() ? ImagemURL : model.ImagemURL;
            Telefone = model.Telefone.IsNullOrEmptyOrWhiteSpace() ? Telefone : model.Telefone;
            Email = model.Email.IsNullOrEmptyOrWhiteSpace() ? Email : model.Email;
            Lotes = model.Lotes == null || !model.Lotes.Any() ? Lotes : model.Lotes;
            RedesSociais = model.RedesSociais == null || !model.RedesSociais.Any() ? RedesSociais : model.RedesSociais;
            PalestrantesEventos = model.PalestrantesEventos == null || !model.PalestrantesEventos.Any() ? PalestrantesEventos : model.PalestrantesEventos;

            return this;
        }
    }
}