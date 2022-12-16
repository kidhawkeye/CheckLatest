using Check1.Data.Base;
using Check1.Data.Enums;
using Check1.Data.ViewModels;
using Check1.Models;
using Microsoft.EntityFrameworkCore;
/*
 *      public string Name { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public BookStatus? BookStatus { get; set; }

        public int StudentId { get; set; }
 */
namespace Check1.Data.Services
{
    public class BooksService:EntityBaseRepository<Book>,IBooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Name = data.Name,
                Author = data.Author,
                Publisher = data.Publisher,
                Edition=data.Edition,
                StartDate=data.StartDate,
                EndDate=data.EndDate,
                BookStatus=data.BookStatus,
                StudentId = data.StudentId,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var books = await _context.Books
                .Include(c => c.Student)
                .FirstOrDefaultAsync(n => n.Id == id);

            return books;
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM()
            {
                Students = await _context.Students.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {
                dbBook.Name = data.Name;
                dbBook.Author = data.Author;
                dbBook.Publisher = data.Publisher;
                dbBook.Edition = data.Edition;
                dbBook.StartDate = data.StartDate;
                dbBook.EndDate = data.EndDate;
                dbBook.BookStatus = data.BookStatus;
                dbBook.StudentId = data.StudentId;
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();
        }
    }
}
