using System.ComponentModel.DataAnnotations;
namespace L01_2020MS650.Models
{
    public class clientes
    {
        [Key]
        public int clienteid { get; set; }

        public string nombreCliente { get; set; }

        public string direccion { get; set; }

    }
}
