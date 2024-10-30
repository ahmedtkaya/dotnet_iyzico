using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DOTNET_iyzico.Models;

namespace DOTNET_iyzico.Models
{
    [Table("ProductImages")]
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }


}
