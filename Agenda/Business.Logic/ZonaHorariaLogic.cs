using Business.Entities;
using Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Logic
{
    public class ZonaHorariaLogic
    {
        private ZonaHorariaAdapter zonaHorariaData;

        public ZonaHorariaLogic()
        {
            zonaHorariaData = new ZonaHorariaAdapter();
        }

        public ZonaHoraria BuscarZonaHoraria(ZonaHoraria zonaHoraria)
        {
            return zonaHorariaData.BuscarZonaHoraria(zonaHoraria);
        }

        public List<ZonaHoraria> BuscarZonasHorarias()
        {
            return zonaHorariaData.BuscarZonasHorarias();
        }
    }
}
