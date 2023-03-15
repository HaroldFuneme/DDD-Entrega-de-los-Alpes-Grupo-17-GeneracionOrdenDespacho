using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneracionOrdenDespacho.Persistencia.Modelos
{
    [Table("ordenes")]
    public class Orden
    {
        [Key]
        public Guid OrdenId { get; set; } = Guid.NewGuid();

        [MaxLength(255)]
        public string Usuario { get; set; }

        [MaxLength(1024)]
        public string DireccionUsuario { get; set; }

        #region Propiedades de navegación

        public virtual ICollection<DespachoUltimaMilla> Despachos { get; set; }

        #endregion
    }
}
