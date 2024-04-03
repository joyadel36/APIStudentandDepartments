using API_FinalTask.Models;
using Microsoft.EntityFrameworkCore;

namespace API_FinalTask.Repositories
{
    public class DepartmentServices : IDepartmentRepo
    {
        public StudentContext _Context { get; }
        public DepartmentServices(StudentContext context)
        {
            _Context = context;
        }
        public bool DeleteDepartmentData(int DepartmentId)
        {
            try
            {
                Department? TempDept = _Context.Departments.Include(d => d.Students).Where(d => d.Id == DepartmentId).FirstOrDefault();
                if (TempDept != null)
                {

                    _Context.Departments.Remove(TempDept);
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

        public bool EditDepartmenttData(int DepartmentId, Department Dept)
        {
            try
            {
                Department? TempDept = _Context.Departments.Include(d => d.Students).Where(d => d.Id == DepartmentId).FirstOrDefault();
                if (TempDept != null && Dept != null)
                {
                    TempDept.Name = Dept.Name;
                    TempDept.MgrName = Dept.MgrName;
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

        public List<Department> GetAllDepartments()
        {
            return _Context.Departments.Include(d => d.Students).ToList();
        }

        public Department GetDepartmentById(int DepartmentId)
        {
            return _Context.Departments.Include(d => d.Students).Where(d => d.Id == DepartmentId).FirstOrDefault();

        }
        public bool CreateDepartment(Department Dept)
        {
            try { 
            if (Dept != null)
            {
                _Context.Departments.Add(Dept);
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
