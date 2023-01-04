using DotNetCore5Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Interface
{
   public interface IMovies
    {
        IEnumerable<Movie> Get();
    }
}
