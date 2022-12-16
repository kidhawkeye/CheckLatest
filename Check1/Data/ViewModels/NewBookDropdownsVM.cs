using Check1.Models;

namespace Check1.Data.ViewModels
{
    public class NewBookDropdownsVM
    {
        public NewBookDropdownsVM()
        {
            Students = new List<Student>();
        }
        public List<Student>? Students { get; set; }
    }
}
