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
    public class TraccionesController : ControllerBase
    {
        private readonly ITraccionRepositorio _repositorio;
        private readonly IMapper _mapper;
        public TraccionesController(IMapper mapper, ITraccionRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositorio.GetAllAsync(item => item.OrderBy(ord => ord.Nombre));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoTraccion>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoTraccion modelo)
        {
            var item = _mapper.Map<Traccion>(modelo);
            var result = await _repositorio.PostAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoTraccion>(result.Datos);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoTraccion modelo)
        {
            var item = _mapper.Map<Traccion>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoTraccion>(result.Datos);

            return Ok(result);
        }
    }
}
