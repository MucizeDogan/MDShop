using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Order.Application.Services {
    public static class ServiceRegistiration { // static olsun ki diğer taraftan direkt bunun içindeki metotlara erişim sağlayabilelim.
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration) {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
        }
    }
}
