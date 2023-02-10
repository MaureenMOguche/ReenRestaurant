using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StLawRestaurant.DataAccess;
using StLawRestaurant.Models;

namespace StLawRestaurant.Pages.Admin.Menu
{
	public class UpsertModel : PageModel
	{
		private readonly AppDbContext _db;
		private readonly IWebHostEnvironment _hostEnvironment;
		[BindProperty]
		public MenuItem menuItem { get; set; }

		public UpsertModel(AppDbContext db, IWebHostEnvironment hostEnvironment)
		{
			_db = db;
			menuItem = new();
			_hostEnvironment = hostEnvironment;
		}


		public void OnGet(int? id)
		{
			if (id != null)
			{
				menuItem = _db.MenuItems.FirstOrDefault(x => x.Id == id);
			}
		}



		public async Task<IActionResult> OnPost(IFormFile? menuImage)
		{
			string webrootpath = _hostEnvironment.WebRootPath;
			//var menuImage = HttpContext.Request.Form.Files;

			if (menuItem.Id == 0)
			{
				//create
				var uploads = Path.Combine(webrootpath, @"images\menuImages");
				string filename = Guid.NewGuid().ToString();
				var extension = Path.GetExtension(menuImage.FileName);
				using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
				{
					menuImage.CopyTo(fileStream);
				}
				menuItem.MenuImage = @"\images\menuImages\" + filename + extension;

				_db.MenuItems.Add(menuItem);
				_db.SaveChanges();
				TempData["Success"] = "Successfully added new menu item";

			}
			else
			{
				var menuItemFromDb = _db.MenuItems.FirstOrDefault(x => x.Id == menuItem.Id);

				if (menuImage != null)
				{
					var uploads = Path.Combine(webrootpath, @"images\menuImages");
					string filename = Guid.NewGuid().ToString();
					var extension = Path.GetExtension(menuImage.FileName);

					//delete old image
					var oldImagePath = Path.Combine(webrootpath, menuItemFromDb.MenuImage.TrimStart('\\'));

					System.IO.File.Delete(oldImagePath);


					//if (System.IO.File.Exists(oldImagePath))
					//{
					//	System.IO.File.Delete(oldImagePath);
					//}


					using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
					{
						menuImage.CopyTo(fileStream);
					}
					menuItemFromDb.MenuImage = @"\images\menuImages\" + filename + extension;
				}
				//edit
				if (ModelState.IsValid)
				{

					//if (menuItemFromDb != null)
					//{
					menuItemFromDb.MenuName = menuItem.MenuName;
					menuItemFromDb.Price = menuItem.Price;
					menuItemFromDb.Description = menuItem.Description;


					_db.MenuItems.Update(menuItemFromDb);
					_db.SaveChanges();
					TempData["Success"] = "Successfully edited menu item";
					//}
				}


			}

			return RedirectToPage("Index");
		}
	}
}
