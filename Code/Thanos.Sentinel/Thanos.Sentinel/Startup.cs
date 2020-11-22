using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Thanos.Adapter.ServiceBus;
using Thanos.Sentinel.Workers;

namespace Thanos.Sentinel
{
    public class Startup
    {
        private HeartBeatAction hbAction;
        private TaskResponseAction trAction;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            hbAction = new HeartBeatAction();
            trAction = new TaskResponseAction();
            State.State.Instance.HueGroundBaseURL = configuration.GetSection("Hue").GetSection("Bridges").GetSection("HueGroundBaseURL").Value;
            State.State.Instance.HueFirstBaseURL = configuration.GetSection("Hue").GetSection("Bridges").GetSection("HueFirstBaseURL").Value;
            Receiver.RegisterOnMessageHandlerAndReceiveMessages();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
