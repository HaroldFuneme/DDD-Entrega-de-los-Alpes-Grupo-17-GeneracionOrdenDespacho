using DotPulsar.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneracionOrdenDespacho.ViewModels
{
    public class EventoIntegracionDespachador
    {
        public long eventId { get; set; }
        public string eventName { get; set; }
        public string eventDataFormat { get; set; }
        public PayloadEventoIntegracionDespachador payload { get; set; }

    }
}
