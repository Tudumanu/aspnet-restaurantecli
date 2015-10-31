using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CorsClient.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nº da Mesa")]
        public int NumeroMesa { get; set; }        
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Nome Produto")]
        public string Produto { get; set; }

        [ForeignKey("Garcom")]        
        public int GarcomId { get; set; }
        public virtual Garcom Garcom { get; set; }
    }
}