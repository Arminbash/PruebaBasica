using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Model
{
    public class Kardex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdKardex { get; set; }
        public int IdProducto { get; set; }
        public decimal CantidadEntrada { get; set; }
        public decimal CantidadSalida { get; set; }
        public decimal Costo { get; set; }
        public int IdDocumento { get; set; }
        public string Documento { get; set; }

        public Producto Producto { get; set; }
    }
}
