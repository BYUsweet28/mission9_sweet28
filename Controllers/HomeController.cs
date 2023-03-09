using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository repo;
        private BookStoreContext context { get; set; }
        //repository setup
        public HomeController(IBookStoreRepository temp)
        {
            repo = temp;
        }
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {

            //initializing page num
            int pageSize = 10;

            //calculating number of pages using linq
            var x = new BooksViewModel
            {
                Book = repo.book
                .Where(b => b.Category == bookCategory || bookCategory == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumProjects = 
                        (bookCategory ==null
                            ? repo.book.Count()
                            : repo.book.Where(x => x.Category == bookCategory).Count()),
                    ProjectsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

                return View(x);
        }
    }
}
