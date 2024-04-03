using API_FinalTask.Models;

namespace API_FinalTask.Repositories
{
    public interface IStudentRepo
    {
        public List<Student>? GetAllStudents();
        public Student? GetStudentById(int StudentId);
        public bool CreateStudent(Student Std);
        public bool EditStudentData(int StudentId,Student Std);
        public bool DeleteStudentData(int StudentId);

    }
}

