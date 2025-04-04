using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [Required]
        public string SKU { get; set; }
        
        [Required]
        public int CurrentStock { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property
        public virtual ICollection<StockMovement> StockMovements { get; set; }
    }
}