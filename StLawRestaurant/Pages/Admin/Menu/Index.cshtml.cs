using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;

namespace StLawRestaurant.Pages.Admin.Menu
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
	{
		public ShoppingCart shoppingCart { get; set; }
		public IEnumerable<MenuItem> menuItems { get; set; }
		private readonly AppDbContext _db;

		public IndexModel(AppDbContext db)
		{
			_db = db;
		}
		public void OnGet(int id)
		{
			menuItems = _db.MenuItems;

		}


		public IActionResult DeleteMenu(int id)
		{
			var menu = _db.MenuItems.Find(id);

			if (menu == null) return Page();

			_db.MenuItems.Remove(menu);
			_db.SaveChanges();
			return RedirectToPage("Index");
		}
	}
}
