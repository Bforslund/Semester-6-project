using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlayerService.MessageHandlers;
using Shared;

namespace HotelService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelService", Version = "v1" });
            });
            services.AddMessagePublishing("HotelService", builder => {
                builder.WithHandler<NewBookingMessageHandler>("BookingCompleted");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelService v1"));
            }

            app.UseRouting();
           // MessageQueue();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public void MessageQueue()
        {
           /* ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.Port = 5672;
            factory.UserName = "guest";
            factory.Password = "guest";

            using IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();
            channel.ExchangeDeclare("bookingqueue", ExchangeType.Fanout, true, false, null);
            channel.QueueDeclare("queueNewBooking");
            channel.QueueBind("queueNewBooking", "bookingqueue", "#");
           
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.Unicode.GetString(body);
                Hotel newHotel = JsonSerializer.Deserialize<Hotel>(message);
                controller.hotelList.Add(newHotel);
            };
            channel.BasicConsume(queue: "queueNewBooking",
                                 autoAck: true,
                                 consumer: consumer);*/

            
        
    }
    }
}
