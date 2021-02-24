using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CursoInstructor
    {
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; }
        public Guid InstructorId { get; set; }
        public Instructor Insctructor { get; set; }
    }
}
