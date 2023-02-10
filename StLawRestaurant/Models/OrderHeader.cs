using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StLawRestaurant.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string ApplicationUserId { get; set; }

		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }

		public decimal OrderTotal { get; set; }
	}
}
