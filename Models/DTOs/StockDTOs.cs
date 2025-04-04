using System;

namespace WarehouseAPI.Models.DTOs
{
    public class StockMovementDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
    }
    
    public class StockMovementHistoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; }
        public string Notes { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}