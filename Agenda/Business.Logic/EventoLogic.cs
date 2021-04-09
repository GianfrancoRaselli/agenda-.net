using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class EventoLogic
    {
        private EventoAdapter eventoData;

        public EventoLogic()
        {
            eventoData = new EventoAdapter();
        }

        public Evento BuscarEvento(Evento evento)
        {
            return eventoData.BuscarEvento(evento);
        }

        public List<Evento> BuscarEventosDelUsuario(Usuario user)
        {
            return eventoData.BuscarEventosDelUsuario(user);
        }

        public List<Evento> BuscarEventosDelUsuario(Usuario user, DateTime fecha)
        {
            return eventoData.BuscarEventosDelUsuario(user, fecha);
        }

        public void RegistrarEvento(Evento evento)
        {
            eventoData.RegistrarEvento(evento);
        }

        public void ActualizarEvento(Evento evento)
        {
            eventoData.ActualizarEvento(evento);
        }

        public void EliminarEvento(Evento evento)
        {
            eventoData.EliminarEvento(evento);
        }
    }
}
