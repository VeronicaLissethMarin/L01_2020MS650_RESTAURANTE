using System.ComponentModel.DataAnnotations;
namespace L01_2020MS650.Models
{
    public class pedidos
    {
        [Key]
        public int pedidoid { get; set; }

        public int? motoristaid { get; set; }

        public int? clienteid { get; set; }

        public int? platoid { get; set; }

        public int cantidad { get; set; }

        public decimal precio { get; set; }
    }
}
