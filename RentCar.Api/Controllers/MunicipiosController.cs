﻿using AutoMapper;
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
    public class MunicipiosController : ControllerBase
    {
        private readonly IMunicipioRepositorio _repositorio;
        private readonly IMapper _mapper;
        public MunicipiosController(IMapper mapper, IMunicipioRepositorio repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repositorio.GetAllAsync(item => item.OrderBy(ord => ord.Nombre), item => item.Provincia);
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoMunicipio>>(result.Datos);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoMunicipio modelo)
        {
            var item = _mapper.Map<Municipio>(modelo);
            var result = await _repositorio.PostAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoMunicipio>(result.Datos);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoMunicipio modelo)
        {
            var item = _mapper.Map<Municipio>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoMunicipio>(result.Datos);

            return Ok(result);
        }
    }
}
