using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Type name is required")]
        public String? Description { get; set; }
        public String Amount { get; set; }

        [Required(ErrorMessage = "name  is required")]
        public String? Name { get; set; }

        public Boolean IsReturnable { get; set; }

        public double Price { get; set; }
        public string Comments { get; set; }
        public Guid BusinessId { get; set; }
        public virtual Business? AssociatedBusiness { get; set; }


    }
}
