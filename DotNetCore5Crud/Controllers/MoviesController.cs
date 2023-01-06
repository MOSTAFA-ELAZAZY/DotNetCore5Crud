using DotNetCore5Crud.BL.Interface;
using DotNetCore5Crud.ViewModels;
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
        private IGenries genries;
        public MoviesController(IMovies movies,IGenries genries)
        {
            this.movies = movies;
            this.genries = genries;
        }

        public IActionResult Index()
        {
            var data = movies.Get();
            return View(data);
        }


        public IActionResult Create()
        {
            var ViewModel = new MovieFormViewModel
            {
                Genres = genries.Get()
            };
            return View();
        }

    }
}
