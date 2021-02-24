using Domain.Entities;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// clase para conexion base de datos.
    /// </summary>
    public class ApplicationDbContextSeed : IdentityDbContext<Usuario>
    {
        public ApplicationDbContextSeed(DbContextOptions options) : base(options)
        {

        }
        //override metodo existente heredado desde el padre, pero se puede cambiar la logica adecuandolo a la necesidad actual.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {//llave compuesta 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });
        }
        //mapeo de clases
        public DbSet<Comentario>Comentario { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Precio> Precio { get; set; }
        


    }
}
