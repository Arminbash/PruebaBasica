using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Model
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompra { get; set; }
        public string Factura { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<CompraDetalle> compraDetalles { get; set; }
    }
}
