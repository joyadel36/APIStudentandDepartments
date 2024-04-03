using API_FinalTask.DTOs;
using API_FinalTask.Models;
using API_FinalTask.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_FinalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase

    {
        private readonly IDepartmentRepo _DeptRepo;
        public DepartmentController(IDepartmentRepo deptRepo)
        {
            _DeptRepo = deptRepo;
        }
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            List<Department>? Departments = _DeptRepo.GetAllDepartments();
            if (Departments != null)
            {
                List<DeptWithStudents> Deptsforui = new List<DeptWithStudents>();
              
                foreach (Department DeptFromDB in Departments)
                {

                    
                    Deptsforui.Add(new DeptWithStudents()
                    {
                        Id=DeptFromDB.Id,
                        Name = DeptFromDB.Name,
                        MgrName = DeptFromDB.MgrName,
                        StudentsNames = []

                    });
                
                }

                return Ok(Deptsforui);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("/details/{id:int}", Name = "GetOneDept")]
        public IActionResult GetDepartmentById(int id)
        {
            Department? DeptFromDB = _DeptRepo.GetDepartmentById(id);
            if (DeptFromDB != null)
            {


                DeptWithStudents DeptForUI = new DeptWithStudents();
                DeptForUI.Id = DeptFromDB.Id;
                DeptForUI.Name = DeptFromDB.Name;
                DeptForUI.MgrName = DeptFromDB.MgrName;
                foreach (var dept in DeptFromDB.Students)
                {
                    DeptForUI.StudentsNames.Add(dept.Name);
                }
                return Ok(DeptForUI);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddDepartment(Department Dept)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool created = _DeptRepo.CreateDepartment(Dept);

                    if (created)
                    {
                        string url = Url.Link("GetOneDept", new { id = Dept.Id });
                        return Created(url, Dept);
                    }
                    else
                    {
                        return BadRequest("error!!");
                    }
                }
                else
                {
                    return BadRequest(new { msg = "error!!", Errors = ModelState });
                }
            }
            catch
            {
                return BadRequest("error!!");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditStudent(int id, Department Dept)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    bool Edited = _DeptRepo.EditDepartmenttData(id, Dept);

                    if (Edited)
                    {

                        return Ok();
                    }
                    else
                    {
                        return BadRequest("error!!");
                    }
                }
                else
                {
                    return BadRequest(new { msg = "error!!", Errors = ModelState });
                }
            }
            catch
            {
                return BadRequest("error!!");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartmentData(int id)
        {
            bool Deleted = _DeptRepo.DeleteDepartmentData(id);

            if (Deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest("error!!");
            }

        }
    }
}
