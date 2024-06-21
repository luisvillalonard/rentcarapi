using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Diversos.Core.Models.Email;
using Diversos.Infraestructure.Contexto;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPropietarioRepositorio _repositorio;
        public PropietariosController(
            IMapper mapper,
            IPropietarioRepositorio repositorio,
            IMailService mailService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositorio.GetAllAsync(
                prop => prop.OrderBy(ord => ord.Nombre),
                prop => prop.Municipio,
                prop => prop.Municipio.Provincia
            );
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoPersona>>(result.Datos);
            return Ok(result);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetPorCodigoAll(string codigo)
        {
            var result = await _repositorio.FindAsync(
                prop => prop.Codigo.Equals(codigo),
                prop => prop.Municipio,
                prop => prop.Municipio.Provincia,
                prop => prop.Usuario,
                prop => prop.Foto
            );
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoPersona>>(result.Datos).FirstOrDefault();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoPersona modelo)
        {
            modelo.Codigo = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            var item = _mapper.Map<Persona>(modelo);
            var result = await _repositorio.PostAsync(item);
            if (!result.Ok)
                return Ok(result);
                
            result.Datos = _mapper.Map<dtoPersona>(result.Datos);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoPersona modelo)
        {
            var item = _mapper.Map<Persona>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoPersona>(result.Datos);

            return Ok(result);
        }
    }
}
