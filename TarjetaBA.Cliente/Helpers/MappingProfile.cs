using AutoMapper;
using TarjetaBA.Cliente.Models;

namespace TarjetaBA.Cliente.Helpers
{
    public class MappingProfile:Profile
    {
        //Se raliza el mapeo de datos
        public MappingProfile()
        {
            CreateMap<TarjetaViewModel, TarjetaViewModel>();
            CreateMap<CompraViewModel, CompraViewModel>();
            CreateMap<PagoViewModel, PagoViewModel>();
            
        }
    }
}
