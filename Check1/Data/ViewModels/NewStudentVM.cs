using System.ComponentModel.DataAnnotations;

namespace Check1.Data.ViewModels
{
    public class NewStudentVM
    {
        public int Id { get; set; }

        [Display(Name = "Employee name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //Relationships
        

        [Display(Name = "Select Courses(s)")]
        [Required(ErrorMessage = "Student Courses(s) is required")]
        public List<int> CourseIds { get; set; }
    }
}
