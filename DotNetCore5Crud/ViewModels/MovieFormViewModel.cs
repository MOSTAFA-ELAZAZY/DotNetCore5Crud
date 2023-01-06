using DotNetCore5Crud.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore5Crud.ViewModels
{
    public class MovieFormViewModel
    {
        [Required,StringLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        [Range(1,10)]
        public double Rate { get; set; }
        [Required, StringLength(2500)]
        public string StoreLine { get; set; }
        public byte[] Poster { get; set; }
        [Display(Name ="Genre")]
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
