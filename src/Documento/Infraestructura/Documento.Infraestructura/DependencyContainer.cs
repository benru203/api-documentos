using Documento.Dominio.Interfaces.Repositorios;
using Documento.Infraestructura.Context;
using Documento.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Documento.Infraestructura
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDocumentoInfraestructura(this IServiceCollection services, string stringConexion)
        {

            services.AddDbContext<DocumentoContext>(op =>
            {
                op.UseSqlServer(stringConexion);
            });
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            return services;
        }
    }
}
