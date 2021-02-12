using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyEntities;
using MyWebAPI.DAL;
using MyWebAPI.Data;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CoursesController(SchoolContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }


        // GET: api/Courses
        [HttpGet]
        public ActionResult GetCourses()
        {
            var courses = _unitOfWork.CourseRepository.Get();
            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = _unitOfWork.CourseRepository.GetByID(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Course> PostCourse(Course course)
        {
            _unitOfWork.CourseRepository.Insert(course);
            _unitOfWork.Save();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, Course course)
        {
            _unitOfWork.CourseRepository.Update(course);
            _unitOfWork.Save();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _unitOfWork.CourseRepository.GetByID(id);
            _unitOfWork.CourseRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _unitOfWork.CourseRepository.Get(p => p.Id == id).Any();
        }
    }
}
