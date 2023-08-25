using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using WS.Business.Implementations;

namespace WS.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAPIServices(this IServiceCollection services)
        {

            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
    }
}
