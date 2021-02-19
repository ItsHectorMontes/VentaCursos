using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CursoInstructor
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public int InstructorId { get; set; }
        public Instructor Insctructor { get; set; }
    }
}
