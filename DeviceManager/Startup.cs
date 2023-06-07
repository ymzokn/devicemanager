using DeviceManager.Data;
using DeviceManager.Hubs;
using DeviceManager.Services;
using Microsoft.AspNetCore.Http.Connections;

namespace DeviceManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IMeasurementService, MeasurementService>();
            services.AddDbContext<DbContextClass>();
            services.AddSingleton<CountHub>();
            services.AddSignalR();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var rabbitMQConnectionString = Configuration.GetConnectionString("RabbitMQ");
            var queueName = Configuration.GetValue<string>("RabbitMQ:QueueName");
            var exchangeName = Configuration.GetValue<string>("RabbitMQ:ExchangeName");

            services.AddHostedService(provider =>
            {
                var serviceProvider = provider.CreateScope().ServiceProvider;
                var measurementService = serviceProvider.GetRequiredService<IMeasurementService>();
                var countHub = serviceProvider.GetRequiredService<CountHub>();
                return new RabbitMQConsumer(rabbitMQConnectionString, queueName, serviceProvider, countHub);
            });
            services.AddScoped(provider =>
            {
                return new RabbitMQProducer(rabbitMQConnectionString, exchangeName, queueName);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CountHub>( Configuration.GetValue<string>("RabbitMQ:QueueName"), options =>
                {
                    options.Transports = HttpTransportType.LongPolling;
                });
            });
        }
    }
}
