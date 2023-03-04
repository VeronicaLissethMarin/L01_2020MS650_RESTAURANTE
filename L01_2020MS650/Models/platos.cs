using System.ComponentModel.DataAnnotations;
namespace L01_2020MS650.Models
{
    public class platos
    {
        [Key]
        public int platoid { get; set; }

        public string nombreplato { get; set; }

        public decimal precio { get; set; }
    }
}
