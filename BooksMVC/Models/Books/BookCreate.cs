using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMVC.Models.Books
{
    public class BookCreate : IValidatableObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DisplayName("Number of pages")]
        [Range(1, int.MaxValue, ErrorMessage ="Number of pages is required")]
        public int NumberOfPages { get; set; }
        // [DataType(DateType.Date)]
        public DateTime PublicationDate { get; set; } = DateTime.Now;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Title.ToLower().EndsWith("king") && Title.ToLower() == "it")
            {
                yield return new ValidationResult("Not that boo please",
                    new string[] { nameof(Title), nameof(Author)});
            }
        }
    }
}
