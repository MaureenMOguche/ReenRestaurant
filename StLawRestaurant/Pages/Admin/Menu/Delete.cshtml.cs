using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;

namespace StLawRestaurant.Pages.Admin.Menu
{
	public class DeleteModel : PageModel
	{
		[BindProperty]
		public MenuItem menuItem { get; set; }
		private readonly AppDbContext _db;
		private readonly IWebHostEnvironment _hostEnvironment;

		public DeleteModel(AppDbContext db, IWebHostEnvironment hostEnvironment)
		{
			_db = db;
			_hostEnvironment = hostEnvironment;
		}


		public void OnGet(int id)
		{
			menuItem = _db.MenuItems.Find(id);

		}



		public async Task<IActionResult> OnPost(int id)
		{
			var menufromDb = _db.MenuItems.Find(id);

			var wwwpath = _hostEnvironment.WebRootPath;

			//Delete old image
			var oldImagePath = Path.Combine(wwwpath, menufromDb.MenuImage.TrimStart('\\'));

			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}



			_db.MenuItems.Remove(menufromDb);
			await _db.SaveChangesAsync();
			TempData["Success"] = "Successfully deleted menu item";
			return RedirectToPage("Index");
		}
	}
}
