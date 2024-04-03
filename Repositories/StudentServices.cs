using API_FinalTask.Models;
using Microsoft.EntityFrameworkCore;

namespace API_FinalTask.Repositories
{
    public class StudentServices : IStudentRepo
    {
        public StudentContext _Context { get; }
        public StudentServices(StudentContext context)
        {
            _Context = context;
        }
        public bool DeleteStudentData(int StudentId)
        {
            try
            {
                Student? TempStd = _Context.Students.Where(s => s.ID == StudentId).FirstOrDefault();
                if (TempStd != null)
                {

                    _Context.Students.Remove(TempStd);
                    _Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }

        }

        public bool EditStudentData(int StudentId, Student Std)
        {
            try
            {
                Student? TempStd = _Context.Students.Include(s => s.Dept).Where(s => s.ID == StudentId).FirstOrDefault();
                if (TempStd != null && Std != null)
                {
                    TempStd.Name = Std.Name;
                    TempStd.Address = Std.Address;
                    TempStd.Age = Std.Age;
                    TempStd.DeptID = Std.DeptID;
                    TempStd.imagepath = Std.imagepath;
                    TempStd.PhoneNumber = Std.PhoneNumber;
                    TempStd.BirthDate = Std.BirthDate;


                    _Context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Student>? GetAllStudents()
        {
            return _Context.Students.Include(s => s.Dept).ToList();
        }

        public Student? GetStudentById(int StudentId)
        {
            return _Context.Students.Include(s => s.Dept).Where(s => s.ID == StudentId).FirstOrDefault();
        }
        public bool CreateStudent(Student Std)
        {
            try
            {
                if (Std != null)
                {
                    _Context.Students.Add(Std);
                    _Context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
