using System;
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
    public class EnrollmentsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public EnrollmentsController(SchoolContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }


        // GET: api/Enrollments
        [HttpGet]
        public ActionResult GetEnrollments()
        {
            var enrollments = _unitOfWork.EnrollmentRepository.Get();
            return Ok(enrollments);
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public ActionResult<Enrollment> GetEnrollment(int id)
        {
            var enrollment = _unitOfWork.EnrollmentRepository.GetByID(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }
        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Enrollment> PostEnrollment(Enrollment enrollment)
        {
            _unitOfWork.EnrollmentRepository.Insert(enrollment);
            _unitOfWork.Save();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, enrollment);
        }

        // PUT: api/Enrollments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutEnrollment(int id, Enrollment enrollment)
        {
            _unitOfWork.EnrollmentRepository.Update(enrollment);
            _unitOfWork.Save();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, enrollment);
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            var enrollment = _unitOfWork.EnrollmentRepository.GetByID(id);
            _unitOfWork.EnrollmentRepository.Delete(id);
            _unitOfWork.Save();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _unitOfWork.EnrollmentRepository.Get(p => p.Id == id).Any();
        }
    }
}
