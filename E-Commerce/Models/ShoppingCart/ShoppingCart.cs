using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using EcomGalaxy.Models.User;

namespace EcomGalaxy.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        [ValidateNever]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems { get; set; }

        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
