using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace API_FinalTask.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
       
        [Required(ErrorMessage = "You must enter your name")]
         public string  Name { get; set; }
       
        [Required(ErrorMessage = "You must enter your name")]
        [MaxLength(20, ErrorMessage = " name must be less than 20 characters"), MinLength(3, ErrorMessage = " name must be more than 3 characters")]
        public string  MgrName { get; set; }
       
        [JsonIgnore]
        public virtual ICollection<Student>? Students { get; set; }
    }
}
