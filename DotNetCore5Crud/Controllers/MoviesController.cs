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
        private List<string> _AllowedExtintos = new List<string> { ".jpg", ".png" };
        private long _MaxSize = 1048576;
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
            return View("MovieForm", ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = genries.Get();
                return View("MovieForm", model);
            }

            var files = Request.Form.Files; 
              
            if (!files.Any())
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Please select movive Poster");
                return View("MovieForm", model);
            }

            var Poster = files.FirstOrDefault();
            if (!_AllowedExtintos.Contains(Path.GetExtension(Poster.FileName).ToLower()))
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Only Jpg , PNG are allowed ");
                return View("MovieForm", model);
            }

            if(Poster.Length > _MaxSize)
            {
                model.Genres = genries.Get();
                ModelState.AddModelError("Poster", "Poster Can't Be More Than 1 MB!" );
                return View("MovieForm", model);
            }

            using var DataStrean = new MemoryStream();
            Poster.CopyToAsync(DataStrean);

            model.Poster = DataStrean.ToArray();

            movies.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {

           var data= movies.GetbyId(Id);

            return View("MovieForm", data);

        }

        [HttpPost]
        public IActionResult Edit(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = genries.Get();
                return View("MovieForm", model);
            }


            var feiles = Request.Form.Files;
            if (feiles.Any())
            {
                var Poster = feiles.FirstOrDefault();
                using var datastream = new MemoryStream();
                Poster.CopyToAsync(datastream);

                model.Poster = datastream.ToArray();
                if (!_AllowedExtintos.Contains(Path.GetExtension(Poster.FileName).ToLower()))
                {
                    model.Genres = genries.Get();
                    ModelState.AddModelError("Poster", "Only Jpg , PNG are allowed ");
                    return View("MovieForm", model);
                }

                if (Poster.Length > _MaxSize)
                {
                    model.Genres = genries.Get();
                    ModelState.AddModelError("Poster", "Poster Can't Be More Than 1 MB!");
                    return View("MovieForm", model);
                }

                model.Poster = datastream.ToArray();
            }


            var movie = movies.GetbyId(model.Id);
            if (movie == null)
            {
                return NotFound();
            }

            movies.Edit(model);
            return RedirectToAction("Index");
        }

    }
}
