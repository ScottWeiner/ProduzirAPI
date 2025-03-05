using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProduzirAPI.Models.Domain
{
    [Table("PRODUCTS")]

    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required int Number { get; set; }
        public required string Description { get; set; }
        public required decimal Weight { get; set; }

        public required ProductClass ProductClass { get; set; }
        public int ProductClassId { get; set; }

        public string? ImageUrl { get; set; }

    }
}