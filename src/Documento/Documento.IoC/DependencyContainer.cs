using Documento.Infraestructura;
using Microsoft.Extensions.DependencyInjection;

namespace Documento.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDocumentoService(this IServiceCollection services, string stringConexion)
        {
            services.AddDocumentoInfraestructura(stringConexion);
            services.AddDocumentoAplicacion();
            return services;
        }
    }
}
