using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        //Creating the basket with all necessary items
        public void AddItem (Books book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if(line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        //Calculating the total to later display to the user
        public double Calculatetotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }

    }

    //Creating a single line item for the basket
    public class BasketLineItem
    {
        public int LineID { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
    }
}
