

using Microsoft.EntityFrameworkCore;

namespace EmployeesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Startup ConfigureServices
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<DepartmentsLookup>();

            var sqlConnectionString = builder.Configuration.GetConnectionString("employees");
            Console.WriteLine("Using this connection string " + sqlConnectionString);
            if (sqlConnectionString == null )
            {
                throw new Exception("Don't start this api! Can't connect to a database");
            }

            builder.Services.AddDbContext<EmployeesDataContext>(options =>
            {
                options.UseSqlServer(sqlConnectionString);
            }); 

            var app = builder.Build();
            
            // Startup Configure
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}