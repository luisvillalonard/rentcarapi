using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Diversos.Infraestructure.Contexto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Infraestructure.Repositories
{
    public class VehiculoRepositorio : RepositorioGenerico<Vehiculo, int>, IVehiculoRepositorio
    {
        private readonly IVehiculoAccesorioRepositorio _accesoriosRepositorio;
        private readonly IVehiculoFotoRepositorio _vehiculoFotoRepositorio;
        private readonly IFotoRepositorio _fotoRepositorio;

        public VehiculoRepositorio(
            DiversosContext context,
            IVehiculoAccesorioRepositorio accesoriosRepositorio,
            IVehiculoFotoRepositorio vehiculoFotoRepositorio,
            IFotoRepositorio fotoRepositorio
        ) : base(context)
        {
            _accesoriosRepositorio = accesoriosRepositorio;
            _vehiculoFotoRepositorio = vehiculoFotoRepositorio;
            _fotoRepositorio = fotoRepositorio;
        }

        public override async Task<ResponseResult> PostAsync(Vehiculo item)
        {
            ResponseResult result = new();

            using (var transaccion = await base.context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Guardo las fotos
                    var fotos = item.VehiculoFoto.Select(acc => acc.Foto).ToArray();
                    await base.context.Foto.AddRangeAsync(fotos);
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible de guardar las fotos del vehículo", false);
                    }

                    // Establezco el codigo del vehiculo
                    item.Codigo = Guid.NewGuid().ToString().Replace("-", "").ToLower();

                    // Establezco la primera de las fotos al vehiculo como la principal
                    item.FotoId = fotos.First().Id;

                    // Guardo el vehiculo
                    base.context.Entry(item).State = EntityState.Added;
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible de guardar los datos del vehículo", false);
                    }

                    // Guardo los accesorios del vehiculo
                    var accesorios = item.VehiculoAccesorio.Select(acc => { acc.VehiculoId = item.Id; acc.AccesorioId = acc.Accesorio.Id; acc.Accesorio = null; return acc; }).ToArray();
                    await base.context.VehiculoAccesorio.AddRangeAsync(accesorios);
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible de guardar los accesorios del vehículo", false);
                    }

                    // Guardo las fotos del vehiculo
                    var vehFotos = item.VehiculoFoto.Select(foto => { foto.VehiculoId = item.Id; return foto; }).ToArray();
                    await base.context.VehiculoFoto.AddRangeAsync(vehFotos);
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible de guardar las fotos del vehículo", false);
                    }

                    // Doy por valida la transaccion en base de datos
                    await transaccion.CommitAsync();
                }
                catch (Exception err)
                {
                    await transaccion.RollbackAsync();
                    return new ResponseResult($"Situación inesperada tratando de guardar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
                }

                return result;
            }
        }
    }
}
