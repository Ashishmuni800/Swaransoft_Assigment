using System.ComponentModel.DataAnnotations;

namespace Swaransoft_Assigment.Models
{
    public class stateCity
    {
        [Key]
        public int CityId { get; set; }
        public int  StateId { get; set; }
        public string? City { get; set; }            
    }
}
