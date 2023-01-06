using DotNetCore5Crud.BL.Interface;
using DotNetCore5Crud.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = genries.Get();
                return View(model);
            }

            var files = Request.Form.Files; 
              
            if (!files.Any())
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Please select movive Poster");
                return View(model);
            }

            var Poster = files.FirstOrDefault();
            var AllowedExtintos = new List<string> { ".jpg", ".png" };
            if (!AllowedExtintos.Contains(Path.GetExtension(Poster.FileName).ToLower()))
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Only Jpg , PNG are allowed ");
                return View(model);
            }

            if(Poster.Length > 1048576)
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Poster Can't Be More Than 1 MB!" );
                return View(model);
            }

            using var DataStrean = new MemoryStream();
            Poster.CopyTo(DataStrean);

            model.Poster = DataStrean.ToArray();

            movies.Add(model);
            return RedirectToAction("Index");
        }

    }
}
