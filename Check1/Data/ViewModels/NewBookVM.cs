using Check1.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Check1.Data.ViewModels
{
    public class NewBookVM
    {
        public int Id { get; set; }
        [Display(Name="Book Name")]
        [Required(ErrorMessage="Book is Required")]
        public string Name { get; set; }
        [Display(Name = "Author Name")]
        [Required(ErrorMessage = "Author is Required")]
        public string Author { get; set; }
        [Display(Name = "Publisher Name")]
        [Required(ErrorMessage = "Publisher is Required")]
        public string Publisher { get; set; }
        [Display(Name = "Edition Version")]
        [Required(ErrorMessage = "Edition Version is Required")]
        public int Edition { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [Display(Name = "Book Status")]
        [Required(ErrorMessage = "Book Status is Required")]
        public BookStatus? BookStatus { get; set; }
        [Display(Name = "Select a Student")]
        [Required(ErrorMessage = "Student is Required")]

        public int StudentId { get; set; }
    }
}
