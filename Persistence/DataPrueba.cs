using Domain.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DataPrueba
    {
        public static async Task InsertarData(ApplicationDbContextSeed contextSeed, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario { NombreCompleto = "Hector Franz", UserName = "HectorFranz", Email = "hector.montesp@gmail.com" };
                await usuarioManager.CreateAsync(usuario,"Password123$");

            }

        }
    }
}
