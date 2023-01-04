using DotNetCore5Crud.BL.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.Controllers
{ 
    public class MoviesController : Controller
    {
        private IMovies movies;
        public MoviesController(IMovies movies)
        {
            this.movies = movies;
        }

        public IActionResult Index()
        {
            var data = movies.Get();
            return View(data);
        }

       
    }
}
