using System;
using MyEntities;
using MyWebAPI.Data;

namespace MyWebAPI.DAL
{
    public class UnitOfWork : IDisposable
    {

        private readonly SchoolContext _context;

        public UnitOfWork(SchoolContext context)
        {
            _context = context;
        }

        private GenericRepository<Course> _courseRepository;
        private GenericRepository<Enrollment> _enrollmentRepository;
        private GenericRepository<Student> _studentRepository;

        public GenericRepository<Course> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new GenericRepository<Course>(_context);
                }
                return _courseRepository;
            }
        }


        public GenericRepository<Enrollment> EnrollmentRepository
        {
            get
            {
                _enrollmentRepository ??= new GenericRepository<Enrollment>(_context);
                return _enrollmentRepository;
            }
        }

        public GenericRepository<Student> StudentRepository
        {
            get
            {
                _studentRepository ??= new GenericRepository<Student>(_context);
                return _studentRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
