using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneracionOrdenDespacho.Persistencia.Modelos
{
    [Table("sagalog")]
    public class SagaLog
    {
        [Key]
        public Guid SagaLogId { get; set; } = Guid.NewGuid();

        public Guid? OrdenId { get; set; }

        public DateTime Fecha { get; set; }

        [MaxLength(1024)]
        public long EventoId { get; set; }

        [MaxLength(1024)]
        public string NombreEvento { get; set; }

        [MaxLength(20000)]
        public string PayloadSerializado { get; set; }

        #region Propiedades de navegación



        #endregion
    }
}
