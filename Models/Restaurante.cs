using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorsClient.Models
{
    [Table("Restaurante")]
    public class Restaurante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}