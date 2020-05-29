using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BooksMVC.Domain;
using BooksMVC.Models.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksDataContext _dataContext;

        public BooksController(BooksDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Edit(int id)
        {
            // TODO:If it isn't there, send a 404
            var bookToEdit = await _dataContext.Books
                .SingleOrDefaultAsync(b => b.Id == id);
            //    .Select(b => new BookEdit
            //    {
            //        Id = b.Id,
            //        Title = b.Title,
            //        Author = b.Author,
            //        NumberOfPages = b.NumberOfPages,
            //        PublicationDate = new DateTime(1969, 4, 20)
            //    });

            return View(bookToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Update(BookEdit editedBook)
        {
            // Todo: Validate it, update it, redirect
            return View();
        }

        public IActionResult New()
        {
            return View(new BookCreate());//return view to add a new book
        }
        [HttpPost("/books")]
        public async Task<IActionResult> Create(BookCreate bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                return View("New", bookToAdd);
            }
            else
            {
                var book = new Book
                {
                    Title = bookToAdd.Title,
                    Author = bookToAdd.Author,
                    InInventory = true,
                    NumberOfPages = bookToAdd.NumberOfPages
                };
                _dataContext.Books.Add(book);
                await _dataContext.SaveChangesAsync();
                ViewData["flash"] = $"Book {book.Title} add as {book.Id}";
                return View("New", new BookCreate());//PRG post redirect get
            }
        }
        [HttpGet("/books/{bookId:int}")]
        public async Task<IActionResult> Details(int bookId)
        {
            
            var response = await _dataContext.Books.Where(b => b.Id == bookId)
                .Select(b => new GetSingleBookResponseModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    InInventory = b.InInventory,
                    NumberOfPages = b.NumberOfPages
                })
                .SingleOrDefaultAsync();
            if (response == null)
            {
                return NotFound("No Book with that ID");
            }
            else
            {
                return View(response);
            }
        }
        //GET /books
        //GET /books/index
        //GET
        public async Task<IActionResult> Index([FromQuery] bool showall = false)
        {
            //no model, just serilizing the domain objextsvart
            //var response = await _dataContext.Books.Where(b => b.InInventory).ToListAsync();
            //return View(response);
            ViewData["sale"] = "all books on sale";
            var response = new GetBooksResponseModel
            {
                Books = await _dataContext.Books.Where(b => b.InInventory).Select(b => new BooksResponseItemModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author
                }).ToListAsync(),
                NumberOfBooksInInventory = await _dataContext.Books.CountAsync(b => b.InInventory),
                NumberOfBooksNotInInventory = await _dataContext.Books.CountAsync(b => b.InInventory == false)
            };
            if (showall)
            {
                response.BooksNotInInventory = await _dataContext.Books.Where(b => b.InInventory == false)
                    .Select(b => new BooksResponseItemModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = b.Author
                    }).ToListAsync();
            }
            return View(response);
        }
    }
}
