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
    public class MensajesController : ControllerBase
    {
        private readonly IMensajeRepositorio _repositorio;
        private readonly IMapper _mapper;
        public MensajesController(IMapper mapper, IMensajeRepositorio repositorio)
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

            result.Datos = _mapper.Map<IEnumerable<dtoMensaje>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoMensaje modelo)
        {
            var item = _mapper.Map<Mensaje>(modelo);
            var result = await _repositorio.PostAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoMensaje>(result.Datos);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoMensaje modelo)
        {
            var item = _mapper.Map<Mensaje>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoMensaje>(result.Datos);

            return Ok(result);
        }
    }
}
