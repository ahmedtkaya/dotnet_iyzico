using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DOTNET_iyzico.Models;

namespace DOTNET_iyzico.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid uuid { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ICollection<ProductCategory> Categories { get; set; } = new List<ProductCategory>();

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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}