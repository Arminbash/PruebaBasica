using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Model
{
    public class CompraDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompraDetalle { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }

        public Compra Compra { get; set; }
        public Producto producto { get; set; }
    }
}
