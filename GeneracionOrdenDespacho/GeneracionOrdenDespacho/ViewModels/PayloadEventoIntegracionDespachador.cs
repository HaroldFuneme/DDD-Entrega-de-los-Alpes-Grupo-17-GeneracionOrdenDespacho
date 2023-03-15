using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneracionOrdenDespacho.ViewModels
{
    public class PayloadEventoIntegracionDespachador
    {
        public Guid ordenId { get; set; }
        public string user { get; set; }
        public string user_addres { get; set; }

        public string items { get; set; }

        public string delivery_address { get; set; }
    }
}
