using System.Collections.Generic;
using System.Linq;

namespace OnlineBookstore.Models
{
    public static class CartRepository
    {
        private static List<CartItem> cart = new List<CartItem>();

        public static List<CartItem> GetCartItems()
        {
            return cart;
        }

        public static void AddToCart(Book book)
        {
            var cartItem = cart.FirstOrDefault(c => c.Book.Id == book.Id);
            if (cartItem == null)
            {
                cart.Add(new CartItem { Book = book, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }
        }

        public static void ClearCart()
        {
            cart.Clear();
        }
    }
}
