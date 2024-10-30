using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DOTNET_iyzico.Models;

namespace DOTNET_iyzico.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        // public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        // public ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> ImagePaths { get; set; } = new List<string>();

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(3)]
        public string Currency { get; set; }
        public int Stock { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemType { get; set; }
    }
}