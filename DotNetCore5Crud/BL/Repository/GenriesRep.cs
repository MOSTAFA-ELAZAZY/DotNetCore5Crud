using DotNetCore5Crud.BL.Interface;
using DotNetCore5Crud.DAL;
using DotNetCore5Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.BL.Repository
{
    public class GenriesRep : IGenries
    {
        public ApplicationDbContext _context;
        public GenriesRep(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Genre> Get()
        {
            var data = _context.Genres.ToList();
            return data;
        }
    }
}
