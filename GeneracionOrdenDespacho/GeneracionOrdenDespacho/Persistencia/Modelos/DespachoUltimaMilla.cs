using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneracionOrdenDespacho.Persistencia.Modelos
{
    [Table("despachosultimamilla")]

    public class DespachoUltimaMilla
    {
        [Key]
        public Guid DespachoUltimaMillaId { get; set; } = Guid.NewGuid();
        public Guid OrdenId { get; set; }
        [MaxLength(255)]
        public string NombreBodega { get; set; }
        [MaxLength(255)]
        public string IdentificadorDelivery { get; set; }
        [MaxLength(40000)]
        public string Items { get; set; }

        #region Propiedades de navegación

        public virtual Orden Orden { get; set; }

        #endregion
    }
}
