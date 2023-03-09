using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class TypesViewComponent : ViewComponent
    {
        private IBookStoreRepository repo { get; set; }

        public TypesViewComponent (IBookStoreRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["bookCategory"];

            var types = repo.book
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(types);
        }
    }
}
