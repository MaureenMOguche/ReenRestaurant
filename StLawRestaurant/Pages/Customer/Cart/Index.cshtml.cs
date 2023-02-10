using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;
using System.Security.Claims;

namespace StLawRestaurant.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _db;
		public IEnumerable<ShoppingCart> shoppingCartLisst { get; set; }
		public decimal CartTotal { get; set; }
		public OrderHeader OrderHeader { get; set; }
		public IndexModel(AppDbContext db)
		{
			_db = db;
			OrderHeader = new();
			CartTotal = 0;
		}

		public void OnGet()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


			if (claim != null)
			{
				shoppingCartLisst = _db.ShoppingCarts.Include(u => u.MenuItem)
					.Where(u => u.ApplicationUserId == claim.Value);

				foreach (var cartItem in shoppingCartLisst)
				{
					CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
				}

			}
		}

		public IActionResult OnPostPlus(int cartId)
		{
			var cart = _db.ShoppingCarts.FirstOrDefault(sh => sh.Id == cartId);
			cart.Count = cart.Count + 1;
			_db.ShoppingCarts.Update(cart);
			_db.SaveChanges();

			return RedirectToPage("Index");
		}

		public IActionResult OnPostMinus(int cartId)
		{
			var cart = _db.ShoppingCarts.FirstOrDefault(sh => sh.Id == cartId);
			cart.Count = cart.Count - 1;
			_db.ShoppingCarts.Update(cart);
			_db.SaveChanges();

			return RedirectToPage("Index");
		}

		public IActionResult OnPostDelete(int cartId)
		{
			var cartItem = _db.ShoppingCarts.FirstOrDefault(sh => sh.Id == cartId);
			_db.ShoppingCarts.Remove(cartItem);
			_db.SaveChanges();

			return RedirectToPage("/Customer/Cart/Index");
		}

		public IActionResult OnPostOrder()
		{
			//Getting the Identity of the user
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			if (claim != null)
			{
				//Retrieve the Shopping cart list of the user
				shoppingCartLisst = _db.ShoppingCarts
					.Include(u => u.MenuItem)
					.Where(u => u.ApplicationUserId == claim.Value).ToList();


				foreach (var cartItem in shoppingCartLisst)
				{
					OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
				}

				OrderHeader.ApplicationUserId = claim.Value;

				_db.OrderHeaders.Add(OrderHeader);
				_db.SaveChanges();



				//Adding Order Details
				foreach (var item in shoppingCartLisst)
				{
					OrderDetails orderDetails = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Count = item.Count,
					};

					_db.OrderDetails.Add(orderDetails);

				}

				_db.ShoppingCarts.RemoveRange(shoppingCartLisst);
				_db.SaveChanges();
			}

			return RedirectToPage("OrderSuccess");
		}

	}
}
