using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ProduzirAPI.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public required string Description { get; set; }
        public required decimal Weight { get; set; }

        public required int ClassId { get; set; }
        public required string ClassName { get; set; }
        public string? ImageUrl { get; set; }
    }
}