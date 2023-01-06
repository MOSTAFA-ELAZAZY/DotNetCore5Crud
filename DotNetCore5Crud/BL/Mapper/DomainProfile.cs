using AutoMapper;
using DotNetCore5Crud.Models;
using DotNetCore5Crud.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Movie, MovieFormViewModel>();
            CreateMap<MovieFormViewModel, Movie>();

        }
    }
}
