using Diversos.Infraestructure.Contexto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading;
using System;
using Diversos.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Diversos.Infraestructure.Repositories
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario, int>, IUsuarioRepositorio
    {
        private readonly IMapper _mapper;
        private IQueryable<Usuario> dbQuery;

        public UsuarioRepositorio(IMapper mapper, DiversosContext context) : base(context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            dbQuery = context.Set<Usuario>().AsQueryable<Usuario>();
        }

        public async Task<ResponseResult> PostValidarAsync(Usuario modelo)
        {
            var usuario = await dbQuery
                .Include(user => user.Rol)
                .Include(user => user.Persona)
                .FirstOrDefaultAsync(x => x.Acceso.ToLower() == modelo.Acceso.ToLower());

            if (usuario == null)
                return new ResponseResult("Nombre de usuario no encontrado", false);

            // Encripto la clave
            string clave = Encriptador.AES.Decrypt(usuario.PasswordHash, usuario.PasswordSalt);

            // Valido la clave del usuario
            if (!modelo.PasswordHash.Equals(clave, StringComparison.OrdinalIgnoreCase))
                return new ResponseResult("Clave de usuario incorrecta", false);

            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString().ToUpper().Replace("-", "")));

            var rol = _mapper.Map<dtoRol>(usuario.Rol);
            var persona = _mapper.Map<dtoPersona>(usuario.Persona);

            // Retorno el token generado
            return new ResponseResult()
            {
                Ok = true,
                Datos = new dtoUsuarioApp()
                {
                    Id = usuario.Id,
                    Codigo = usuario.Codigo,
                    Acceso = modelo.Acceso,
                    Rol = rol,
                    Persona = persona,
                    Token = token
                }
            };
        }
    }
}
