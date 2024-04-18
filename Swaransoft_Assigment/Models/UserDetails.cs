using Microsoft.AspNetCore.Http; // Import the necessary namespace

namespace Swaransoft_Assigment.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Remarks { get; set; }

        // Include the uploadphoto property with type IFormFile
        public string uploadphoto { get; set; }
    }
}