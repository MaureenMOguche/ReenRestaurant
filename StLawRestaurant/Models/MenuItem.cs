using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StLawRestaurant.Models
{
	public class MenuItem
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string MenuName { get; set; }
		public string Description { get; set; }
		[ValidateNever]
		public string? MenuImage { get; set; }
		[Range(1, 30000, ErrorMessage = "Price should be between N1 and N30,000")]
		public decimal Price { get; set; }
	}
}
