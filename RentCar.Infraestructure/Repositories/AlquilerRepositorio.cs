using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Diversos.Infraestructure.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Infraestructure.Repositories
{
    public class AlquilerRepositorio : RepositorioGenerico<Alquiler, long>, IAlquilerRepositorio
    {
        private readonly IPersonaRepositorio _personaRepositorio;
        private readonly IFotoRepositorio _fotoRepositorio;

        public AlquilerRepositorio(
            DiversosContext context,
            IPersonaRepositorio personaRepositorio,
            IFotoRepositorio fotoRepositorio
        ) : base(context) 
        {
            _personaRepositorio = personaRepositorio;
            _fotoRepositorio = fotoRepositorio;
        }

        public override async Task<ResponseResult> PostAsync(Alquiler item)
        {
            ResponseResult result = new();

            using (var transaccion = await base.context.Database.BeginTransactionAsync())
            {
                try
                {
                    result = await _personaRepositorio.FindAsync(per => per.Documento.Equals(item.Persona.Documento));
                    if (!result.Ok)
                    {
                        await transaccion.RollbackAsync();
                        return result;
                    }

                    // Obtengo la persona desde la base de datos
                    var persona = (result.Datos as IEnumerable<Persona>).FirstOrDefault();

                    // Si no existe la persona la registro
                    if (persona == null)
                    {
                        result = await _personaRepositorio.PostAsync(item.Persona);
                        if (!result.Ok)
                        {
                            await transaccion.RollbackAsync();
                            return result;
                        }
                        persona = result.Datos as Persona;
                    }

                    // Asigno la persona al alquiler
                    item.PersonaId = item.Persona.Id;

                    // Guardo el comprobante
                    result = await _fotoRepositorio.PostAsync(item.Comprobante);
                    if (!result.Ok)
                    {
                        await transaccion.RollbackAsync();
                        return result;
                    }
                    
                    // Asigno el comprobante al alquiler
                    item.ComprobanteId = item.Comprobante.Id;

                    // Guardo el alquiler
                    result = await base.PostAsync(item);
                    if (!result.Ok)
                    {
                        await transaccion.RollbackAsync();
                        return result;
                    }

                    // Doy por valida la transaccion en base de datos
                    await transaccion.CommitAsync();
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();
                }

                return result;
            }
        }
    }
}
