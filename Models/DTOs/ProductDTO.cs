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
        public string? Description { get; set; }
        public string? ClassName { get; set; }
    }
}