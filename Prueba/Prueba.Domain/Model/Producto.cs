using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Model
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Kardex> Kardexs { get; set; }
        public virtual ICollection<CompraDetalle> compraDetalles { get; set; }
        public virtual ICollection<FacturaDetalle> facturaDetalles { get; set; }
    }
}
