using System;
using System.Collections.Generic;
using System.Text;

namespace Tarea.Models
{
    public class Producto
    {
        public int idProducto { set; get; }
        public string nombreProducto { set; get; }
        public string tipoProducto { set; get; }
        public double precioUnidad { set; get; }
        public double precioUnidadsinIva { set; get; }
        public double totalProducto { set; get; }
    }
}
