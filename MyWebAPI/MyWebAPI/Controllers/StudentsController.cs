using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyEntities;
using MyWebAPI.DAL;
using MyWebAPI.Data;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public StudentsController(SchoolContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }
        // GET: api/Students
        [HttpGet]
        public ActionResult GetStudents()
        {
            var students = _unitOfWork.StudentRepository.Get();
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _unitOfWork.StudentRepository.GetByID(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            _unitOfWork.StudentRepository.Insert(student);
            _unitOfWork.Save();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            _unitOfWork.StudentRepository.Update(student);
            _unitOfWork.Save();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _unitOfWork.StudentRepository.GetByID(id);
            _unitOfWork.StudentRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _unitOfWork.StudentRepository.Get(p => p.Id == id).Any();
        }
    }
}
