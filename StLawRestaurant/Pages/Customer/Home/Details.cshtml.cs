using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;
using System.Security.Claims;

namespace StLawRestaurant.Pages.Customer.Home
{
	[Authorize]
	public class DetailsModel : PageModel
	{
		private readonly AppDbContext _db;
		public DetailsModel(AppDbContext db)
		{
			_db = db;
		}

		[BindProperty]
		public ShoppingCart ShoppingCart { get; set; }

		public void OnGet(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCart = new()
			{
				ApplicationUserId = claim.Value,
				MenuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id),
				MenuItemId = id
			};
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				var shoppingCartfromDb = _db.ShoppingCarts.FirstOrDefault(
					u => u.ApplicationUserId == ShoppingCart.ApplicationUserId &&
					u.MenuItemId == ShoppingCart.MenuItemId);

				if (shoppingCartfromDb == null)
				{
					_db.ShoppingCarts.Add(ShoppingCart);
					_db.SaveChanges();
				}
				else
				{
					shoppingCartfromDb.Count += ShoppingCart.Count;
					_db.ShoppingCarts.Update(shoppingCartfromDb);
					_db.SaveChanges();
				}

				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
