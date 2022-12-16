using Check1.Data.Services;
using Check1.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Check1.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync(n => n.Student);
            return View(allBooks);
        }


        /* public async Task<IActionResult> Filter(string searchString)
         {
             var allBooks = await _service.GetAllAsync(n => n.Student);

             if (!string.IsNullOrEmpty(searchString))
             {
                 //var filteredResult = allStudents.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                 var filteredResultNew = allBooks.Where(n => string.Equals(n.AttendanceStatus, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                 return View("Index", filteredResultNew);
             }

             return View("Index", allStudents);
         }
        */
        //GET: Books/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var BookDetail = await _service.GetBookByIdAsync(id);
            return View(BookDetail);
        }

        //GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var BookDropdownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.Students = new SelectList(BookDropdownsData.Students, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewBookVM Book)
        {
            if (!ModelState.IsValid)
            {
                var BookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Students = new SelectList(BookDropdownsData.Students, "Id", "Name");
                return View(Book);
            }

            await _service.AddNewBookAsync(Book);
            return RedirectToAction(nameof(Index));
        }


        //GET: Books/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var BookDetails = await _service.GetBookByIdAsync(id);
            if (BookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = BookDetails.Id,
                Name = BookDetails.Name,
                Author = BookDetails.Author,
                Publisher = BookDetails.Publisher,
                Edition = BookDetails.Edition,
                StartDate = BookDetails.StartDate,
                EndDate = BookDetails.EndDate,
                BookStatus = BookDetails.BookStatus,
                StudentId = BookDetails.StudentId,
            };

            var BookDropdownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.Students = new SelectList(BookDropdownsData.Students, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewBookVM Book)
        {
            if (id != Book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var BookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Students = new SelectList(BookDropdownsData.Students, "Id", "Name");
                return View(Book);
            }

            await _service.UpdateBookAsync(Book);
            return RedirectToAction(nameof(Index));
        }
    }
}
