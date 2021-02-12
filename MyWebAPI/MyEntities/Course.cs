using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEntities
{
    public class Course : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
