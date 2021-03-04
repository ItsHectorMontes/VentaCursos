﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Cursos.Queries.DtoCourse
{
    public class InstructorDto
    {
        public Guid InstructorId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Grado { get; set; }
        public byte[] FotoPerfil { get; set; }
    }
}
