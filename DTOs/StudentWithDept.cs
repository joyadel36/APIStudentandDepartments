using API_FinalTask.Models;
using System.ComponentModel.DataAnnotations;

namespace API_FinalTask.DTOs
{
    public class StudentWithDept
    {
        public string Name { get; set; }

        public int id { get; set; }
        public int Age { get; set; }

        public string Address { get; set; }

        public string imagepath { get; set; }

        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        [MyPhone]
        public string PhoneNumber { get; set; }

        public string DeptName { get; set; }

    }
}
