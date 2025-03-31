using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProduzirAPI.Models.DTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public required int Number { get; set; }
        public required string Description { get; set; }
        public required int ClassId { get; set; }

        public required decimal Weight { get; set; }
        public string? ImageUrl { get; set; }
    }
}