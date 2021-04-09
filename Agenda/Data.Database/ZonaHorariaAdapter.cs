using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Database
{
    public class ZonaHorariaAdapter
    {
        public ZonaHoraria BuscarZonaHoraria(ZonaHoraria zh)
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.ZonasHorarias.Find(zh.id_zona_horaria);
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar zona horaria", e);
                throw excepcionManejada;
            }
        }

        public List<ZonaHoraria> BuscarZonasHorarias()
        {
            try
            {
                using (Entities context = new Entities())
                {
                    return context.ZonasHorarias.ToList();
                }
            }
            catch (Exception e)
            {
                Exception excepcionManejada = new Exception("Error al buscar zonas horarias", e);
                throw excepcionManejada;
            }
        }
    }
}
