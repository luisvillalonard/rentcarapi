using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracionAlquilerNotasController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguracionAlquilerNotaRepositorio _repositorio;
        public ConfiguracionAlquilerNotasController(IMapper mapper, IConfiguracionAlquilerNotaRepositorio repositorio)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositorio.GetAllAsync(item => item.OrderBy(ord => ord.Id));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoConfiguracionAlquilerNota>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoConfiguracionAlquilerNota modelo)
        {
            var item = _mapper.Map<ConfiguracionAlquilerNota>(modelo);
            var result = await _repositorio.PostAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoConfiguracionAlquilerNota>(result.Datos);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoConfiguracionAlquilerNota modelo)
        {
            var item = _mapper.Map<ConfiguracionAlquilerNota>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoConfiguracionAlquilerNota>(result.Datos);

            return Ok(result);
        }
    }
}
