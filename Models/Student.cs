using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API_FinalTask.Models
{
    public class Student
    {

        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "You must enter your name")]
        [MaxLength(20, ErrorMessage = " name must be less than 20 characters"), MinLength(3, ErrorMessage = " name must be more than 3 characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "You must enter your age")]
        public int Age { get; set; }
   
        [Required(ErrorMessage = "You must enter your address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must enter your image path")]
        public string imagepath { get; set; }

        [Required(ErrorMessage = "You must enter your birth date")]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        [MyPhone]
        public string PhoneNumber { get; set; }

        [ForeignKey("Dept")]
        public int DeptID { get; set; }

        [JsonIgnore]
        public virtual Department? Dept { get; set; }

    }
}
