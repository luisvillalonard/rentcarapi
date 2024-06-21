using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversos.Core.Dto
{
    public class dtoVehiculoAccesorio
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public dtoAccesorio Accesorio { get; set; }
    }
}
