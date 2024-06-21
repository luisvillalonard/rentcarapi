using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlquileresController : ControllerBase
    {
        private readonly IAlquilerRepositorio _repositorio;
        private readonly IMapper _mapper;
        public AlquileresController(IMapper mapper, IAlquilerRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{codigo}/propietario")]
        public async Task<IActionResult> GetPorPropietarioAll(string codigo)
        {
            var result = await _repositorio.FindAsync(
                alq => alq.Vehiculo.Persona.Codigo.Equals(codigo),
                alq => alq.Vehiculo,
                alq => alq.Vehiculo.Modelo,
                alq => alq.Vehiculo.Modelo.Marca,
                alq => alq.Vehiculo.Exterior,
                alq => alq.Vehiculo.Foto
            );
            if (!result.Ok)
                return Ok(result);

            var alquiler = _mapper.Map<IEnumerable<dtoAlquiler>>(result.Datos);
            result.Datos = alquiler.OrderBy(veh => veh.FechaInicio);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoAlquiler modelo)
        {
            var item = _mapper.Map<Alquiler>(modelo);
            item.Persona = _mapper.Map<Persona>(modelo.Persona);
            item.Comprobante = _mapper.Map<Foto>(modelo.Comprobante);

            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoAlquiler modelo)
        {
            var item = _mapper.Map<Alquiler>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoAlquiler>(result.Datos);

            return Ok(result);
        }
    }
}

