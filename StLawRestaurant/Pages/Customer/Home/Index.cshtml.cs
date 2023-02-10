using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;

namespace StLawRestaurant.Pages.Customer.Home
{
	public class IndexModel : PageModel
	{
		[BindProperty]
		public ShoppingCart shoppingCart { get; set; }
		public IEnumerable<MenuItem> MenuItemList { get; set; }
		public int Count { get; set; }
		private readonly AppDbContext _db;

		public IndexModel(AppDbContext db)
		{
			_db = db;
		}

		public void OnGet(int id)
		{
			MenuItemList = _db.MenuItems.ToList();
		}

	}
}
