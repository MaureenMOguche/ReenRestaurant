using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;
using System.Security.Claims;

namespace StLawRestaurant.Pages.Admin.Orders
{
	[Authorize(Roles = "Admin")]
	public class IndexModel : PageModel
	{
		private readonly AppDbContext _db;
		public IEnumerable<OrderHeader> orderHeaders;
		public IEnumerable<OrderDetails> orderDetails;


		public IndexModel(AppDbContext db)
		{
			_db = db;
		}
		public void OnGet()
		{
			orderHeaders = _db.OrderHeaders.Include(u => u.ApplicationUser);

			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			//if (claim != null)
			//{
			//	foreach (var item in orderHeaders)
			//	{
			//		orderDetails = item.
			//	}
			//}
		}

		public IActionResult OnPostOrderDelete(int orderId)
		{
			var detailsList = _db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
			_db.OrderDetails.RemoveRange(detailsList);

			var order = _db.OrderHeaders.FirstOrDefault(id => id.Id == orderId);
			_db.OrderHeaders.Remove(order);

			_db.SaveChanges();
			return RedirectToPage("Index");
		}
	}
}
