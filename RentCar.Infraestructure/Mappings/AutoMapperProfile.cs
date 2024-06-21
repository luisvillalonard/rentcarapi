using AutoMapper;
using Diversos.Core.Dto;
using Diversos.Core.Entidades;
using Diversos.Core.Models;
using System;
using System.Linq;

namespace Diversos.Infraestructure.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public readonly string DateFormat_DD_MM_YYYY = "dd/MM/yyyy";
        public readonly string DateFormat_YYYY_MM_DD = "yyyy-MM-dd";

        public AutoMapperProfile()
        {
            CreateMap<Accesorio, dtoAccesorio>();
            CreateMap<dtoAccesorio, Accesorio>();

            CreateMap<Carga, dtoCarga>();
            CreateMap<dtoCarga, Carga>();

            CreateMap<Color, dtoColor>();
            CreateMap<dtoColor, Color>();

            CreateMap<Combustible, dtoCombustible>();
            CreateMap<dtoCombustible, Combustible>();

            CreateMap<Estado, dtoEstado>();
            CreateMap<dtoEstado, Estado>();

            CreateMap<Foto, dtoFoto>()
                .ForMember(dest => dest.Imagen, orig => orig.MapFrom(b => Util.StringToBase64(b.Imagen, b.Extension)));
            CreateMap<dtoFoto, Foto>()
                .ForMember(dest => dest.Imagen, orig => orig.MapFrom(b => Util.Base64ToBytes(b.Imagen)))
                .ForMember(dest => dest.Extension, orig => orig.MapFrom(b => Util.ExtensionFromBase64(b.Imagen)));
            
            CreateMap<Marca, dtoMarca>();
            CreateMap<dtoMarca, Marca>();

            CreateMap<Modelo, dtoModelo>()
                .ForMember(dest => dest.Marca, orig => orig.MapFrom(b => b.Marca));
            CreateMap<dtoModelo, Modelo>()
                .ForMember(dest => dest.Marca, orig => orig.Ignore());
            
            CreateMap<Motor, dtoMotor>();
            CreateMap<dtoMotor, Motor>();
            
            CreateMap<Provincia, dtoProvincia>();
            CreateMap<dtoProvincia, Provincia>();
            
            CreateMap<Municipio, dtoMunicipio>()
                .ForMember(dest => dest.Provincia, orig => orig.MapFrom(b => b.Provincia));
            CreateMap<dtoMunicipio, Municipio>()
                .ForMember(dest => dest.Provincia, orig => orig.Ignore());
            
            CreateMap<Rol, dtoRol>();
            CreateMap<dtoRol, Rol>();
            
            CreateMap<Traccion, dtoTraccion>();
            CreateMap<dtoTraccion, Traccion>();
            
            CreateMap<Transmision, dtoTransmision>();
            CreateMap<dtoTransmision, Transmision>();

            CreateMap<VehiculoTipo, dtoVehiculoTipo>();
            CreateMap<dtoVehiculoTipo, VehiculoTipo>();

            CreateMap<ConfiguracionAlquilerNota, dtoConfiguracionAlquilerNota>();
            CreateMap<dtoConfiguracionAlquilerNota, ConfiguracionAlquilerNota>();




            CreateMap<Usuario, dtoUsuario>()
                .ForMember(dest => dest.Rol, orig => orig.MapFrom(b => b.Rol))
                .ForMember(dest => dest.PasswordHash, orig => orig.Ignore())
                .ForMember(dest => dest.PasswordSalt, orig => orig.Ignore());
            CreateMap<dtoUsuario, Usuario>()
                .ForMember(dest => dest.Rol, orig => orig.Ignore());
            
            CreateMap<Usuario, dtoLogin>()
                .ForMember(dest => dest.Usuario, orig => orig.MapFrom(b => b.Acceso))
                .ForMember(dest => dest.Clave, orig => orig.MapFrom(b => b.PasswordHash));
            CreateMap<dtoLogin, Usuario>()
                .ForMember(dest => dest.Acceso, orig => orig.MapFrom(b => b.Usuario))
                .ForMember(dest => dest.PasswordHash, orig => orig.MapFrom(b => b.Clave));
            
            CreateMap<Persona, dtoPersona>()
                .ForMember(dest => dest.Municipio, orig => orig.MapFrom(b => b.Municipio))
                .ForMember(dest => dest.Usuario, orig => orig.MapFrom(b => b.Usuario == null ? null : b.Usuario.FirstOrDefault()))
                .ForMember(dest => dest.Foto, orig => orig.MapFrom(b => b.Foto));
            CreateMap<dtoPersona, Persona>()
                .ForMember(dest => dest.Municipio, orig => orig.Ignore())
                .ForMember(dest => dest.Vehiculo, orig => orig.Ignore())
                .ForMember(dest => dest.Usuario, orig => orig.Ignore())
                .ForMember(dest => dest.Foto, orig => orig.MapFrom(b => b.Foto));
            
            CreateMap<VehiculoFoto, dtoVehiculoFoto>()
                .ForMember(dest => dest.Foto, orig => orig.MapFrom(b => b.Foto));
            CreateMap<dtoVehiculoFoto, VehiculoFoto>()
                .ForMember(dest => dest.Foto, orig => orig.MapFrom(dest => dest.Foto))
                .ForMember(dest => dest.Vehiculo, orig => orig.Ignore());
            
            CreateMap<VehiculoAccesorio, dtoVehiculoAccesorio>()
                .ForMember(dest => dest.Accesorio, orig => orig.MapFrom(b => b.Accesorio));
            CreateMap<dtoVehiculoAccesorio, VehiculoAccesorio>()
                .ForMember(dest => dest.Accesorio, orig => orig.Ignore())
                .ForMember(dest => dest.Vehiculo, orig => orig.Ignore());

            CreateMap<Vehiculo, dtoVehiculo>()
                .ForMember(dest => dest.Persona, orig => orig.MapFrom(b => b.Persona == null ? null : b.Persona))
                .ForMember(dest => dest.Tipo, orig => orig.MapFrom(b => b.Tipo == null ? null : b.Tipo))
                .ForMember(dest => dest.Modelo, orig => orig.MapFrom(b => b.Modelo == null ? null : b.Modelo))
                .ForMember(dest => dest.Combustible, orig => orig.MapFrom(b => b.Combustible == null ? null : b.Combustible))
                .ForMember(dest => dest.Transmision, orig => orig.MapFrom(b => b.Transmision == null ? null : b.Transmision))
                .ForMember(dest => dest.Traccion, orig => orig.MapFrom(b => b.Traccion == null ? null : b.Traccion))
                .ForMember(dest => dest.Interior, orig => orig.MapFrom(b => b.Interior == null ? null : b.Interior))
                .ForMember(dest => dest.Exterior, orig => orig.MapFrom(b => b.Exterior == null ? null : b.Exterior))
                .ForMember(dest => dest.Motor, orig => orig.MapFrom(b => b.Motor == null ? null : b.Motor))
                .ForMember(dest => dest.Carga, orig => orig.MapFrom(b => b.Carga == null ? null : b.Carga))
                .ForMember(dest => dest.Foto, orig => orig.MapFrom(b => b.Foto))
                .ForMember(dest => dest.Accesorios, orig => orig.MapFrom(b => b.VehiculoAccesorio.Select(v => v.Accesorio)))
                .ForMember(dest => dest.Fotos, orig => orig.MapFrom(b => b.VehiculoFoto.Select(v => v.Foto)));
            CreateMap<dtoVehiculo, Vehiculo>()
                .ForMember(dest => dest.Persona, orig => orig.Ignore())
                .ForMember(dest => dest.Tipo, orig => orig.Ignore())
                .ForMember(dest => dest.Modelo, orig => orig.Ignore())
                .ForMember(dest => dest.Combustible, orig => orig.Ignore())
                .ForMember(dest => dest.Transmision, orig => orig.Ignore())
                .ForMember(dest => dest.Traccion, orig => orig.Ignore())
                .ForMember(dest => dest.Interior, orig => orig.Ignore())
                .ForMember(dest => dest.Exterior, orig => orig.Ignore())
                .ForMember(dest => dest.Motor, orig => orig.Ignore())
                .ForMember(dest => dest.Carga, orig => orig.Ignore())
                .ForMember(dest => dest.Foto, orig => orig.Ignore())
                .ForMember(dest => dest.VehiculoFoto, orig => orig.Ignore())
                .ForMember(dest => dest.VehiculoAccesorio, orig => orig.Ignore());
            
            CreateMap<Alquiler, dtoAlquiler>()
                .ForMember(dest => dest.FechaInicio, orig => orig.MapFrom(b => b.FechaInicio.ToString(DateFormat_YYYY_MM_DD)))
                .ForMember(dest => dest.FechaFin, orig => orig.MapFrom(b => b.FechaFin.ToString(DateFormat_YYYY_MM_DD)))
                .ForMember(dest => dest.Vehiculo, orig => orig.MapFrom(b => b.Vehiculo))
                .ForMember(dest => dest.Persona, orig => orig.MapFrom(b => b.Persona))
                .ForMember(dest => dest.Comprobante, orig => orig.MapFrom(b => !b.ComprobanteId.HasValue ? null : b.Comprobante))
                .ForMember(dest => dest.Notas, orig => orig.MapFrom(b => b.AlquilerNota));
            CreateMap<dtoAlquiler, Alquiler>()
                .ForMember(dest => dest.FechaInicio, orig => orig.MapFrom(b => DateTime.Parse(b.FechaInicio)))
                .ForMember(dest => dest.FechaFin, orig => orig.MapFrom(b => DateTime.Parse(b.FechaFin)))
                .ForMember(dest => dest.AlquilerNota, orig => orig.MapFrom(v => v.Notas))
                .ForMember(dest => dest.Persona, orig => orig.Ignore())
                .ForMember(dest => dest.Vehiculo, orig => orig.Ignore())
                .ForMember(dest => dest.Comprobante, orig => orig.Ignore());
            
            CreateMap<AlquilerNota, dtoAlquilerNota>();
            CreateMap<dtoAlquilerNota, AlquilerNota>();
            
            CreateMap<Mensaje, dtoMensaje>()
                .ForMember(dest => dest.Fecha, orig => orig.MapFrom(b => b.Fecha.ToString(DateFormat_YYYY_MM_DD)));
            CreateMap<dtoMensaje, Mensaje>()
                .ForMember(dest => dest.Fecha, orig => orig.MapFrom(b => DateTime.Parse(b.Fecha)));
        }
    }
}
