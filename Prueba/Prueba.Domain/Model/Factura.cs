using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Model
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFactura { get; set; }
        public string NoFactura { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<FacturaDetalle> facturaDetalles { get; set; }
    }
}
