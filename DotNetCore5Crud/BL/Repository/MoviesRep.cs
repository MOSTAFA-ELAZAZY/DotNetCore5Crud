using AutoMapper;
using DotNetCore5Crud.BL.Interface;
using DotNetCore5Crud.DAL;
using DotNetCore5Crud.Models;
using DotNetCore5Crud.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Repository
{
    public class MoviesRep : IMovies
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper mapper;

        public MoviesRep(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public void Add(MovieFormViewModel movieFormViewModel)
        {
            var data = mapper.Map<Movie>(movieFormViewModel);
            _applicationDbContext.Movies.Add(data);
            _applicationDbContext.SaveChanges();
        }

        public void Edit(MovieFormViewModel MovieFormViewModel)
        {
            var data = mapper.Map<Movie>(MovieFormViewModel);
            _applicationDbContext.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }

        public IEnumerable<Movie> Get()
        {
            var Movies = _applicationDbContext.Movies.ToList();
            return Movies;
        }

        public MovieFormViewModel GetbyId(int id)
        {
            var data = _applicationDbContext.Movies.Where(a => a.Id == id)
                .Select(a => new MovieFormViewModel
                {
                    Id=a.Id,
                    Poster=a.Poster,
                    Rate=a.Rate,
                    StoreLine=a.StoreLine,
                    Title=a.Title,
                    Year=a.Year,
                    GenreId=a.GenreId,
                  Genres=_applicationDbContext.Genres.OrderBy(m=>m.Name).ToList()
                }).FirstOrDefault();

            return data;
        }
    }
}
