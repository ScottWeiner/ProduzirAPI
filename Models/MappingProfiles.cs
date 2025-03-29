using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProduzirAPI.Models.Domain;
using ProduzirAPI.Models.DTOs;

namespace ProduzirAPI.Models
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            RecognizePrefixes("Product");

            CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.ProductClass.Name))
            .ForMember(dest => dest.ClassId, opt => opt.MapFrom(src => src.ProductClass.Id))
            .ReverseMap()
            ;




        }
    }
}