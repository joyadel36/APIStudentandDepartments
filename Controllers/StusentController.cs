using API_FinalTask.DTOs;
using API_FinalTask.Filters;
using API_FinalTask.Models;
using API_FinalTask.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_FinalTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StusentController : ControllerBase
    {
        private readonly IStudentRepo _StdRepo;
        public StusentController(IStudentRepo StdRepo)
        {
            _StdRepo = StdRepo;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            List<Student>? Students = _StdRepo.GetAllStudents();
            if (Students != null)
            {
                List<StudentWithDept> studentsforui = new List<StudentWithDept>();

                foreach (Student StdFromDB in Students)
                {
                    studentsforui.Add(new StudentWithDept()
                    {
                        Name = StdFromDB.Name,
                        Age = StdFromDB.Age,
                        Address = StdFromDB.Address,
                        BirthDate = StdFromDB.BirthDate,
                        PhoneNumber = StdFromDB.PhoneNumber,
                        imagepath = StdFromDB.imagepath,
                        DeptName = StdFromDB.Dept.Name,
                        id = StdFromDB.ID


                });
                }

                return Ok(studentsforui);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("/detailss/{id:int}", Name = "GetOneStd")]
        public IActionResult GetStudentById(int id)
        {
            Student? StdFromDB = _StdRepo.GetStudentById(id);
            if (StdFromDB != null)
            {

                StudentWithDept StdForUI = new StudentWithDept();
                StdForUI.Name = StdFromDB.Name;
                StdForUI.Age = StdFromDB.Age;
                StdForUI.Address = StdFromDB.Address;
                StdForUI.BirthDate = StdFromDB.BirthDate;
                StdForUI.PhoneNumber = StdFromDB.PhoneNumber;
                StdForUI.imagepath = StdFromDB.imagepath;
                StdForUI.DeptName = StdFromDB.Dept.Name;
                StdForUI.id = StdFromDB.ID;


                return Ok(StdForUI);

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidationDate]
        public IActionResult AddStudent(Student Std)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool created = _StdRepo.CreateStudent(Std);

                    if (created)
                    {
                        string url = Url.Link("GetOneStd", new { id = Std.ID });
                        return Created(url, Std);
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
        public IActionResult EditStudent(int id, Student std)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool Edited = _StdRepo.EditStudentData(id, std);

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
        public IActionResult DeleteStudent(int id)
        {
            bool Deleted = _StdRepo.DeleteStudentData(id);

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
