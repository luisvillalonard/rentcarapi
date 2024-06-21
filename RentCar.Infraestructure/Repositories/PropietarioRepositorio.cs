using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Interfaces;
using Diversos.Core.Models;
using Diversos.Core.Models.Email;
using Diversos.Infraestructure.Contexto;
using Diversos.Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversos.Infraestructure.Repositories
{
    public class PropietarioRepositorio : RepositorioGenerico<Persona, int>, IPropietarioRepositorio
    {
        private readonly IMailService _mailService;
        public PropietarioRepositorio(DiversosContext context, IMailService mailService) : base(context)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public override async Task<ResponseResult> PostAsync(Persona item)
        {
            ResponseResult result = new();

            using (var transaccion = await base.context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Guardo la foto
                    if (item.Foto != null)
                    {
                        base.context.Entry(item.Foto).State = EntityState.Added;
                        if (await context.SaveChangesAsync() > 0)
                            item.FotoId = item.Foto.Id;
                    }

                    // Guardo los datos del propietario
                    base.context.Entry(item).State = EntityState.Added;
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible guardar los datos del propietario.", false);
                    }

                    // Obtengo el rol del propietario
                    var rol = await base.context.Rol.FirstOrDefaultAsync(rol => rol.Propietario == true);
                    if (rol == null)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible establecer el rol del propietario.", false);
                    }

                    // Creo el usuario del propietario
                    var salt = Util.NewSalt();
                    var clave = Encriptador.AES.Encrypt(Util.RandomString(10), salt);
                    var usuario = new Usuario()
                    {
                        Codigo = Guid.NewGuid().ToString().ToLower().Replace("-", ""),
                        Acceso = item.Documento.Replace("-", ""),
                        Rol = rol,
                        PasswordHash = clave,
                        PasswordSalt = salt,
                        CreadoEn = DateTime.Now,
                        Persona = new() { Id = item.Id }
                    };

                    // Guardo el usuario del propietario
                    base.context.Entry(usuario).State = EntityState.Added;
                    if (await context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible crear el usuario del propietario.", false);
                    }

                    // Doy por valida la transaccion en base de datos
                    await transaccion.CommitAsync();
                }
                catch (Exception err)
                {
                    await transaccion.RollbackAsync();
                    return new ResponseResult($"Situación inesperada tratando de guardar los datos. {err.Message} {(err.InnerException ?? new Exception()).Message}".Trim(), false);
                }

                try
                {
                    // Envio el correo
                    var mailRequest = new MailRequest()
                    {
                        ToEmail = item.Correo,
                        Subject = "Creación de Usuario en Sistema Alquiler de Vehículos.",
                        Body = $"Ha realizado su registro en el sistema La Manguera Car de forma exitosa. para completar su ingreso al sistema deberá ir al sistema y establecer su nueva contraseña haciendo click en el siguiente enlace: <a href='http://localhost:3000/usuarios/confirmacion/{item.Codigo.ToLower()}'>Completar acceso al sistema</a>"
                    };

                    await _mailService.SendEmailAsync(mailRequest);

                }
                catch (Exception) { }

                return result;
            }
        }

        public override async Task<ResponseResult> PutAsync(Persona item)
        {
            ResponseResult result = new();

            await using (var transaccion = await base.context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obtengo el propietario
                    var propietario = await base.context.Persona
                        .AsNoTracking()
                        .Include(prop => prop.Foto)
                        .FirstOrDefaultAsync(prop => prop.Id == item.Id);
                    if (propietario == null)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("Código de propietario inválido.", false);
                    }

                    // Si cargo una foto la guardo
                    var existeFoto = propietario.FotoId.HasValue;
                    if (existeFoto)
                    {
                        if (item.Foto != null) 
                        {
                            propietario.Foto.Imagen = item.Foto.Imagen;
                            propietario.Foto.Extension = item.Foto.Extension;
                            base.context.Entry(propietario.Foto).State = EntityState.Modified;
                            await base.context.SaveChangesAsync();
                        }
                    }
                    else if (!existeFoto && item.Foto != null)
                    {
                        base.context.Entry(item.Foto).State = EntityState.Added;
                        if (await base.context.SaveChangesAsync() > 0)
                            item.FotoId = item.Foto.Id;
                    }

                    // Guardo los datos del propietario
                    base.context.Entry(item).State = EntityState.Modified;
                    if (await base.context.SaveChangesAsync() == 0)
                    {
                        await transaccion.RollbackAsync();
                        return new ResponseResult("No fue posible guardar los datos del propietario.", false);
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
