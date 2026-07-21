using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Business
    {
        [Key]
        public Guid Id { get; set; }

       [Required(ErrorMessage = "Business name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public String? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string City { get; set; }
        public string Neighborhood { get; set; }

        public String Street { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string HouseNumber { get; set; }

        public string Comments { get; set; }

        public Guid UserId { get; set; }
        public virtual User? UserOwner { get; set; }


    }
}
