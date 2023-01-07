using DotNetCore5Crud.Models;
using DotNetCore5Crud.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Interface
{
   public interface IMovies
    {
        IEnumerable<Movie> Get();

        public void Add(MovieFormViewModel movieFormViewModel);

        public MovieFormViewModel GetbyId(int id);

        public void Edit(MovieFormViewModel MovieFormViewModel);
    }
}
