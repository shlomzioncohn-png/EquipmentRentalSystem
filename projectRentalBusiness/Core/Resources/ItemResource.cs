using System.ComponentModel.DataAnnotations;
using Core.Models;

namespace Core.Resources
{
    public class ItemResource
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "name name is required")]
        public String? Name { get; set; }


        [Required(ErrorMessage = "Type name is required")]
        public string Description { get; set; }

        public String Amount { get; set; }
        public string Comments { get; set; }


        public double Price { get; set; }


        public Boolean IsReturnable { get; set; }


        public Guid BusinessId { get; set; }

        public string? BusinessName { get; set; }

        public string? BusinessCity { get; set; }

    }
}