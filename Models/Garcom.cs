using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorsClient.Models
{
    [Table("Garcom")]
    public class Garcom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Range(1,100)]
        public int Idade { get; set; }

        [ForeignKey("Restaurante")]        
        public int RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}