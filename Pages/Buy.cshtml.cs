using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Infrastructure;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStore.Pages
{
    public class BuyModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }

        public BuyModel (IBookStoreRepository temp)
        {
            repo = temp;
        }

        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }
        //sending data through a get request
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }
        //adding books to the cart on a post request and redirecting to the correct screen
        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Books b = repo.book.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage (new {ReturnUrl = returnUrl});
        }
    }
}
