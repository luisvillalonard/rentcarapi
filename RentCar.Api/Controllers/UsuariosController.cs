using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Diversos.Core.Models.Email;
using Diversos.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMapper _mapper;
        public UsuariosController(IMapper mapper, IUsuarioRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositorio.GetAllAsync(
                ord => ord.OrderBy(ord => ord.Acceso).ThenBy(ord => ord.Rol.Nombre), 
                usu => usu.Rol);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoUsuario>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoUsuario modelo)
        {
            modelo.Codigo = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            var item = _mapper.Map<Usuario>(modelo);
            //item.CreadoEn = DateTime.Now;
            item.PasswordSalt = Util.NewSalt();
            item.PasswordHash = Encriptador.AES.Encrypt(Util.RandomString(10), item.PasswordSalt);
            var result = await _repositorio.PostAsync(item);
            if (!result.Ok)
                return Ok(result);

            //try
            //{
            //    // Envio el correo
            //    var mailRequest = new MailRequest()
            //    {
            //        ToEmail = item.Correo,
            //        Subject = "Creación de Usuario en Sistema Alquiler de Vehículos.",
            //        Body = $"Ha realizado su registro en el sistema La Manguera Car de forma exitosa. para completar su ingreso al sistema deberá ir al sistema y establecer su nueva contraseña haciendo click en el siguiente enlace: <a href='http://localhost:3000/usuarios/confirmacion/{item.Codigo.ToLower()}'>Completar acceso al sistema</a>"
            //    };

            //    await _mailService.SendEmailAsync(mailRequest);

            //}
            //catch (Exception) { }

            result.Datos = _mapper.Map<dtoUsuario>(result.Datos);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoUsuario modelo)
        {
            var result = await _repositorio.FindAsync(x => x.Id == modelo.Id);
            if (!result.Ok)
                return Ok(result);

            var usuario = _mapper.Map<IEnumerable<Usuario>>(result.Datos).FirstOrDefault();
            if (usuario == null)
                return Ok(new ResponseResult("Código de usuario no encontrado", false));

            usuario.RolId = modelo.Rol.Id;
            usuario.Activo = modelo.Activo;

            result = await _repositorio.PutAsync(usuario);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<dtoUsuario>(result.Datos);
            return Ok(result);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetPorCodigoAsync(string codigo)
        {
            var result = await _repositorio.FindAsync(
                prop => prop.Codigo.Equals(codigo)
            );
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoUsuario>>(result.Datos).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost("cambiarClave")]
        public async Task<IActionResult> PostCambiarClave([FromBody] dtoUsuarioCambioClave modelo)
        {
            var result = await _repositorio.GetByIdAsync(modelo.Id);
            if (!result.Ok)
                return Ok(result);

            var usuario = result.Datos as Usuario;
            if (usuario == null)
                return Ok(new ResponseResult("Código de usuario no encontrado"));

            usuario.PasswordHash = Encriptador.AES.Encrypt(modelo.PasswordNew, usuario.PasswordSalt);
            usuario.Cambio = true;
            usuario.Activo = true;

            result = await _repositorio.PutAsync(usuario);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<dtoUsuario>(result.Datos);
            return Ok(result);
        }

        [HttpPost("obtener")]
        public async Task<IActionResult> PostObtener([FromBody] dtoLogin modelo)
        {
            var result = await _repositorio.FindAsync(x => x.Acceso.Equals(modelo.Usuario));
            if (!result.Ok)
                return Ok(result);

            var item = _mapper.Map<IEnumerable<Usuario>>(result.Datos).FirstOrDefault();
            if (item == null)
                return Ok(new ResponseResult("Código de usuario no encontrado", false));

            result.Datos = _mapper.Map<dtoLogin>(item);
            return Ok(result);
        }

        [HttpPost("validar")]
        public async Task<IActionResult> PostValidar([FromBody] dtoLogin modelo)
        {
            var item = _mapper.Map<Usuario>(modelo);
            var result = await _repositorio.PostValidarAsync(item);
            if (!result.Ok)
                return Ok(result);

            return Ok(result);
        }
    }
}
