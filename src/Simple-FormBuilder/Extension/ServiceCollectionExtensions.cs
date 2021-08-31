using Simple_Formbuilder.Context;
using Simple_Formbuilder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple_FormBuilder;

namespace Simple_Formbuilder.Extension
{
    public static class ServiceCollectionExtensions
    {

        public static void AddClwFormbuilder(this IServiceCollection services, string mongoConnectionString, string mongoDatabaseName)
        {
            services.AddSingleton(new MongoConfiguration
            {
                MongoConnectionString = mongoConnectionString,
                MongoDatabase = mongoDatabaseName
            });
            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<IFormManagementService, FormManagementService>();
            services.AddScoped<IRecordsService, RecordsService>();
        }
    }
}
