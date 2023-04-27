
using FarzadsTask;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}





//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using FarzadsTask.Data;
//using FarzadsTask.Repositories;
//using FluentValidation.AspNetCore;
//using FarzadsTask.Validators;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<MyApiDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

////builder.Services.AddAutoMapper(typeof(Program).Assembly);

//builder.Services.AddControllers().AddFluentValidation(options =>
//{
//    options.RegisterValidatorsFromAssemblyContaining<CustomerValidator>();
//});
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
