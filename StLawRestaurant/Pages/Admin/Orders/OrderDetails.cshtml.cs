using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;

namespace StLawRestaurant.Pages.Admin.Orders
{
	public class OrderDetailsModel : PageModel
	{
		public IEnumerable<OrderDetails> orderDetailsList;
		private readonly AppDbContext _db;
		public OrderDetailsModel(AppDbContext db)
		{
			_db = db;
		}
		public void OnGet(int orderId)
		{
			orderDetailsList = _db.OrderDetails.Include(u => u.MenuItem)
				.Where(u => u.OrderId == orderId);
		}
	}
}
