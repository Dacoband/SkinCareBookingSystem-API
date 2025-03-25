using MassTransit;
using MicroBlogs.Microservices.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

////Logger
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

////RabbitMQ-Consumer
builder.Services.AddMassTransit(x =>
{
    //add a new consumer, named TicketConsumer
    x.AddConsumer<BlogConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        //cfg.UseHealthCheck(provider);
        //cfg.Host(new Uri("rabbitmq://localhost:xxxx"), h =>
        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        //define the Receive endpoint, since this is a consumer.
        cfg.ReceiveEndpoint("blogQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            //Finally, link the orderQueue to the OrderConsumer class.
            ep.ConfigureConsumer<BlogConsumer>(provider);
        });
    }));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
