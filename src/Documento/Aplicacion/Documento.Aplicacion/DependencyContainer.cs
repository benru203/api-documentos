using Documento.Aplicacion.Interfaces;
using Documento.Aplicacion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Documento.Infraestructura
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDocumentoAplicacion(this IServiceCollection services)
        {
            services.AddScoped<IDocumentoService, DocumentoService>();
            return services;
        }
    }
}
