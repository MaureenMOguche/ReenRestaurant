using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StLawRestaurant.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }

        [ForeignKey("MenuItemId")]
        [ValidateNever]
        public MenuItem MenuItem { get; set; }

        [Range(1, 100, ErrorMessage = "Please enter a value between 1 - 100")]
        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


        public void IncrementCount(int id, int count)
        {

        }
    }
}
