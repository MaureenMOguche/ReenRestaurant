using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace StLawRestaurant.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		[ValidateNever]
		public OrderHeader OrderHeader { get; set; }


		[Required]
		public int MenuItemId { get; set; }
		[ForeignKey("MenuItemId")]
		public MenuItem MenuItem { get; set; }

		public int Count { get; set; }
	}
}
