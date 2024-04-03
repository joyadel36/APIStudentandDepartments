using API_FinalTask.Models;

namespace API_FinalTask.Repositories
{
    public interface IDepartmentRepo
    {
        public List<Department>? GetAllDepartments();
        public Department? GetDepartmentById(int DepartmentId);
        public bool CreateDepartment(Department Dept);
        public bool EditDepartmenttData(int DepartmentId, Department Dept);
        public bool DeleteDepartmentData(int DepartmentId);
    }
}
