using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations.MigrationConfig
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceDb = serviceScope.ServiceProvider
                                    .GetService<DevFreelaDbContext>();

                serviceDb.Database.Migrate();
            }
        }
    }
}
