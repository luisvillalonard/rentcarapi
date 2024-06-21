using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehiculoRepositorio _repositorio;
        private readonly IVehiculoTipoRepositorio _repoVehiculoTipo;
        private readonly IVehiculoFotoRepositorio _repoFoto;
        private readonly IVehiculoAccesorioRepositorio _repoAccesorio;
        public VehiculosController(
            IMapper mapper, 
            IVehiculoRepositorio repositorio, 
            IVehiculoTipoRepositorio repoVehiculoTipo,
            IVehiculoFotoRepositorio repoFoto,
            IVehiculoAccesorioRepositorio repoAccesorio)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _repoVehiculoTipo = repoVehiculoTipo ?? throw new ArgumentNullException(nameof(repoVehiculoTipo));
            _repoFoto = repoFoto ?? throw new ArgumentNullException(nameof(repoFoto));
            _repoAccesorio = repoAccesorio ?? throw new ArgumentNullException(nameof(repoAccesorio));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] RequestFilter query)
        {
            Expression<Func<Vehiculo, bool>> filters = null;
            if (query == null && string.IsNullOrEmpty(query.Filter))
            {
                filters = veh => veh.Modelo.Marca.Nombre.Contains(query.Filter) ||
                                 veh.Modelo.Nombre.Contains(query.Filter);
            }

            var result = await _repositorio.FindAsync(
                query, 
                filters,
                item => item.OrderBy(ord => ord.Tipo.Nombre),
                item => item.Persona,
                item => item.Tipo,
                item => item.Modelo,
                item => item.Modelo.Marca,
                item => item.Combustible,
                item => item.Transmision,
                item => item.Traccion,
                item => item.Interior,
                item => item.Exterior,
                item => item.Motor,
                item => item.Carga,
                item => item.VehiculoAccesorio,
                item => item.VehiculoFoto,
                item => item.Foto
            );
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoVehiculo>>(result.Datos);
            return Ok(result);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetPorCodigoAll(string codigo)
        {
            var result = await _repositorio.FindAsync(
                veh => veh.Codigo.Equals(codigo),
                item => item.Persona,
                item => item.Tipo,
                item => item.Modelo,
                item => item.Modelo.Marca,
                item => item.Combustible,
                item => item.Transmision,
                item => item.Traccion,
                item => item.Interior,
                item => item.Exterior,
                item => item.Motor,
                item => item.Carga,
                item => item.Foto
            );
            if (!result.Ok)
                return Ok(result);

            var vehiculo = (result.Datos as IEnumerable<Vehiculo>).FirstOrDefault();

            result = await _repoAccesorio.FindAsync(
                acc => acc.VehiculoId == vehiculo.Id,
                acc => acc.Accesorio
            );
            if (result.Ok)
            {
                var accesorios = (result.Datos as IEnumerable<VehiculoAccesorio>)
                    .OrderBy(rel => rel.Accesorio.Nombre);
                vehiculo.VehiculoAccesorio = vehiculo.VehiculoAccesorio
                    .Select(rel => { rel.VehiculoId = vehiculo.Id; return rel; })
                    .ToArray();
            }

            result = await _repoFoto.FindAsync(
                foto => foto.VehiculoId == vehiculo.Id,
                foto => foto.Foto
            );
            if (result.Ok)
            {
                var fotos = (result.Datos as IEnumerable<VehiculoFoto>);
                vehiculo.VehiculoFoto = vehiculo.VehiculoFoto
                    .Select(rel => { rel.VehiculoId = vehiculo.Id; return rel; })
                    .ToArray();
            }

            result.Datos = _mapper.Map<dtoVehiculo>(vehiculo);
            return Ok(result);
        }

        [HttpGet("propietario/{codigo}")]
        public async Task<IActionResult> GetPorPropietarioAll(string codigo)
        {
            var result = await _repositorio.FindAsync(
                veh => veh.Persona.Codigo.Equals(codigo),
                item => item.Tipo,
                item => item.Modelo,
                item => item.Modelo.Marca,
                item => item.Combustible,
                item => item.Transmision,
                item => item.Traccion,
                item => item.Interior,
                item => item.Exterior,
                item => item.Motor,
                item => item.Carga,
                item => item.Foto
            );
            if (!result.Ok)
                return Ok(result);

            var vehiculos = result.Datos as IEnumerable<Vehiculo>;

            result = await _repoAccesorio.FindAsync(
                acc => vehiculos.Select(v => v.Id).Any(veh => veh == acc.VehiculoId),
                acc => acc.Accesorio
            );
            if (result.Ok)
            {
                var accesorios = (result.Datos as IEnumerable<VehiculoAccesorio>)
                    .OrderBy(rel => rel.Accesorio.Nombre);
                vehiculos = vehiculos
                    .Select(veh => { veh.VehiculoAccesorio = accesorios.Where(acc => acc.VehiculoId == veh.Id).ToArray(); return veh; })
                    .ToArray();
            }
            
            //result = await _repoFoto.FindAsync(
            //    foto => vehiculos.Select(v => v.Id).Any(veh => veh == foto.VehiculoId),
            //    foto => foto.Foto
            //);
            //if (result.Ok)
            //{
            //    var fotos = (result.Datos as IEnumerable<VehiculoFoto>);
            //    vehiculos = vehiculos
            //        .Select(veh => { veh.VehiculoFoto = fotos.Where(foto => foto.VehiculoId == veh.Id).ToArray(); return veh; })
            //        .ToArray();
            //}

            var dtoVehiculos = _mapper.Map<IEnumerable<dtoVehiculo>>(vehiculos);
            result.Datos = dtoVehiculos
                .OrderBy(veh => veh.Modelo.Marca.Nombre)
                .ThenBy(veh => veh.Modelo.Nombre);

            return Ok(result);
        }

        [HttpPost("disponibles")]
        public async Task<IActionResult> GetDisponiblesAll([FromBody] dtoAlquilerFiltro filtro)
        {
            var parametros = new Dictionary<string, object>();
            if (filtro != null)
            {
                parametros.Add("@MarcaId", filtro.MarcaId == 0 ? null : filtro.MarcaId);
                parametros.Add("@ModeloId", filtro.ModeloId == 0 ? null : filtro.ModeloId);
                parametros.Add("@CombustibleId", filtro.ModeloId == 0 ? null : filtro.CombustibleId);
                parametros.Add("@FechaInicio", (!string.IsNullOrEmpty(filtro.FechaInicio) && DateTime.TryParse(filtro.FechaInicio, out DateTime resFechaInicio) == true) ? $"'{DateTime.Parse(filtro.FechaInicio).ToString("yyyy-MM-dd")}'" : null);
                parametros.Add("@FechaFin", (!string.IsNullOrEmpty(filtro.FechaFin) && DateTime.TryParse(filtro.FechaFin, out DateTime resFEchaFin) == true) ? $"'{DateTime.Parse(filtro.FechaFin).ToString("yyyy-MM-dd")}'" : null);
            }

            var result = await _repositorio.ExecuteProcedureAsync(Procedimientos.SP_Vehiculos_Disponibles, parametros);
            if (!result.Ok)
                return Ok(result);

            var pre = _mapper.Map<IEnumerable<dtoVehiculo>>(result.Datos).Select(veh => veh.Id).ToList();
            result = await _repositorio.FindAsync(
                veh => pre.Any(x => x == veh.Id),
                item => item.Persona,
                item => item.Tipo,
                item => item.Modelo,
                item => item.Modelo.Marca,
                item => item.Combustible,
                item => item.Transmision,
                item => item.Traccion,
                item => item.Interior,
                item => item.Exterior,
                item => item.Motor,
                item => item.Carga,
                item => item.Foto
            );
            if (!result.Ok)
                return Ok(result);

            var vehiculos = result.Datos as IEnumerable<Vehiculo>;

            result = await _repoAccesorio.FindAsync(
                acc => vehiculos.Select(v => v.Id).Any(veh => veh == acc.VehiculoId),
                acc => acc.Accesorio
            );
            if (result.Ok)
            {
                var accesorios = (result.Datos as IEnumerable<VehiculoAccesorio>)
                    .OrderBy(rel => rel.Accesorio.Nombre);
                vehiculos = vehiculos
                    .Select(veh => { veh.VehiculoAccesorio = accesorios.Where(acc => acc.VehiculoId == veh.Id).ToArray(); return veh; })
                    .ToArray();
            }

            result = await _repoFoto.FindAsync(
                foto => vehiculos.Select(v => v.Id).Any(veh => veh == foto.VehiculoId),
                foto => foto.Foto
            );
            if (result.Ok)
            {
                var fotos = (result.Datos as IEnumerable<VehiculoFoto>);
                vehiculos = vehiculos
                    .Select(veh => { veh.VehiculoFoto = fotos.Where(foto => foto.VehiculoId == veh.Id).ToArray(); return veh; })
                    .ToArray();
            }

            var dtoVehiculos = _mapper.Map<IEnumerable<dtoVehiculo>>(vehiculos);
            result.Datos = dtoVehiculos
                .OrderBy(veh => veh.Modelo.Marca.Nombre)
                .ThenBy(veh => veh.Modelo.Nombre);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoVehiculo modelo)
        {
            var item = _mapper.Map<Vehiculo>(modelo);

            item.VehiculoFoto = _mapper.Map<Foto[]>(modelo.Fotos)
                .Select(foto => new VehiculoFoto() { Foto = foto }).ToArray();

            item.VehiculoAccesorio = _mapper.Map<Accesorio[]>(modelo.Accesorios)
                .Select(acc => new VehiculoAccesorio() { Accesorio = acc }).ToArray();

            var result = await _repositorio.PostAsync(item);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] dtoVehiculo modelo)
        {
            var item = _mapper.Map<Vehiculo>(modelo);
            var result = await _repositorio.PutAsync(item);
            if (result.Ok)
                result.Datos = _mapper.Map<dtoVehiculo>(result.Datos);

            return Ok(result);
        }

        [HttpGet("tipos")]
        public async Task<IActionResult> GetTiposAll()
        {
            var result = await _repoVehiculoTipo.GetAllAsync(item => item.OrderBy(ord => ord.Nombre));
            if (!result.Ok)
                return Ok(result);

            result.Datos = _mapper.Map<IEnumerable<dtoVehiculoTipo>>(result.Datos);
            return Ok(result);
        }
    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
