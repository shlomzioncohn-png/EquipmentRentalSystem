using System.ComponentModel.DataAnnotations;

namespace Core.Resources
{
    public class UserResource
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }


        public string Email { get; set; }
        public string Password { get; set; }



    }
}