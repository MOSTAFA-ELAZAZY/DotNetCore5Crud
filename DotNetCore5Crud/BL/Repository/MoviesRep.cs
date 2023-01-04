   using DotNetCore5Crud.BL.Interface;
using DotNetCore5Crud.DAL;
using DotNetCore5Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Repository
{
    public class MoviesRep : IMovies
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MoviesRep(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Movie> Get()
        {
            var Movies = _applicationDbContext.Movies.ToList();
            return Movies;
        }
    }
}
